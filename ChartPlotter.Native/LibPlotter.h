#pragma once

#include "Definitions.h"
#include <Windows.h>
#include <inttypes.h>
#include <vector>

#define DLLEXPORT(type)	extern "C" _declspec(dllexport) type _cdecl

bool checkHandle(HPLOTTER plotter);
bool checkHandle(HPLOTDATA plot);
DLLEXPORT(HPLOTTER) createPlotter();
DLLEXPORT(void) deletePlotter(HPLOTTER plotter);
DLLEXPORT(void) renderPlotter(HPLOTTER plotter, int width, int height, uint8_t* bitmap, int bitmapSize);
DLLEXPORT(void) renderPlotterToFileW(HPLOTTER plotter, const wchar_t* filename, int width, int height);
DLLEXPORT(void) renderPlotterToFileA(HPLOTTER plotter, const char* filename, int width, int height);
DLLEXPORT(HPLOTDATA) createPlotData(const double* x, const double* y, int length);
DLLEXPORT(void) deletePlotData(HPLOTDATA plot);
DLLEXPORT(void) clearAllPlotData(HPLOTTER plotter);
DLLEXPORT(void) addPlotData(HPLOTTER plotter, HPLOTDATA plot);
DLLEXPORT(void) setPlotterTitleW(HPLOTTER plotter, const wchar_t* title);
DLLEXPORT(void) setPlotterLabelW(HPLOTTER plotter, const wchar_t* labelX, const wchar_t* labelY);
DLLEXPORT(void) setPlotterLabelY2W(HPLOTTER plotter, const wchar_t* label);
DLLEXPORT(void) setPlotTitleW(HPLOTDATA plot, const wchar_t* title);
DLLEXPORT(void) setPlotColor(HPLOTDATA plot, ChartColor color);
DLLEXPORT(void) setPlotStyle(HPLOTDATA plot, char type);
DLLEXPORT(void) showPlotW(HPLOTTER plotter, const wchar_t* windowTitle = NULL);
DLLEXPORT(void) setPlotIndex(HPLOTDATA plot, int index);
DLLEXPORT(void) setPlotWidth(HPLOTDATA plot, float width);
DLLEXPORT(void) setPlotShowInLegend(HPLOTDATA plot, bool visible);
DLLEXPORT(void) setPlotterShowLegend(HPLOTTER plotter, bool visible);
DLLEXPORT(void) setPlotterColor(HPLOTTER plotter, ChartColor foreground, ChartColor background);
DLLEXPORT(void) setPlotterRangeX(HPLOTTER plotter, double min, double max);
DLLEXPORT(void) setPlotterRangeY1(HPLOTTER plotter, double min, double max);
DLLEXPORT(void) setPlotterRangeY2(HPLOTTER plotter, double min, double max);
DLLEXPORT(ChartColor) colorFromRGB(unsigned char r, unsigned char g, unsigned char b);
DLLEXPORT(ChartColor) colorFromNameA(const char* name);
DLLEXPORT(ChartColor) colorFromNameW(const wchar_t* name);
DLLEXPORT(void) plotAddPoint(HPLOTDATA plot, double x, double y);