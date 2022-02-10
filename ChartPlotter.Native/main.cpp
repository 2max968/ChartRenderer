#include "LibPlotter.h"

using namespace System;

#ifdef DLLRUN
int main()
{
	double (*negsin_f)(double);

	double x[628];
	double y[628];
	HPLOTDATA cos = createPlotData(NULL, NULL, 0);
	for (int i = 0; i < 628; i++)
	{
		x[i] = i / 100.0;
		y[i] = Math::Sin(x[i]);

		plotAddPoint(cos, x[i], Math::Cos(x[i]));
	}

	HPLOTTER plotter = createPlotter();
	HPLOTDATA sine = createPlotData(x, y, 628);

	negsin_f = [](double x) {
		return -sin(x);
	};
	HPLOTDATA negsin = createPlotDataY(0, 0.001, 3.1415 * 2, negsin_f);

	HPLOTDATA invsin = createPlotDataXY(0, 0.001, 3.1415 * 2, [](double t, double* x, double* y) {
		*y = t;
		*x = sin(t);
		});

	addPlotData(plotter, sine);
	addPlotData(plotter, cos);
	addPlotData(plotter, negsin);
	addPlotData(plotter, invsin);
	setPlotterTitleW(plotter, L"Sinus");
	setPlotterLabelXW(plotter, L"time / s");
	setPlotterLabelYW(plotter, L"Value");
	setPlotTitleW(sine, L"a\\[>=]b");
	setPlotStyle(sine, STYLE_DASH);
	setPlotWidth(sine, 4);
	setPlotterRangeY1(plotter, -2, RANGE_AUTO);
	showPlotW(plotter);

	deletePlotData(sine);
	deletePlotter(plotter);

	plotXY(x, y, 628, "Sinus", "time / \\[omega]", "\\[alpha]");
}
#else
/*BOOL WINAPI DllMain(
	HINSTANCE hinstDLL,  // handle to DLL module
	DWORD fdwReason,     // reason for calling function
	LPVOID lpReserved )  // reserved
{
	// Perform actions based on the reason for calling.
	switch (fdwReason)
	{
	case DLL_PROCESS_ATTACH:
		// Initialize once for each new process.
		// Return FALSE to fail DLL load.
		break;

	case DLL_THREAD_ATTACH:
		// Do thread-specific initialization.
		break;

	case DLL_THREAD_DETACH:
		// Do thread-specific cleanup.
		break;

	case DLL_PROCESS_DETACH:
		// Perform any necessary cleanup.
		break;
	}
	return TRUE;  // Successful DLL_PROCESS_ATTACH.
}*/
#endif