using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartPlotter
{
    public class CSVObject
    {
        public CSVObject(object value)
        {
            Value = value;
        }

        public object Value { get; set; }

        public bool IsReal()
        {
            if (IsNull()) return false;
            return Value is double || Value is int;
        }

        public bool IsInteger()
        {
            if (IsNull()) return false;
            return Value is int;
        }

        public bool IsText()
        {
            if (IsNull()) return false;
            return Value is string;
        }

        public bool IsNull()
        {
            return Value == null;
        }

        public double GetReal()
        {
            if (Value is double)
                return (double)Value;
            if (Value is int)
                return (int)Value;
            return 0;
        }

        public int GetInteger()
        {
            if (Value is int)
                return (int)Value;
            return 0;
        }

        public string GetText()
        {
            if (Value is string)
                return (string)Value;
            if (Value is double)
                return "" + (double)Value;
            if (Value is int)
                return "" + (int)Value;
            return "";
        }
    }
}
