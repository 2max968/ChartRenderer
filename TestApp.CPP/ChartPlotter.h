#pragma once

#include <inttypes.h>

#ifndef CHARTRENDERER_FULLTYPES_
typedef void* HPLOTTER;
typedef void* HPLOTDATA;
#else
#include <msclr/gcroot.h>
using PLOTTER = msclr::gcroot<ChartPlotter::XYPlotRenderer^>;
using HPLOTTER = PLOTTER*;
using PLOTDATA = msclr::gcroot<ChartPlotter::XYPlotData^>;
using HPLOTDATA = PLOTDATA*;
#endif

typedef enum : int {
	Regular,
	Bold,
	Italic,
	Underline
} ChartFontFlags;

typedef struct
{
	const char* fontFamily;
	float fontSize;
	ChartFontFlags fontFlags;
} ChartFont;

typedef struct
{
	unsigned char r;
	unsigned char g;
	unsigned char b;
} ChartColor;

#define STYLE_LINE			'_'
#define STYLE_DASH			'-'
#define STYLE_CROSS			'X'
#define STYLE_CIRCLE		'O'
#define STYLE_DOT			'.'
#define STYLE_LINEANDDOT	'!'

#define CHART_NAN			((float)((1e+300 * 1e+300) * 0.0F))
#define RANGE_AUTO			CHART_NAN

int        __cdecl initChartPlotter();
HPLOTTER   __cdecl createPlotter();
void       __cdecl deletePlotter(HPLOTTER plotter);
void       __cdecl renderPlotter(HPLOTTER plotter, int width, int height, uint8_t* bitmap, int bitmapSize);
void       __cdecl renderPlotterToFileW(HPLOTTER plotter, const wchar_t* filename, int width, int height);
void       __cdecl renderPlotterToFileA(HPLOTTER plotter, const char* filename, int width, int height);
HPLOTDATA  __cdecl createPlotData(const double* x, const double* y, int length);
void       __cdecl deletePlotData(HPLOTDATA plot);
void       __cdecl clearAllPlotData(HPLOTTER plotter);
void       __cdecl addPlotData(HPLOTTER plotter, HPLOTDATA plot);
void       __cdecl setPlotterTitleW(HPLOTTER plotter, const wchar_t* title);
void       __cdecl setPlotterLabelW(HPLOTTER plotter, const wchar_t* labelX, const wchar_t* labelY);
void       __cdecl setPlotterLabelY2W(HPLOTTER plotter, const wchar_t* label);
void       __cdecl setPlotTitleW(HPLOTDATA plot, const wchar_t* title);
void       __cdecl setPlotColor(HPLOTDATA plot, ChartColor color);
void       __cdecl setPlotStyle(HPLOTDATA plot, char type);
void       __cdecl showPlotW(HPLOTTER plotter, const wchar_t* windowTitle = NULL);
void       __cdecl setPlotIndex(HPLOTDATA plot, int index);
void       __cdecl setPlotWidth(HPLOTDATA plot, float width);
void       __cdecl setPlotShowInLegend(HPLOTDATA plot, bool visible);
void       __cdecl setPlotterShowLegend(HPLOTTER plotter, bool visible);
void       __cdecl setPlotterColor(HPLOTTER plotter, ChartColor foreground, ChartColor background);
void       __cdecl setPlotterRangeX(HPLOTTER plotter, double min, double max);
void       __cdecl setPlotterRangeY1(HPLOTTER plotter, double min, double max);
void       __cdecl setPlotterRangeY2(HPLOTTER plotter, double min, double max);
ChartColor __cdecl colorFromRGB(unsigned char r, unsigned char g, unsigned char b);
ChartColor __cdecl colorFromNameA(const char* name);
ChartColor __cdecl colorFromNameW(const wchar_t* name);
void       __cdecl plotAddPoint(HPLOTDATA plot, double x, double y);
