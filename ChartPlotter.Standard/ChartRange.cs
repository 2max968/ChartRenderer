using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartPlotter
{
    [TypeConverter(typeof(ChartRangeTypeConverter))]
    //[TypeConverter(typeof(ExpandableObjectConverter))]
    public class ChartRange
    {
        public double Min { get; set; } = 0;
        public double Max { get; set; } = 10;
        public double Range
        {
            get
            {
                return Max - Min;
            }
        }

        public ChartRange(double min, double max)
        {
            this.Min = min;
            this.Max = max;
        }

        public override string ToString()
        {
            string strmin = Min.ToString(CultureInfo.InvariantCulture);
            string strmax = Max.ToString(CultureInfo.InvariantCulture);
            return strmin + ":" + strmax;
        }
    }

    public class ChartRangeTypeConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (base.CanConvertFrom(context, sourceType))
                return true;
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (base.CanConvertTo(context, destinationType))
                return true;
            return destinationType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                if (((string)value) == "null")
                    return null;
                string[] parts = ((string)value).Split(':');
                if (parts.Length != 2) throw new ArgumentException();
                double min = double.Parse(parts[0], culture);
                double max = double.Parse(parts[1], culture);
                return new ChartRange(min, max);
            }
            else
            {
                return base.ConvertFrom(context, culture, value);
            }
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType.IsAssignableFrom(typeof(string)))
            {
                if (value == null)
                    return "null";
                var range = value as ChartRange;
                string strmin = range.Min.ToString(culture);
                string strmax = range.Max.ToString(culture);
                return strmin + ":" + strmax;
            }
            else
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }

    public class ChartRangeOptionsConverter : ExpandableObjectConverter
    { }
}
