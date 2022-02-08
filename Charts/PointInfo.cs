using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartPlotter
{
	public class XYPointInfo
	{
		public XYPlotData PlotData { get; private set; }
		public int PointIndex { get; private set; }
		public double X { get;private set; }
		public double Y { get;private set; }
		public PointF ScreenPosition { get; private set; }
		public float Distance { get; private set; }

		public XYPointInfo(XYPlotData plot, int pointIndex, PointF screenPosition, float distance)
		{
			PlotData = plot;
			PointIndex = pointIndex;
			X = plot.DataX[pointIndex];
			Y = plot.DataY[pointIndex];
			ScreenPosition = screenPosition;
			Distance = distance;
		}
	}
}
