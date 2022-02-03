using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class ObjectCountWatcher
    {
        [DllImport("User32")]
        extern public static int GetGuiResources(IntPtr hProcess, int uiFlags);

		public static int GetGuiResourcesGDICount()
		{
			return GetGuiResources(Process.GetCurrentProcess().Handle, 0);
		}

		public static int GetGuiResourcesUserCount()
		{
			return GetGuiResources(Process.GetCurrentProcess().Handle, 1);
		}
	}
}
