#include ""
#include <windows.h>

typedef HPLOTTER   (__cdecl * type_createPlotter)       ();
typedef void       (__cdecl * type_deletePlotter)       (HPLOTTER plotter);
typedef void       (__cdecl * type_renderPlotter)       (HPLOTTER plotter, int width, int height, uint8_t* bitmap, int bitmapSize);
typedef void       (__cdecl * type_renderPlotterToFileW)(HPLOTTER plotter, const wchar_t* filename, int width, int height);
typedef void       (__cdecl * type_renderPlotterToFileA)(HPLOTTER plotter, const char* filename, int width, int height);
typedef HPLOTDATA  (__cdecl * type_createPlotData)      (const double* x, const double* y, int length);
typedef void       (__cdecl * type_deletePlotData)      (HPLOTDATA plot);
typedef void       (__cdecl * type_clearAllPlotData)    (HPLOTTER plotter);
typedef void       (__cdecl * type_addPlotData)         (HPLOTTER plotter, HPLOTDATA plot);
typedef void       (__cdecl * type_setPlotterTitleW)    (HPLOTTER plotter, const wchar_t* title);
typedef void       (__cdecl * type_setPlotterLabelW)    (HPLOTTER plotter, const wchar_t* labelX, const wchar_t* labelY);
typedef void       (__cdecl * type_setPlotterLabelY2W)  (HPLOTTER plotter, const wchar_t* label);
typedef void       (__cdecl * type_setPlotTitleW)       (HPLOTDATA plot, const wchar_t* title);
typedef void       (__cdecl * type_setPlotColor)        (HPLOTDATA plot, ChartColor color);
typedef void       (__cdecl * type_setPlotStyle)        (HPLOTDATA plot, char type);
typedef void       (__cdecl * type_showPlotW)           (HPLOTTER plotter, const wchar_t* windowTitle);
typedef void       (__cdecl * type_setPlotIndex)        (HPLOTDATA plot, int index);
typedef void       (__cdecl * type_setPlotWidth)        (HPLOTDATA plot, float width);
typedef void       (__cdecl * type_setPlotShowInLegend) (HPLOTDATA plot, bool visible);
typedef void       (__cdecl * type_setPlotterShowLegend)(HPLOTTER plotter, bool visible);
typedef void       (__cdecl * type_setPlotterColor)     (HPLOTTER plotter, ChartColor foreground, ChartColor background);
typedef void       (__cdecl * type_setPlotterRangeX)    (HPLOTTER plotter, double min, double max);
typedef void       (__cdecl * type_setPlotterRangeY1)   (HPLOTTER plotter, double min, double max);
typedef void       (__cdecl * type_setPlotterRangeY2)   (HPLOTTER plotter, double min, double max);
typedef ChartColor (__cdecl * type_colorFromRGB)        (unsigned char r, unsigned char g, unsigned char b);
typedef ChartColor (__cdecl * type_colorFromNameA)      (const char* name);
typedef ChartColor (__cdecl * type_colorFromNameW)      (const wchar_t* name);
typedef void       (__cdecl * type_plotAddPoint)        (HPLOTDATA plot, double x, double y);

static type_createPlotter        fp_createPlotter;
static type_deletePlotter        fp_deletePlotter;
static type_renderPlotter        fp_renderPlotter;
static type_renderPlotterToFileW fp_renderPlotterToFileW;
static type_renderPlotterToFileA fp_renderPlotterToFileA;
static type_createPlotData       fp_createPlotData;
static type_deletePlotData       fp_deletePlotData;
static type_clearAllPlotData     fp_clearAllPlotData;
static type_addPlotData          fp_addPlotData;
static type_setPlotterTitleW     fp_setPlotterTitleW;
static type_setPlotterLabelW     fp_setPlotterLabelW;
static type_setPlotterLabelY2W   fp_setPlotterLabelY2W;
static type_setPlotTitleW        fp_setPlotTitleW;
static type_setPlotColor         fp_setPlotColor;
static type_setPlotStyle         fp_setPlotStyle;
static type_showPlotW            fp_showPlotW;
static type_setPlotIndex         fp_setPlotIndex;
static type_setPlotWidth         fp_setPlotWidth;
static type_setPlotShowInLegend  fp_setPlotShowInLegend;
static type_setPlotterShowLegend fp_setPlotterShowLegend;
static type_setPlotterColor      fp_setPlotterColor;
static type_setPlotterRangeX     fp_setPlotterRangeX;
static type_setPlotterRangeY1    fp_setPlotterRangeY1;
static type_setPlotterRangeY2    fp_setPlotterRangeY2;
static type_colorFromRGB         fp_colorFromRGB;
static type_colorFromNameA       fp_colorFromNameA;
static type_colorFromNameW       fp_colorFromNameW;
static type_plotAddPoint         fp_plotAddPoint;

