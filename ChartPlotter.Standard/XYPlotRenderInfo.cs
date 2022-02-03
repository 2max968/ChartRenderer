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
        public ChartRange RangeY { get; private set; }
        public Rect ChartBounds { get; private set; }

        public XYPlotRenderInfo(ChartRange xrange, ChartRange yrange, Rect bounds)
        {
            RangeX = xrange;
            RangeY = yrange;
            ChartBounds = bounds;
        }
    }
}
