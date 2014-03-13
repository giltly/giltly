using gilt.dblinq;
using gilt.util;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;

namespace gilt.geo
{
    /// <summary>
    /// Parse the MaxMind GeoLite Files
    /// A raw SQL dump of GeoIp and GeoLocation is around 650MB of INSERT INTO
    /// </summary>
    public sealed class GeoIpParse
    {
        //TODO:   fix the service trying to load the file from who knows where?
        private const string GEO_LITE_CITY_LOCATION = @"C:\Program Files (x86)\Giltly\Tail\GeoLiteCity-Location.csv";
        private const string GEO_LITE_CITY_BLOCKS = @"C:\Program Files (x86)\Giltly\Tail\GeoLiteCity-Blocks.csv";
        private static Logger _giltSetupLogger = LogManager.GetLogger("giltsetup");
        private const int TAKE_COUNT = 10000;
        private const int GEO_IP_COUNT = 1790461;
        private const int GEO_LOCATION_COUNT = 438386;

        /// <summary>
        /// Write the GeoLocation to the databse if it was not already found
        /// </summary>
        public void WriteToDatabase()
        {
            bool parseData = true;
            bool truncData = false;
            using (giltdbDataContext context = new giltdbDataContext(AppSettings.DatabaseConnectionString))
            {
                int geoIpCount = context.GeoIps.Count();
                int geoLocationCount = context.GeoLocations.Count();

                _giltSetupLogger.Info(String.Format("GeoIpCount Count: {0}", geoIpCount));
                _giltSetupLogger.Info(String.Format("GeoLocation Count: {0}", geoLocationCount));

                if ((GEO_IP_COUNT != geoIpCount) ||
                    (GEO_LOCATION_COUNT != geoLocationCount))
                {
                    truncData = true;
                }
                //everything is okay
                if ((GEO_IP_COUNT == geoIpCount) && (GEO_LOCATION_COUNT == geoLocationCount))
                {
                    parseData = false;
                }
                //if there the counts dont match it means something went wrong
                //do it again
                if (truncData)
                {
                    _giltSetupLogger.Error(String.Format("GeoLocation Data Bad Or Missing.  Truncating and Running Again"));
                    context.GeoLocations.DeleteAllOnSubmit(context.GeoLocations);
                    context.GeoIps.DeleteAllOnSubmit(context.GeoIps);
                    context.SubmitChanges();
                }
            }
            if (parseData)
            {
                _giltSetupLogger.Info(String.Format("Parsing GeoLocation Data"));
                this.ParseLocationFile();
                this.ParseBlockFile();
            }
            else
            {
                _giltSetupLogger.Info(String.Format("GeoLocation Was Present and Counts Matched.  No Processing Necessary"));
            }
        }

        private void ParseBlockFile()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            _giltSetupLogger.Info(String.Format("Inserting GeoLocation Block Data"));

            List<string[]> parsedLines = this.ParseInternal(GEO_LITE_CITY_BLOCKS,
                2,
                new string[] { " " },
                new char[] { '"' },
                new List<string>() { "#" },
                ",");

            List<GeoIp> geoIPList = new List<GeoIp>();
            (from a in parsedLines select a).ToList<string[]>().ForEach(x =>
            {                
                GeoIp gl = new GeoIp();
                //supports ipv6
                gl.StartIpNumber = BitConverter.GetBytes(Convert.ToUInt32(x[0]));
                gl.EndIpNumber = BitConverter.GetBytes(Convert.ToUInt32(x[1]));
                gl.LocationId = Convert.ToInt32(x[2]);
                geoIPList.Add(gl);
            });

            int takeModCount = ((geoIPList.Count / TAKE_COUNT) + 1);
            for (int i = 0; i < takeModCount; i++)
            {
                int offSet = (i * TAKE_COUNT);
                int takeLength = TAKE_COUNT;
                if ((offSet + TAKE_COUNT) > geoIPList.Count)
                {
                    takeLength = geoIPList.Count - offSet;
                }
                List<GeoIp> geoIpListTemp = geoIPList.GetRange(offSet, takeLength);
                using (TransactionScope trans = new TransactionScope())
                {
                    using (giltdbDataContext context = new giltdbDataContext(AppSettings.DatabaseConnectionString))
                    {
                        context.GeoIps.InsertAllOnSubmit(geoIpListTemp);
                        context.SubmitChanges();
                    }
                    trans.Complete();
                }
                _giltSetupLogger.Info(String.Format("GeoIp Data: Percent Done: {0} ", ((float)(i + 1) / (float)takeModCount).ToString("P")));
            }
            _giltSetupLogger.Info(String.Format("GeoIp Data: {0} ms", sw.ElapsedMilliseconds));
        }