int __cdecl initChartPlotter()
{
	HMODULE lib = LoadLibraryW(L"ChartPlotter.Native.dll");
	if(lib == NULL)
		return 0;
	fp_createPlotter        = (type_createPlotter)GetProcAddress(lib, "createPlotter");
	fp_deletePlotter        = (type_deletePlotter)GetProcAddress(lib, "deletePlotter");
	fp_renderPlotter        = (type_renderPlotter)GetProcAddress(lib, "renderPlotter");
	fp_renderPlotterToFileW = (type_renderPlotterToFileW)GetProcAddress(lib, "renderPlotterToFileW");
	fp_renderPlotterToFileA = (type_renderPlotterToFileA)GetProcAddress(lib, "renderPlotterToFileA");
	fp_createPlotData       = (type_createPlotData)GetProcAddress(lib, "createPlotData");
	fp_deletePlotData       = (type_deletePlotData)GetProcAddress(lib, "deletePlotData");
	fp_clearAllPlotData     = (type_clearAllPlotData)GetProcAddress(lib, "clearAllPlotData");
	fp_addPlotData          = (type_addPlotData)GetProcAddress(lib, "addPlotData");
	fp_setPlotterTitleW     = (type_setPlotterTitleW)GetProcAddress(lib, "setPlotterTitleW");
	fp_setPlotterLabelW     = (type_setPlotterLabelW)GetProcAddress(lib, "setPlotterLabelW");
	fp_setPlotterLabelY2W   = (type_setPlotterLabelY2W)GetProcAddress(lib, "setPlotterLabelY2W");
	fp_setPlotTitleW        = (type_setPlotTitleW)GetProcAddress(lib, "setPlotTitleW");
	fp_setPlotColor         = (type_setPlotColor)GetProcAddress(lib, "setPlotColor");
	fp_setPlotStyle         = (type_setPlotStyle)GetProcAddress(lib, "setPlotStyle");
	fp_showPlotW            = (type_showPlotW)GetProcAddress(lib, "showPlotW");
	fp_setPlotIndex         = (type_setPlotIndex)GetProcAddress(lib, "setPlotIndex");
	fp_setPlotWidth         = (type_setPlotWidth)GetProcAddress(lib, "setPlotWidth");
	fp_setPlotShowInLegend  = (type_setPlotShowInLegend)GetProcAddress(lib, "setPlotShowInLegend");
	fp_setPlotterShowLegend = (type_setPlotterShowLegend)GetProcAddress(lib, "setPlotterShowLegend");
	fp_setPlotterColor      = (type_setPlotterColor)GetProcAddress(lib, "setPlotterColor");
	fp_setPlotterRangeX     = (type_setPlotterRangeX)GetProcAddress(lib, "setPlotterRangeX");
	fp_setPlotterRangeY1    = (type_setPlotterRangeY1)GetProcAddress(lib, "setPlotterRangeY1");
	fp_setPlotterRangeY2    = (type_setPlotterRangeY2)GetProcAddress(lib, "setPlotterRangeY2");
	fp_colorFromRGB         = (type_colorFromRGB)GetProcAddress(lib, "colorFromRGB");
	fp_colorFromNameA       = (type_colorFromNameA)GetProcAddress(lib, "colorFromNameA");
	fp_colorFromNameW       = (type_colorFromNameW)GetProcAddress(lib, "colorFromNameW");
	fp_plotAddPoint         = (type_plotAddPoint)GetProcAddress(lib, "plotAddPoint");
	return 1;
}

HPLOTTER __cdecl createPlotter()
{
	if(fp_createPlotter == NULL)
		throw 0;
	return fp_createPlotter();
}

void __cdecl deletePlotter(HPLOTTER plotter)
{
	if(fp_deletePlotter == NULL)
		throw 0;
	fp_deletePlotter(plotter);
}

void __cdecl renderPlotter(HPLOTTER plotter, int width, int height, uint8_t* bitmap, int bitmapSize)
{
	if(fp_renderPlotter == NULL)
		throw 0;
	fp_renderPlotter(plotter, width, height, bitmap, bitmapSize);
}

void __cdecl renderPlotterToFileW(HPLOTTER plotter, const wchar_t* filename, int width, int height)
{
	if(fp_renderPlotterToFileW == NULL)
		throw 0;
	fp_renderPlotterToFileW(plotter, filename, width, height);
}

void __cdecl renderPlotterToFileA(HPLOTTER plotter, const char* filename, int width, int height)
{
	if(fp_renderPlotterToFileA == NULL)
		throw 0;
	fp_renderPlotterToFileA(plotter, filename, width, height);
}

HPLOTDATA __cdecl createPlotData(const double* x, const double* y, int length)
{
	if(fp_createPlotData == NULL)
		throw 0;
	return fp_createPlotData(x, y, length);
}

