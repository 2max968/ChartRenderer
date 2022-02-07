using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ChartPlotter.Native
{
    public class Class1
    {
        static Dictionary<uint, XYPlotRenderer> plotters = new Dictionary<uint, XYPlotRenderer>();
        static Dictionary<uint, XYPlotData> datas = new Dictionary<uint, XYPlotData>();
        static Random rand = new Random();

        [UnmanagedCallersOnly(EntryPoint = "helloWorld", CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static void helloWorld(int number)
        {
            Console.WriteLine("Hello World");
        }

        [UnmanagedCallersOnly(EntryPoint = "createPlotter", CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static uint createPlotter()
        {
            var plotter = new XYPlotRenderer();
            uint id;
            do
            {
                id = (uint)(rand.NextInt64() & 0xFFFFFFFF);
            }
            while (plotters.ContainsKey(id));
            plotters.Add(id, plotter);
            return id;
        }

        [UnmanagedCallersOnly(EntryPoint = "createPlotData", CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static unsafe uint createPlotData(double* x, double* y, int length)
        {
            double[] _x = new double[length];
            double[] _y = new double[length];
            for(int i = 0; i < length; i++)
            {
                _x[i] = x[i];
                _y[i] = y[i];
            }
            XYPlotData data = new XYPlotData(_x, _y);
            uint id;
            do
            {
                id = (uint)(rand.NextInt64() & 0xFFFFFFFF);
            }
            while (datas.ContainsKey(id));
            datas.Add(id, data);
            return id;
        }
    }
}