        private void ParseLocationFile()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            _giltSetupLogger.Info(String.Format("Inserting GeoLocation Data"));

            List<string[]> parsedLines = this.ParseInternal(GEO_LITE_CITY_LOCATION,
                2,
                new string[] { " " },
                new char[] { '"' },
                new List<string>() { "#" },
                ",");

            List<GeoLocation> geoLocationList = new List<GeoLocation>();
            (from a in parsedLines select a).ToList<string[]>().ForEach(x =>
                 {
                     //fix city that has a comma in the name
                     // e.g.  "Washington, d.c."
                     if (10 == x.Length)
                     {
                         string[] fixQuoteComma = new string[9];
                         Array.Copy(x, 0, fixQuoteComma, 0, 4);
                         fixQuoteComma[3] = x[3] + "," + x[4];
                         Array.Copy(x, 5, fixQuoteComma, 4, 5);
                         x = fixQuoteComma;
                     }
                     GeoLocation gl = new GeoLocation();
                     gl.LocationId = Convert.ToInt32(x[0]);
                     gl.CountryCode = Convert.ToString(x[1]);
                     gl.RegionCode = Convert.ToString(x[2]);
                     gl.City = Convert.ToString(x[3]);
                     gl.PostalCode = Convert.ToString(x[4]);
                     gl.Latitude = Convert.ToDouble(x[5]);
                     gl.Longitude = Convert.ToDouble(x[6]);
                     gl.DmaCode = (x.Length >= 8 && !String.IsNullOrEmpty(x[7]) ? Convert.ToInt32(x[7]) : 0);
                     gl.AreaCode = (x.Length >= 9 && !String.IsNullOrEmpty(x[8]) ? Convert.ToInt32(x[8]) : 0);
                     geoLocationList.Add(gl);
                 });

            int takeModCount = ((geoLocationList.Count / TAKE_COUNT) + 1);
            for (int i = 0; i < takeModCount; i++)
            {
                int offSet = (i * TAKE_COUNT);
                int takeLength = TAKE_COUNT;
                if ((offSet + TAKE_COUNT) > geoLocationList.Count)
                {
                    takeLength = geoLocationList.Count - offSet;
                }
                List<GeoLocation> geoLocationListTemp = geoLocationList.GetRange(offSet, takeLength);
                using (TransactionScope trans = new TransactionScope())
                {
                    using (giltdbDataContext context = new giltdbDataContext(AppSettings.DatabaseConnectionString))
                    {
                        context.GeoLocations.InsertAllOnSubmit(geoLocationListTemp);
                        context.SubmitChanges();
                    }
                    trans.Complete();
                }
                _giltSetupLogger.Info(String.Format("GeoLocation Data: Percent Done: {0} ", ((float)(i + 1) / (float)takeModCount).ToString("P")));
            }
            _giltSetupLogger.Info(String.Format("GeoLocation Data: {0} ms", sw.ElapsedMilliseconds));
        }

        private List<string[]> ParseInternal(string FileName, int skipLineCount, string[] lineTrims, char[] valueTrims, List<string> ignoreLinesStartingWith, string _delimiter)
        {
            List<string[]> parsedLines = new List<string[]>();

            _giltSetupLogger.Info(String.Format("Trying To Read From File: {0}", FileName));
            if (File.Exists(FileName))
            {
                _giltSetupLogger.Info(String.Format("File: {0}  Found", FileName));
                try
                {
                    string line = null;
                    using (StreamReader sr = new StreamReader(FileName))
                    {
                        for (int skip = 0; skip < skipLineCount; skip++)
                        {
                            sr.ReadLine();
                        }

                        while (null != (line = sr.ReadLine()))
                        {
                            string trimmed = line;
                            foreach (string t in lineTrims)
                            {
                                trimmed = Regex.Replace(trimmed, "^" + t, string.Empty);
                            }
                            bool ignoreLine = false;
                            foreach (string il in ignoreLinesStartingWith)
                            {
                                if (trimmed.StartsWith(il))
                                {
                                    ignoreLine = true;
                                    break;
                                }
                            }
                            if (!ignoreLine)
                            {
                                string[] split = trimmed.Split(_delimiter.ToCharArray());
                                if ((split.Length >= 1) && !String.IsNullOrWhiteSpace(trimmed))
                                {
                                    //remove whitespace
                                    split = (from s in split select s.Trim()).ToArray();
                                    split = (from s in split select s.Trim(valueTrims)).ToArray();
                                    parsedLines.Add(split);
                                }
                            }
                        }
                    }
                }
                catch (Exception exc)
                {
                    _giltSetupLogger.Fatal(exc);
                }
            }
            return parsedLines;
        }
    }
}
