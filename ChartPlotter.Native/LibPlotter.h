#pragma once

#include "Definitions.h"
#include <Windows.h>
#include <inttypes.h>
#include <vector>

#define DLLEXPORT(type)	extern "C" _declspec(dllexport) type _cdecl

bool checkHandle(HPLOTTER plotter);
bool checkHandle(HPLOTDATA plot);
/// <summary>
/// Creates a new plot renderer
/// </summary>
/// <returns>Handle to a plot renderer</returns>
DLLEXPORT(HPLOTTER) createPlotter();
/// <summary>
/// Marks a plot renderer for deletion
/// </summary>
/// <param name="plotter"></param>
/// <returns></returns>
DLLEXPORT(void) deletePlotter(HPLOTTER plotter);
/// <summary>
/// Render all plots in the plot renderer to a image file, the image format will be determined by the file extension
/// </summary>
/// <param name="plotter"></param>
/// <param name="filename"></param>
/// <param name="width"></param>
/// <param name="height"></param>
/// <returns></returns>
DLLEXPORT(void) renderPlotterToFileW(HPLOTTER plotter, const wchar_t* filename, int width, int height);
DLLEXPORT(void) renderPlotterToFileA(HPLOTTER plotter, const char* filename, int width, int height);
DLLEXPORT(HPLOTDATA) createPlotData(const double* x, const double* y, int length);
DLLEXPORT(void) deletePlotData(HPLOTDATA plot);
DLLEXPORT(void) clearAllPlotData(HPLOTTER plotter);
DLLEXPORT(void) addPlotData(HPLOTTER plotter, HPLOTDATA plot);
DLLEXPORT(void) setPlotterTitleW(HPLOTTER plotter, const wchar_t* title);
DLLEXPORT(void) setPlotterLabelXW(HPLOTTER plotter, const wchar_t* labelX);
DLLEXPORT(void) setPlotterLabelYW(HPLOTTER plotter, const wchar_t* labelY);
DLLEXPORT(void) setPlotterLabelY2W(HPLOTTER plotter, const wchar_t* label);
DLLEXPORT(void) setPlotTitleW(HPLOTDATA plot, const wchar_t* title);
DLLEXPORT(void) setPlotColor(HPLOTDATA plot, ChartColor color);
DLLEXPORT(void) setPlotterTitleA(HPLOTTER plotter, const char* title);
DLLEXPORT(void) setPlotterLabelXA(HPLOTTER plotter, const char* labelX);
DLLEXPORT(void) setPlotterLabelYA(HPLOTTER plotter, const char* labelY);
DLLEXPORT(void) setPlotterLabelY2A(HPLOTTER plotter, const char* label);
DLLEXPORT(void) setPlotTitleA(HPLOTDATA plot, const char* title);
DLLEXPORT(void) setPlotStyle(HPLOTDATA plot, char type);
DLLEXPORT(void) showPlotW(HPLOTTER plotter, const wchar_t* windowTitle = NULL);
DLLEXPORT(void) showPlotA(HPLOTTER plotter, const char* windowTitle);
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
DLLEXPORT(void) plotXY(double* x, double* y, int length, const char* title = NULL, const char* labelX = NULL, const char* labelY = NULL);
DLLEXPORT(HPLOTDATA) createPlotDataY(double xstart, double xstep, double xend, functionY function);
DLLEXPORT(HPLOTDATA) createPlotDataXY(double xstart, double xstep, double xend, functionXY function);
/// <summary>
/// Renders the plot to an image buffer
/// </summary>
/// <param name="plotter">The plotter to render</param>
/// <param name="width">The width of the image to render</param>
/// <param name="height"The height of the image to render></param>
/// <param name="size">Outputs the size of the created buffer in bytes</param>
/// <param name="format">The format to save the image. RawRGB24 just saves the pixels in the array</param>
/// <returns>The pointer to a buffer containing the image data. This buffer has to be deleted with deleteImageBuffer</returns>
DLLEXPORT(uint8_t*) renderPlotterToImageBuffer(HPLOTTER plotter, int width, int height, int* size, ChartImageFormat format = ChartImageFormat_Png);
DLLEXPORT(void) deleteImageBuffer(uint8_t* imageBuffer);
//DLLEXPORT(HWND) createPlotViewer(HPLOTTER plotter);
//DLLEXPORT(void) deletePlotViewer(HWND handle);
DLLEXPORT(void) setPlotterTitleFontW(HPLOTTER plotter, const wchar_t* fontFamily, float fontSize, ChartFontFlags flags = ChartFontFlags_Regular);
DLLEXPORT(void) setPlotterFontW(HPLOTTER plotter, const wchar_t* fontFamily, float fontSize, ChartFontFlags flags = ChartFontFlags_Regular);
DLLEXPORT(void) setPlotterLegendFontW(HPLOTTER plotter, const wchar_t* fontFamily, float fontSize, ChartFontFlags flags = ChartFontFlags_Regular);
DLLEXPORT(void) setPlotterTitleFontA(HPLOTTER plotter, const char* fontFamily, float fontSize, ChartFontFlags flags = ChartFontFlags_Regular);
DLLEXPORT(void) setPlotterFontA(HPLOTTER plotter, const char* fontFamily, float fontSize, ChartFontFlags flags = ChartFontFlags_Regular);
DLLEXPORT(void) setPlotterLegendFontA(HPLOTTER plotter, const char* fontFamily, float fontSize, ChartFontFlags flags = ChartFontFlags_Regular);
DLLEXPORT(uint32_t) getPlotDataLength(HPLOTDATA plot);
DLLEXPORT(void) getPlotDataX(HPLOTDATA plot, double* buffer);
DLLEXPORT(void) getPlotDataY(HPLOTDATA plot, double* buffer);
DLLEXPORT(int) showPlotAsync(HPLOTTER plotter, const wchar_t* title = NULL);
DLLEXPORT(void) joinWindow(int id);
DLLEXPORT(void) plotWindowRedraw(int id);