void __cdecl deletePlotData(HPLOTDATA plot)
{
	if(fp_deletePlotData == NULL)
		throw 0;
	fp_deletePlotData(plot);
}

void __cdecl clearAllPlotData(HPLOTTER plotter)
{
	if(fp_clearAllPlotData == NULL)
		throw 0;
	fp_clearAllPlotData(plotter);
}

void __cdecl addPlotData(HPLOTTER plotter, HPLOTDATA plot)
{
	if(fp_addPlotData == NULL)
		throw 0;
	fp_addPlotData(plotter, plot);
}

void __cdecl setPlotterTitleW(HPLOTTER plotter, const wchar_t* title)
{
	if(fp_setPlotterTitleW == NULL)
		throw 0;
	fp_setPlotterTitleW(plotter, title);
}

void __cdecl setPlotterLabelW(HPLOTTER plotter, const wchar_t* labelX, const wchar_t* labelY)
{
	if(fp_setPlotterLabelW == NULL)
		throw 0;
	fp_setPlotterLabelW(plotter, labelX, labelY);
}

void __cdecl setPlotterLabelY2W(HPLOTTER plotter, const wchar_t* label)
{
	if(fp_setPlotterLabelY2W == NULL)
		throw 0;
	fp_setPlotterLabelY2W(plotter, label);
}

void __cdecl setPlotTitleW(HPLOTDATA plot, const wchar_t* title)
{
	if(fp_setPlotTitleW == NULL)
		throw 0;
	fp_setPlotTitleW(plot, title);
}

void __cdecl setPlotColor(HPLOTDATA plot, ChartColor color)
{
	if(fp_setPlotColor == NULL)
		throw 0;
	fp_setPlotColor(plot, color);
}

void __cdecl setPlotStyle(HPLOTDATA plot, char type)
{
	if(fp_setPlotStyle == NULL)
		throw 0;
	fp_setPlotStyle(plot, type);
}

void __cdecl showPlotW(HPLOTTER plotter, const wchar_t* windowTitle)
{
	if(fp_showPlotW == NULL)
		throw 0;
	fp_showPlotW(plotter, windowTitle);
}

void __cdecl setPlotIndex(HPLOTDATA plot, int index)
{
	if(fp_setPlotIndex == NULL)
		throw 0;
	fp_setPlotIndex(plot, index);
}

void __cdecl setPlotWidth(HPLOTDATA plot, float width)
{
	if(fp_setPlotWidth == NULL)
		throw 0;
	fp_setPlotWidth(plot, width);
}

void __cdecl setPlotShowInLegend(HPLOTDATA plot, bool visible)
{
	if(fp_setPlotShowInLegend == NULL)
		throw 0;
	fp_setPlotShowInLegend(plot, visible);
}

void __cdecl setPlotterShowLegend(HPLOTTER plotter, bool visible)
{
	if(fp_setPlotterShowLegend == NULL)
		throw 0;
	fp_setPlotterShowLegend(plotter, visible);
}

void __cdecl setPlotterColor(HPLOTTER plotter, ChartColor foreground, ChartColor background)
{
	if(fp_setPlotterColor == NULL)
		throw 0;
	fp_setPlotterColor(plotter, foreground, background);
}

void __cdecl setPlotterRangeX(HPLOTTER plotter, double min, double max)
{
	if(fp_setPlotterRangeX == NULL)
		throw 0;
	fp_setPlotterRangeX(plotter, min, max);
}

void __cdecl setPlotterRangeY1(HPLOTTER plotter, double min, double max)
{
	if(fp_setPlotterRangeY1 == NULL)
		throw 0;
	fp_setPlotterRangeY1(plotter, min, max);
}

void __cdecl setPlotterRangeY2(HPLOTTER plotter, double min, double max)
{
	if(fp_setPlotterRangeY2 == NULL)
		throw 0;
	fp_setPlotterRangeY2(plotter, min, max);
}

ChartColor __cdecl colorFromRGB(unsigned char r, unsigned char g, unsigned char b)
{
	if(fp_colorFromRGB == NULL)
		throw 0;
	return fp_colorFromRGB(r, g, b);
}

ChartColor __cdecl colorFromNameA(const char* name)
{
	if(fp_colorFromNameA == NULL)
		throw 0;
	return fp_colorFromNameA(name);
}

ChartColor __cdecl colorFromNameW(const wchar_t* name)
{
	if(fp_colorFromNameW == NULL)
		throw 0;
	return fp_colorFromNameW(name);
}

void __cdecl plotAddPoint(HPLOTDATA plot, double x, double y)
{
	if(fp_plotAddPoint == NULL)
		throw 0;
	fp_plotAddPoint(plot, x, y);
}

