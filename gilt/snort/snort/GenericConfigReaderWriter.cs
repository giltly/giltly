using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace gilt.snort
{
    /// <summary>
    /// Abstract class to help with parsing the configuration file that are line and delimiter based 
    /// like the snort configuration files
    /// </summary>
    public abstract class GenericConfigReaderWriter : IGenericConfigReader, IGenericConfigWriter
    {
        private string _delimiter = "||";
        private StreamReader _sr = null;
        private string _filename = null;
        private List<string> _ignoreLinesStartingWith = null;

        /// <summary>
        /// Characters to remove from the begining and end of each line
        /// </summary>
        protected string[] _lineTrims = null;
        /// <summary>
        /// A list of each line that has been parsed and its contents
        /// </summary>
        protected List<string[]> _parsedLines = null;
        /// <summary>
        /// Setup Nlog logger
        /// </summary>
        protected Logger _giltSetupLogger = null;

        /// <summary>
        /// Create a generic config file reader
        /// </summary>
        /// <param name="FileName">The full path to the configuration file</param>
        /// <param name="Delimiter">A line parsing delimiter</param>
        /// <param name="Trims">Characters to trim from the start and finish of a line</param>
        public GenericConfigReaderWriter(string FileName, string Delimiter = "||", string[] Trims = null)
        {
            _filename = FileName;
            _sr = new StreamReader(_filename);
            _lineTrims = Trims ?? new string[] {" "} ;
            _delimiter = Delimiter;
            _ignoreLinesStartingWith = new List<string>() { "#" };            
            _giltSetupLogger = LogManager.GetLogger("giltsetup");
        }

        #region IGenericConfigReader
        /// <summary>
        /// Read a configuration file and store the results in _parsedLines
        /// </summary>
        public void ReadFile()
        {
            _parsedLines = new List<string[]>();
            if (File.Exists(_filename))
            {
                try
                {
                    string line = null;
                    using (StreamReader _sr = new StreamReader(_filename))
                    {
                        while (null != (line = _sr.ReadLine()))
                        {
                            string trimmed = line;
                            foreach (string t in _lineTrims)
                            {
                                trimmed = Regex.Replace(trimmed, "^" + t, string.Empty);
                            }
                            bool ignoreLine = false;
                            foreach (string il in _ignoreLinesStartingWith)
                            {
                                if (trimmed.StartsWith(il))
                                {
                                    ignoreLine = true;
                                    break;
                                }
                            }
                            if (!ignoreLine)
                            {
                                string[] split = trimmed.Split(_delimiter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                if ((split.Length >= 1) && !String.IsNullOrWhiteSpace(trimmed))
                                {
                                    //remove whitespace
                                    split = (from s in split select s.Trim()).ToArray();
                                    _parsedLines.Add(split);
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
        }
        #endregion

        #region IGenericConfigWriter
        /// <summary>
        /// Call WriteToDabase() function on the current class's definition
        /// </summary>
        public virtual void WriteToDatabase()
        {
            this.WriteToDatabase();
        }
        #endregion
    }
}
