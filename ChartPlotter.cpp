#include ".\ChartPlotter.h"
#include <windows.h>
#if defined(WINX64) or defined(__x86_64__)
#define DLL_NAME L"ChartPlotter.Native64.dll"
#else
#define DLL_NAME L"ChartPlotter.Native32.dll"
#endif

#ifdef __cplusplus
extern "C"{
#endif

typedef HPLOTTER   (__cdecl * type_createPlotter)             ();
typedef void       (__cdecl * type_deletePlotter)             (HPLOTTER plotter);
typedef void       (__cdecl * type_renderPlotterToFileW)      (HPLOTTER plotter, const wchar_t* filename, int width, int height);
typedef void       (__cdecl * type_renderPlotterToFileA)      (HPLOTTER plotter, const char* filename, int width, int height);
typedef HPLOTDATA  (__cdecl * type_createPlotData)            (const double* x, const double* y, int length);
typedef void       (__cdecl * type_deletePlotData)            (HPLOTDATA plot);
typedef void       (__cdecl * type_clearAllPlotData)          (HPLOTTER plotter);
typedef void       (__cdecl * type_addPlotData)               (HPLOTTER plotter, HPLOTDATA plot);
typedef void       (__cdecl * type_setPlotterTitleW)          (HPLOTTER plotter, const wchar_t* title);
typedef void       (__cdecl * type_setPlotterLabelXW)         (HPLOTTER plotter, const wchar_t* labelX);
typedef void       (__cdecl * type_setPlotterLabelYW)         (HPLOTTER plotter, const wchar_t* labelY);
typedef void       (__cdecl * type_setPlotterLabelY2W)        (HPLOTTER plotter, const wchar_t* label);
typedef void       (__cdecl * type_setPlotTitleW)             (HPLOTDATA plot, const wchar_t* title);
typedef void       (__cdecl * type_setPlotColor)              (HPLOTDATA plot, ChartColor color);
typedef void       (__cdecl * type_setPlotterTitleA)          (HPLOTTER plotter, const char* title);
typedef void       (__cdecl * type_setPlotterLabelXA)         (HPLOTTER plotter, const char* labelX);
typedef void       (__cdecl * type_setPlotterLabelYA)         (HPLOTTER plotter, const char* labelY);
typedef void       (__cdecl * type_setPlotterLabelY2A)        (HPLOTTER plotter, const char* label);
typedef void       (__cdecl * type_setPlotTitleA)             (HPLOTDATA plot, const char* title);
typedef void       (__cdecl * type_setPlotStyle)              (HPLOTDATA plot, char type);
typedef void       (__cdecl * type_showPlotW)                 (HPLOTTER plotter, const wchar_t* windowTitle);
typedef void       (__cdecl * type_showPlotA)                 (HPLOTTER plotter, const char* windowTitle);
typedef void       (__cdecl * type_setPlotIndex)              (HPLOTDATA plot, int index);
typedef void       (__cdecl * type_setPlotWidth)              (HPLOTDATA plot, float width);
typedef void       (__cdecl * type_setPlotShowInLegend)       (HPLOTDATA plot, bool visible);
typedef void       (__cdecl * type_setPlotterShowLegend)      (HPLOTTER plotter, bool visible);
typedef void       (__cdecl * type_setPlotterColor)           (HPLOTTER plotter, ChartColor foreground, ChartColor background);
typedef void       (__cdecl * type_setPlotterRangeX)          (HPLOTTER plotter, double min, double max);
typedef void       (__cdecl * type_setPlotterRangeY1)         (HPLOTTER plotter, double min, double max);
typedef void       (__cdecl * type_setPlotterRangeY2)         (HPLOTTER plotter, double min, double max);
typedef ChartColor (__cdecl * type_colorFromRGB)              (unsigned char r, unsigned char g, unsigned char b);
typedef ChartColor (__cdecl * type_colorFromNameA)            (const char* name);
typedef ChartColor (__cdecl * type_colorFromNameW)            (const wchar_t* name);
typedef void       (__cdecl * type_plotAddPoint)              (HPLOTDATA plot, double x, double y);
typedef void       (__cdecl * type_plotXY)                    (double* x, double* y, int length, const char* title, const char* labelX, const char* labelY);
typedef HPLOTDATA  (__cdecl * type_createPlotDataY)           (double xstart, double xstep, double xend, functionY function);
typedef HPLOTDATA  (__cdecl * type_createPlotDataXY)          (double xstart, double xstep, double xend, functionXY function);
typedef uint8_t*   (__cdecl * type_renderPlotterToImageBuffer)(HPLOTTER plotter, int width, int height, int* size, ChartImageFormat format);
typedef void       (__cdecl * type_deleteImageBuffer)         (uint8_t* imageBuffer);
typedef void       (__cdecl * type_setPlotterTitleFontW)      (HPLOTTER plotter, const wchar_t* fontFamily, float fontSize, ChartFontFlags flags);
typedef void       (__cdecl * type_setPlotterFontW)           (HPLOTTER plotter, const wchar_t* fontFamily, float fontSize, ChartFontFlags flags);
typedef void       (__cdecl * type_setPlotterLegendFontW)     (HPLOTTER plotter, const wchar_t* fontFamily, float fontSize, ChartFontFlags flags);
typedef void       (__cdecl * type_setPlotterTitleFontA)      (HPLOTTER plotter, const char* fontFamily, float fontSize, ChartFontFlags flags);
typedef void       (__cdecl * type_setPlotterFontA)           (HPLOTTER plotter, const char* fontFamily, float fontSize, ChartFontFlags flags);
typedef void       (__cdecl * type_setPlotterLegendFontA)     (HPLOTTER plotter, const char* fontFamily, float fontSize, ChartFontFlags flags);
typedef uint32_t   (__cdecl * type_getPlotDataLength)         (HPLOTDATA plot);
typedef void       (__cdecl * type_getPlotDataX)              (HPLOTDATA plot, double* buffer);
typedef void       (__cdecl * type_getPlotDataY)              (HPLOTDATA plot, double* buffer);
typedef int        (__cdecl * type_showPlotAsync)             (HPLOTTER plotter, const wchar_t* title);
typedef void       (__cdecl * type_joinWindow)                (int id);
typedef void       (__cdecl * type_plotWindowRedraw)          (int id);
typedef void       (__cdecl * type_showTable)                 (HPLOTDATA plot);

static type_createPlotter              fp_createPlotter;
static type_deletePlotter              fp_deletePlotter;
static type_renderPlotterToFileW       fp_renderPlotterToFileW;
static type_renderPlotterToFileA       fp_renderPlotterToFileA;
static type_createPlotData             fp_createPlotData;
static type_deletePlotData             fp_deletePlotData;
static type_clearAllPlotData           fp_clearAllPlotData;
static type_addPlotData                fp_addPlotData;
static type_setPlotterTitleW           fp_setPlotterTitleW;
static type_setPlotterLabelXW          fp_setPlotterLabelXW;
static type_setPlotterLabelYW          fp_setPlotterLabelYW;
static type_setPlotterLabelY2W         fp_setPlotterLabelY2W;
static type_setPlotTitleW              fp_setPlotTitleW;
static type_setPlotColor               fp_setPlotColor;
static type_setPlotterTitleA           fp_setPlotterTitleA;
static type_setPlotterLabelXA          fp_setPlotterLabelXA;
static type_setPlotterLabelYA          fp_setPlotterLabelYA;
static type_setPlotterLabelY2A         fp_setPlotterLabelY2A;
static type_setPlotTitleA              fp_setPlotTitleA;
static type_setPlotStyle               fp_setPlotStyle;
static type_showPlotW                  fp_showPlotW;
static type_showPlotA                  fp_showPlotA;
static type_setPlotIndex               fp_setPlotIndex;
static type_setPlotWidth               fp_setPlotWidth;
static type_setPlotShowInLegend        fp_setPlotShowInLegend;
static type_setPlotterShowLegend       fp_setPlotterShowLegend;
static type_setPlotterColor            fp_setPlotterColor;
static type_setPlotterRangeX           fp_setPlotterRangeX;
static type_setPlotterRangeY1          fp_setPlotterRangeY1;
static type_setPlotterRangeY2          fp_setPlotterRangeY2;
static type_colorFromRGB               fp_colorFromRGB;
static type_colorFromNameA             fp_colorFromNameA;
static type_colorFromNameW             fp_colorFromNameW;
static type_plotAddPoint               fp_plotAddPoint;
static type_plotXY                     fp_plotXY;
static type_createPlotDataY            fp_createPlotDataY;
static type_createPlotDataXY           fp_createPlotDataXY;
static type_renderPlotterToImageBuffer fp_renderPlotterToImageBuffer;
static type_deleteImageBuffer          fp_deleteImageBuffer;
static type_setPlotterTitleFontW       fp_setPlotterTitleFontW;
static type_setPlotterFontW            fp_setPlotterFontW;
static type_setPlotterLegendFontW      fp_setPlotterLegendFontW;
static type_setPlotterTitleFontA       fp_setPlotterTitleFontA;
static type_setPlotterFontA            fp_setPlotterFontA;
static type_setPlotterLegendFontA      fp_setPlotterLegendFontA;
static type_getPlotDataLength          fp_getPlotDataLength;
static type_getPlotDataX               fp_getPlotDataX;
static type_getPlotDataY               fp_getPlotDataY;
static type_showPlotAsync              fp_showPlotAsync;
static type_joinWindow                 fp_joinWindow;
static type_plotWindowRedraw           fp_plotWindowRedraw;
static type_showTable                  fp_showTable;

int __cdecl initChartPlotter()
{
	HMODULE lib = LoadLibraryW(DLL_NAME);
	if(lib == NULL)
		return 0;
	fp_createPlotter              = (type_createPlotter)GetProcAddress(lib, "createPlotter");
	fp_deletePlotter              = (type_deletePlotter)GetProcAddress(lib, "deletePlotter");
	fp_renderPlotterToFileW       = (type_renderPlotterToFileW)GetProcAddress(lib, "renderPlotterToFileW");
	fp_renderPlotterToFileA       = (type_renderPlotterToFileA)GetProcAddress(lib, "renderPlotterToFileA");
	fp_createPlotData             = (type_createPlotData)GetProcAddress(lib, "createPlotData");
	fp_deletePlotData             = (type_deletePlotData)GetProcAddress(lib, "deletePlotData");
	fp_clearAllPlotData           = (type_clearAllPlotData)GetProcAddress(lib, "clearAllPlotData");
	fp_addPlotData                = (type_addPlotData)GetProcAddress(lib, "addPlotData");
	fp_setPlotterTitleW           = (type_setPlotterTitleW)GetProcAddress(lib, "setPlotterTitleW");
	fp_setPlotterLabelXW          = (type_setPlotterLabelXW)GetProcAddress(lib, "setPlotterLabelXW");
	fp_setPlotterLabelYW          = (type_setPlotterLabelYW)GetProcAddress(lib, "setPlotterLabelYW");
	fp_setPlotterLabelY2W         = (type_setPlotterLabelY2W)GetProcAddress(lib, "setPlotterLabelY2W");
	fp_setPlotTitleW              = (type_setPlotTitleW)GetProcAddress(lib, "setPlotTitleW");
	fp_setPlotColor               = (type_setPlotColor)GetProcAddress(lib, "setPlotColor");
	fp_setPlotterTitleA           = (type_setPlotterTitleA)GetProcAddress(lib, "setPlotterTitleA");
	fp_setPlotterLabelXA          = (type_setPlotterLabelXA)GetProcAddress(lib, "setPlotterLabelXA");
	fp_setPlotterLabelYA          = (type_setPlotterLabelYA)GetProcAddress(lib, "setPlotterLabelYA");
	fp_setPlotterLabelY2A         = (type_setPlotterLabelY2A)GetProcAddress(lib, "setPlotterLabelY2A");
	fp_setPlotTitleA              = (type_setPlotTitleA)GetProcAddress(lib, "setPlotTitleA");
	fp_setPlotStyle               = (type_setPlotStyle)GetProcAddress(lib, "setPlotStyle");
	fp_showPlotW                  = (type_showPlotW)GetProcAddress(lib, "showPlotW");
	fp_showPlotA                  = (type_showPlotA)GetProcAddress(lib, "showPlotA");
	fp_setPlotIndex               = (type_setPlotIndex)GetProcAddress(lib, "setPlotIndex");
	fp_setPlotWidth               = (type_setPlotWidth)GetProcAddress(lib, "setPlotWidth");
	fp_setPlotShowInLegend        = (type_setPlotShowInLegend)GetProcAddress(lib, "setPlotShowInLegend");
	fp_setPlotterShowLegend       = (type_setPlotterShowLegend)GetProcAddress(lib, "setPlotterShowLegend");
	fp_setPlotterColor            = (type_setPlotterColor)GetProcAddress(lib, "setPlotterColor");
	fp_setPlotterRangeX           = (type_setPlotterRangeX)GetProcAddress(lib, "setPlotterRangeX");
	fp_setPlotterRangeY1          = (type_setPlotterRangeY1)GetProcAddress(lib, "setPlotterRangeY1");
	fp_setPlotterRangeY2          = (type_setPlotterRangeY2)GetProcAddress(lib, "setPlotterRangeY2");
	fp_colorFromRGB               = (type_colorFromRGB)GetProcAddress(lib, "colorFromRGB");
	fp_colorFromNameA             = (type_colorFromNameA)GetProcAddress(lib, "colorFromNameA");
	fp_colorFromNameW             = (type_colorFromNameW)GetProcAddress(lib, "colorFromNameW");
	fp_plotAddPoint               = (type_plotAddPoint)GetProcAddress(lib, "plotAddPoint");
	fp_plotXY                     = (type_plotXY)GetProcAddress(lib, "plotXY");
	fp_createPlotDataY            = (type_createPlotDataY)GetProcAddress(lib, "createPlotDataY");
	fp_createPlotDataXY           = (type_createPlotDataXY)GetProcAddress(lib, "createPlotDataXY");
	fp_renderPlotterToImageBuffer = (type_renderPlotterToImageBuffer)GetProcAddress(lib, "renderPlotterToImageBuffer");
	fp_deleteImageBuffer          = (type_deleteImageBuffer)GetProcAddress(lib, "deleteImageBuffer");
	fp_setPlotterTitleFontW       = (type_setPlotterTitleFontW)GetProcAddress(lib, "setPlotterTitleFontW");
	fp_setPlotterFontW            = (type_setPlotterFontW)GetProcAddress(lib, "setPlotterFontW");
	fp_setPlotterLegendFontW      = (type_setPlotterLegendFontW)GetProcAddress(lib, "setPlotterLegendFontW");
	fp_setPlotterTitleFontA       = (type_setPlotterTitleFontA)GetProcAddress(lib, "setPlotterTitleFontA");
	fp_setPlotterFontA            = (type_setPlotterFontA)GetProcAddress(lib, "setPlotterFontA");
	fp_setPlotterLegendFontA      = (type_setPlotterLegendFontA)GetProcAddress(lib, "setPlotterLegendFontA");
	fp_getPlotDataLength          = (type_getPlotDataLength)GetProcAddress(lib, "getPlotDataLength");
	fp_getPlotDataX               = (type_getPlotDataX)GetProcAddress(lib, "getPlotDataX");
	fp_getPlotDataY               = (type_getPlotDataY)GetProcAddress(lib, "getPlotDataY");
	fp_showPlotAsync              = (type_showPlotAsync)GetProcAddress(lib, "showPlotAsync");
	fp_joinWindow                 = (type_joinWindow)GetProcAddress(lib, "joinWindow");
	fp_plotWindowRedraw           = (type_plotWindowRedraw)GetProcAddress(lib, "plotWindowRedraw");
	fp_showTable                  = (type_showTable)GetProcAddress(lib, "showTable");
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

void __cdecl renderPlotterToFile(HPLOTTER plotter, const wchar_t* filename, int width, int height)
{
	if(fp_renderPlotterToFileW == NULL)
		throw 0;
	fp_renderPlotterToFileW(plotter, filename, width, height);
}

void __cdecl renderPlotterToFile(HPLOTTER plotter, const char* filename, int width, int height)
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

void __cdecl setPlotterTitle(HPLOTTER plotter, const wchar_t* title)
{
	if(fp_setPlotterTitleW == NULL)
		throw 0;
	fp_setPlotterTitleW(plotter, title);
}

void __cdecl setPlotterLabelX(HPLOTTER plotter, const wchar_t* labelX)
{
	if(fp_setPlotterLabelXW == NULL)
		throw 0;
	fp_setPlotterLabelXW(plotter, labelX);
}

void __cdecl setPlotterLabelY(HPLOTTER plotter, const wchar_t* labelY)
{
	if(fp_setPlotterLabelYW == NULL)
		throw 0;
	fp_setPlotterLabelYW(plotter, labelY);
}

void __cdecl setPlotterLabelY2(HPLOTTER plotter, const wchar_t* label)
{
	if(fp_setPlotterLabelY2W == NULL)
		throw 0;
	fp_setPlotterLabelY2W(plotter, label);
}

void __cdecl setPlotTitle(HPLOTDATA plot, const wchar_t* title)
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

void __cdecl setPlotterTitle(HPLOTTER plotter, const char* title)
{
	if(fp_setPlotterTitleA == NULL)
		throw 0;
	fp_setPlotterTitleA(plotter, title);
}

void __cdecl setPlotterLabelX(HPLOTTER plotter, const char* labelX)
{
	if(fp_setPlotterLabelXA == NULL)
		throw 0;
	fp_setPlotterLabelXA(plotter, labelX);
}

void __cdecl setPlotterLabelY(HPLOTTER plotter, const char* labelY)
{
	if(fp_setPlotterLabelYA == NULL)
		throw 0;
	fp_setPlotterLabelYA(plotter, labelY);
}

void __cdecl setPlotterLabelY2(HPLOTTER plotter, const char* label)
{
	if(fp_setPlotterLabelY2A == NULL)
		throw 0;
	fp_setPlotterLabelY2A(plotter, label);
}

void __cdecl setPlotTitle(HPLOTDATA plot, const char* title)
{
	if(fp_setPlotTitleA == NULL)
		throw 0;
	fp_setPlotTitleA(plot, title);
}

void __cdecl setPlotStyle(HPLOTDATA plot, char type)
{
	if(fp_setPlotStyle == NULL)
		throw 0;
	fp_setPlotStyle(plot, type);
}

void __cdecl showPlot(HPLOTTER plotter, const wchar_t* windowTitle)
{
	if(fp_showPlotW == NULL)
		throw 0;
	fp_showPlotW(plotter, windowTitle);
}

void __cdecl showPlot(HPLOTTER plotter, const char* windowTitle)
{
	if(fp_showPlotA == NULL)
		throw 0;
	fp_showPlotA(plotter, windowTitle);
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

ChartColor __cdecl colorFromName(const char* name)
{
	if(fp_colorFromNameA == NULL)
		throw 0;
	return fp_colorFromNameA(name);
}

ChartColor __cdecl colorFromName(const wchar_t* name)
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

void __cdecl plotXY(double* x, double* y, int length, const char* title, const char* labelX, const char* labelY)
{
	if(fp_plotXY == NULL)
		throw 0;
	fp_plotXY(x, y, length, title, labelX, labelY);
}

HPLOTDATA __cdecl createPlotDataY(double xstart, double xstep, double xend, functionY function)
{
	if(fp_createPlotDataY == NULL)
		throw 0;
	return fp_createPlotDataY(xstart, xstep, xend, function);
}

HPLOTDATA __cdecl createPlotDataXY(double xstart, double xstep, double xend, functionXY function)
{
	if(fp_createPlotDataXY == NULL)
		throw 0;
	return fp_createPlotDataXY(xstart, xstep, xend, function);
}

uint8_t* __cdecl renderPlotterToImageBuffer(HPLOTTER plotter, int width, int height, int* size, ChartImageFormat format)
{
	if(fp_renderPlotterToImageBuffer == NULL)
		throw 0;
	return fp_renderPlotterToImageBuffer(plotter, width, height, size, format);
}

void __cdecl deleteImageBuffer(uint8_t* imageBuffer)
{
	if(fp_deleteImageBuffer == NULL)
		throw 0;
	fp_deleteImageBuffer(imageBuffer);
}

void __cdecl setPlotterTitleFont(HPLOTTER plotter, const wchar_t* fontFamily, float fontSize, ChartFontFlags flags)
{
	if(fp_setPlotterTitleFontW == NULL)
		throw 0;
	fp_setPlotterTitleFontW(plotter, fontFamily, fontSize, flags);
}

void __cdecl setPlotterFont(HPLOTTER plotter, const wchar_t* fontFamily, float fontSize, ChartFontFlags flags)
{
	if(fp_setPlotterFontW == NULL)
		throw 0;
	fp_setPlotterFontW(plotter, fontFamily, fontSize, flags);
}

void __cdecl setPlotterLegendFont(HPLOTTER plotter, const wchar_t* fontFamily, float fontSize, ChartFontFlags flags)
{
	if(fp_setPlotterLegendFontW == NULL)
		throw 0;
	fp_setPlotterLegendFontW(plotter, fontFamily, fontSize, flags);
}

void __cdecl setPlotterTitleFont(HPLOTTER plotter, const char* fontFamily, float fontSize, ChartFontFlags flags)
{
	if(fp_setPlotterTitleFontA == NULL)
		throw 0;
	fp_setPlotterTitleFontA(plotter, fontFamily, fontSize, flags);
}

void __cdecl setPlotterFont(HPLOTTER plotter, const char* fontFamily, float fontSize, ChartFontFlags flags)
{
	if(fp_setPlotterFontA == NULL)
		throw 0;
	fp_setPlotterFontA(plotter, fontFamily, fontSize, flags);
}

void __cdecl setPlotterLegendFont(HPLOTTER plotter, const char* fontFamily, float fontSize, ChartFontFlags flags)
{
	if(fp_setPlotterLegendFontA == NULL)
		throw 0;
	fp_setPlotterLegendFontA(plotter, fontFamily, fontSize, flags);
}

uint32_t __cdecl getPlotDataLength(HPLOTDATA plot)
{
	if(fp_getPlotDataLength == NULL)
		throw 0;
	return fp_getPlotDataLength(plot);
}

void __cdecl getPlotDataX(HPLOTDATA plot, double* buffer)
{
	if(fp_getPlotDataX == NULL)
		throw 0;
	fp_getPlotDataX(plot, buffer);
}

void __cdecl getPlotDataY(HPLOTDATA plot, double* buffer)
{
	if(fp_getPlotDataY == NULL)
		throw 0;
	fp_getPlotDataY(plot, buffer);
}

int __cdecl showPlotAsync(HPLOTTER plotter, const wchar_t* title)
{
	if(fp_showPlotAsync == NULL)
		throw 0;
	return fp_showPlotAsync(plotter, title);
}

void __cdecl joinWindow(int id)
{
	if(fp_joinWindow == NULL)
		throw 0;
	fp_joinWindow(id);
}

void __cdecl plotWindowRedraw(int id)
{
	if(fp_plotWindowRedraw == NULL)
		throw 0;
	fp_plotWindowRedraw(id);
}

void __cdecl showTable(HPLOTDATA plot)
{
	if(fp_showTable == NULL)
		throw 0;
	fp_showTable(plot);
}

#ifdef __cplusplus
}
#endif
