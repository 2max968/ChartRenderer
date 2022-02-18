#ifndef _CHARTPLOTTER_H
#define _CHARTPLOTTER_H

#ifdef __cplusplus
extern "C++"{
#endif


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
	ChartFontFlags_Regular,
	ChartFontFlags_Bold,
	ChartFontFlags_Italic,
	ChartFontFlags_Underline,
	ChartFontFlags_Strikeout
} ChartFontFlags;

typedef enum : uint8_t {
	ChartImageFormat_Png,
	ChartImageFormat_Bmp,
	ChartImageFormat_Jpg,
	ChartImageFormat_RawRGB24
} ChartImageFormat;

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

typedef double (*functionY)(double x);
typedef void (*functionXY)(double t, double* x, double* y);

#define STYLE_LINE			'_'
#define STYLE_DASH			'-'
#define STYLE_CROSS			'X'
#define STYLE_CIRCLE		'O'
#define STYLE_DOT			'.'
#define STYLE_LINEANDDOT	'!'

#define CHART_NAN			((float)((1e+300 * 1e+300) * 0.0F))
#define RANGE_AUTO			CHART_NAN



int        __cdecl initChartPlotter();
/// <summary>
/// Creates a new plot renderer
/// </summary>
/// <returns>Handle to a plot renderer</returns>
HPLOTTER   __cdecl createPlotter();
/// <summary>
/// Marks a plot renderer for deletion
/// </summary>
/// <param name="plotter"></param>
/// <returns></returns>
void       __cdecl deletePlotter(HPLOTTER plotter);
/// <summary>
/// Render all plots in the plot renderer to a image file, the image format will be determined by the file extension
/// </summary>
/// <param name="plotter"></param>
/// <param name="filename"></param>
/// <param name="width"></param>
/// <param name="height"></param>
/// <returns></returns>
void       __cdecl renderPlotterToFile(HPLOTTER plotter, const wchar_t* filename, int width, int height);
void       __cdecl renderPlotterToFile(HPLOTTER plotter, const char* filename, int width, int height);
HPLOTDATA  __cdecl createPlotData(const double* x, const double* y, int length);
void       __cdecl deletePlotData(HPLOTDATA plot);
void       __cdecl clearAllPlotData(HPLOTTER plotter);
void       __cdecl addPlotData(HPLOTTER plotter, HPLOTDATA plot);
void       __cdecl setPlotterTitle(HPLOTTER plotter, const wchar_t* title);
void       __cdecl setPlotterLabelX(HPLOTTER plotter, const wchar_t* labelX);
void       __cdecl setPlotterLabelY(HPLOTTER plotter, const wchar_t* labelY);
void       __cdecl setPlotterLabelY2(HPLOTTER plotter, const wchar_t* label);
void       __cdecl setPlotTitle(HPLOTDATA plot, const wchar_t* title);
void       __cdecl setPlotColor(HPLOTDATA plot, ChartColor color);
void       __cdecl setPlotterTitle(HPLOTTER plotter, const char* title);
void       __cdecl setPlotterLabelX(HPLOTTER plotter, const char* labelX);
void       __cdecl setPlotterLabelY(HPLOTTER plotter, const char* labelY);
void       __cdecl setPlotterLabelY2(HPLOTTER plotter, const char* label);
void       __cdecl setPlotTitle(HPLOTDATA plot, const char* title);
void       __cdecl setPlotStyle(HPLOTDATA plot, char type);
void       __cdecl showPlot(HPLOTTER plotter, const wchar_t* windowTitle = NULL);
void       __cdecl showPlot(HPLOTTER plotter, const char* windowTitle);
void       __cdecl setPlotIndex(HPLOTDATA plot, int index);
void       __cdecl setPlotWidth(HPLOTDATA plot, float width);
void       __cdecl setPlotShowInLegend(HPLOTDATA plot, bool visible);
void       __cdecl setPlotterShowLegend(HPLOTTER plotter, bool visible);
void       __cdecl setPlotterColor(HPLOTTER plotter, ChartColor foreground, ChartColor background);
void       __cdecl setPlotterRangeX(HPLOTTER plotter, double min, double max);
void       __cdecl setPlotterRangeY1(HPLOTTER plotter, double min, double max);
void       __cdecl setPlotterRangeY2(HPLOTTER plotter, double min, double max);
ChartColor __cdecl colorFromRGB(unsigned char r, unsigned char g, unsigned char b);
ChartColor __cdecl colorFromName(const char* name);
ChartColor __cdecl colorFromName(const wchar_t* name);
void       __cdecl plotAddPoint(HPLOTDATA plot, double x, double y);
void       __cdecl plotXY(double* x, double* y, int length, const char* title = NULL, const char* labelX = NULL, const char* labelY = NULL);
HPLOTDATA  __cdecl createPlotDataY(double xstart, double xstep, double xend, functionY function);
HPLOTDATA  __cdecl createPlotDataXY(double xstart, double xstep, double xend, functionXY function);
/// <summary>
/// Renders the plot to an image buffer
/// </summary>
/// <param name="plotter">The plotter to render</param>
/// <param name="width">The width of the image to render</param>
/// <param name="height"The height of the image to render></param>
/// <param name="size">Outputs the size of the created buffer in bytes</param>
/// <param name="format">The format to save the image. RawRGB24 just saves the pixels in the array</param>
/// <returns>The pointer to a buffer containing the image data. This buffer has to be deleted with deleteImageBuffer</returns>
uint8_t*   __cdecl renderPlotterToImageBuffer(HPLOTTER plotter, int width, int height, int* size, ChartImageFormat format = ChartImageFormat_Png);
void       __cdecl deleteImageBuffer(uint8_t* imageBuffer);
void       __cdecl setPlotterTitleFont(HPLOTTER plotter, const wchar_t* fontFamily, float fontSize, ChartFontFlags flags = ChartFontFlags_Regular);
void       __cdecl setPlotterFont(HPLOTTER plotter, const wchar_t* fontFamily, float fontSize, ChartFontFlags flags = ChartFontFlags_Regular);
void       __cdecl setPlotterLegendFont(HPLOTTER plotter, const wchar_t* fontFamily, float fontSize, ChartFontFlags flags = ChartFontFlags_Regular);
void       __cdecl setPlotterTitleFont(HPLOTTER plotter, const char* fontFamily, float fontSize, ChartFontFlags flags = ChartFontFlags_Regular);
void       __cdecl setPlotterFont(HPLOTTER plotter, const char* fontFamily, float fontSize, ChartFontFlags flags = ChartFontFlags_Regular);
void       __cdecl setPlotterLegendFont(HPLOTTER plotter, const char* fontFamily, float fontSize, ChartFontFlags flags = ChartFontFlags_Regular);
uint32_t   __cdecl getPlotDataLength(HPLOTDATA plot);
void       __cdecl getPlotDataX(HPLOTDATA plot, double* buffer);
void       __cdecl getPlotDataY(HPLOTDATA plot, double* buffer);
int        __cdecl showPlotAsync(HPLOTTER plotter, const wchar_t* title = NULL);
void       __cdecl joinWindow(int id);
void       __cdecl plotWindowRedraw(int id);
void       __cdecl showTable(HPLOTDATA plot);

#ifdef __cplusplus
}
#endif

#endif //_CHARTPLOTTER_H
