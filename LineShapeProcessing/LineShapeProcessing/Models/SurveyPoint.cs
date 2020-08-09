using System;
using System.Collections.Generic;
using System.Text;

namespace LineShapeProcessing.Models
{
    public class SurveyPoint
    {
        public int SequenceNumber { get; set; }
        public string No { get; set; }
        public string BacksightPoint { get; set; }
        public decimal ForsightValue { get; set; }
        public decimal BacksightValue { get; set; }
        public decimal ElevationValue { get; set; }
        public decimal ElevationModValue { get; set; }
        public decimal ModElevationValue { get; set; }

    }
}
