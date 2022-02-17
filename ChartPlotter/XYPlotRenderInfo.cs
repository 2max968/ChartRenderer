using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartPlotter
{
    public class XYPlotRenderInfo
    {
        public ChartRange RangeX { get; private set; }
        public ChartRange RangeY1 { get; private set; }
        public ChartRange RangeY2 { get; private set; }
        public Rect ChartBounds { get; private set; }

        public XYPlotRenderInfo(ChartRange xrange, ChartRange yrange1, ChartRange yrange2, Rect bounds)
        {
            RangeX = xrange;
            RangeY1 = yrange1;
            RangeY2 = yrange2;
            ChartBounds = bounds;
        }
    }
}
