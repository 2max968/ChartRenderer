using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartPlotter
{
    public class CSVSettings
    {
        public string EntrySeparators { get; set; } = ";,";
        public bool IgnoreQuotationmarks { get; set; } = false;
        public CultureInfo CultureInfo { get; set; } = CultureInfo.InvariantCulture;
    }
}
