#include "LibPlotter.h"

using namespace System;

const wchar_t* CLASSNAME = L"ChartTest";

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
	setPlotterTitleW(plotter, L"$y = sin(t)$");
	setPlotterLabelXW(plotter, L"$t / \\text{s}$");
	setPlotterLabelYW(plotter, L"$y / \\text{cm}$");
	setPlotTitleW(sine, L"a\\[>=]b");
	setPlotStyle(sine, STYLE_DASH);
	setPlotWidth(sine, 4);
	setPlotterRangeY1(plotter, -2, RANGE_AUTO);
	setPlotterTitleFontA(plotter, "Cascadia Mono", 24, ChartFontFlags_Bold);
	HPLOTTER plot2 = createPlotter();
	int wid = showPlotAsync(plotter);
	int wid2 = showPlotAsync(plot2);
	joinWindow(wid);
	joinWindow(wid2);

	int size;
	uint8_t* imageBuffer = renderPlotterToImageBuffer(plotter, 600, 400, &size, ChartImageFormat_RawRGB24);
	
	/*HWND wnd = CreateWindowExW(0, CLASSNAME, L"Text",
		WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT,
		NULL, NULL, GetModuleHandle(NULL), NULL);

	ShowWindow(wnd, SW_NORMAL);

	while (true)
	{
		MSG msg;
		GetMessageW(&msg, wnd, 0, 0);
		TranslateMessage(&msg);
		DispatchMessageW(&msg);
	}*/

	deletePlotData(sine);
	deletePlotter(plotter);
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