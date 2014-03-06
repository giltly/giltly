using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimileTimeline
{
              //eventSource: eventSource,
              //date: "Sat 12 Nov 2011 00:00:00 GMT-0000",
              //width: "70%",              
              //layout: "original",
              //intervalUnit: Timeline.DateTime.DAY,
              //intervalPixels: 50

    public class BandInfo
    {
        public DateTime Date;
        public int WidthPercent;
        public string Layout;
        public DateTimeFormats IntervalUnit;
        public int IntervalPixels;

        public BandInfo()
        {            
            this.WidthPercent = 70;            
            this.Layout = "original";
            this.IntervalUnit = DateTimeFormats.DAY;
            this.IntervalPixels = 50;
        }

    }
}
