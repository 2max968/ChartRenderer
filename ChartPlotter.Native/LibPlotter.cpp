#define CHARTRENDERER_FULLTYPES_
#include "LibPlotter.h"
#include <cmath>

using namespace System;
using namespace ChartPlotter;
using namespace ChartPlotter::WinForms;
using namespace System::Drawing;
using namespace System::Drawing::Imaging;
using namespace System::Runtime::InteropServices;
using namespace System::Windows::Forms;

std::vector<HPLOTTER> plotterHandles;
std::vector<HPLOTDATA> dataHandles;

bool checkHandle(HPLOTTER plotter)
{
	for (auto it = plotterHandles.begin(); it != plotterHandles.end(); it++)
		if (*it == plotter)
			return true;
	return false;
}

bool checkHandle(HPLOTDATA plot)
{
	for (auto it = dataHandles.begin(); it != dataHandles.end(); it++)
		if (*it == plot)
			return true;
	return false;
}

DLLEXPORT(HPLOTTER) createPlotter()
{
	XYPlotRenderer^ plotter = gcnew XYPlotRenderer();
	HPLOTTER handle = new PLOTTER(plotter);
	plotterHandles.push_back(handle);
	return handle;
}

DLLEXPORT(void) deletePlotter(HPLOTTER plotter)
{
	if (!checkHandle(plotter))
		return;
	for (auto it = plotterHandles.begin(); it != plotterHandles.end(); it++) 
	{
		if (*it == plotter) 
		{
			plotterHandles.erase(it);
			break;
		}
	}
	delete plotter;
}

DLLEXPORT(void) renderPlotter(HPLOTTER plotter, int width, int height, uint8_t* bitmap, int bitmapSize)
{
	if (!checkHandle(plotter))
		INVALID_HANDLE_VALUE;
	Bitmap^ bmp = (*plotter)->RenderChart(width, height);
	auto data = bmp->LockBits(System::Drawing::Rectangle(0, 0, width, height), ImageLockMode::ReadOnly, PixelFormat::Format24bppRgb);
	memcpy_s(bitmap, bitmapSize, data->Scan0.ToPointer(), height * data->Stride);
	bmp->UnlockBits(data);
}

void renderPlotterToFile(HPLOTTER plotter, String^ filename, int width, int height)
{
	if (!checkHandle(plotter))
		return;
	Bitmap^ bmp = (*plotter)->RenderChart(width, height);
	bmp->Save(filename);
	delete bmp;
}

DLLEXPORT(void) renderPlotterToFileW(HPLOTTER plotter, const wchar_t* filename, int width, int height)
{
	renderPlotterToFile(plotter, gcnew String(filename), width, height);
}

DLLEXPORT(void) renderPlotterToFileA(HPLOTTER plotter, const char* filename, int width, int height)
{
	renderPlotterToFile(plotter, gcnew String(filename), width, height);
}

DLLEXPORT(HPLOTDATA) createPlotData(const double* x, const double* y, int length)
{
	auto _x = gcnew array<double>(length);
	auto _y = gcnew array<double>(length);
	for (int i = 0; i < length; i++)
	{
		_x[i] = x[i];
		_y[i] = y[i];
	}
	//Marshal::Copy(IntPtr(x), _x, 0, length);
	//Marshal::Copy(IntPtr(y), _y, 0, length);
	XYPlotData^ data = gcnew XYPlotData(_x, _y);
	HPLOTDATA handle = new PLOTDATA(data);
	dataHandles.push_back(handle);
	return handle;
}

DLLEXPORT(void) deletePlotData(HPLOTDATA plot)
{
	for (auto it = dataHandles.begin(); it != dataHandles.end(); it++)
	{
		if (*it == plot)
		{
			dataHandles.erase(it);
			break;
		}
	}
	delete plot;
}

DLLEXPORT(void) clearAllPlotData(HPLOTTER plotter)
{
	if (!checkHandle(plotter))
		return;
	(*plotter)->ClearPlots();
}

DLLEXPORT(void) addPlotData(HPLOTTER plotter, HPLOTDATA plot)
{
	if (!checkHandle(plotter))
		return;
	if (!checkHandle(plot))
		return;
	(*plotter)->AddPlot(*plot);
}

DLLEXPORT(void) setPlotterTitleW(HPLOTTER plotter, const wchar_t* title)
{
	if (!checkHandle(plotter))
		return;
	auto str = gcnew String(title);
	(*plotter)->Title = str;
}

DLLEXPORT(void) setPlotterTitleA(HPLOTTER plotter, const char* title)
{
	if (!checkHandle(plotter))
		return;
	auto str = gcnew String(title);
	(*plotter)->Title = str;
}

DLLEXPORT(void) setPlotterLabelXW(HPLOTTER plotter, const wchar_t* labelX)
{
	if (!checkHandle(plotter))
		return;
	auto lx = gcnew String(labelX);
	(*plotter)->LabelX = lx;
}

DLLEXPORT(void) setPlotterLabelXA(HPLOTTER plotter, const char* labelX)
{
	if (!checkHandle(plotter))
		return;
	auto lx = gcnew String(labelX);
	(*plotter)->LabelX = lx;
}

DLLEXPORT(void) setPlotterLabelYW(HPLOTTER plotter, const wchar_t* labelY)
{
	if (!checkHandle(plotter))
		return;
	auto ly = gcnew String(labelY);
	(*plotter)->LabelY1 = ly;
}

DLLEXPORT(void) setPlotterLabelYA(HPLOTTER plotter, const char* labelY)
{
	if (!checkHandle(plotter))
		return;
	auto ly = gcnew String(labelY);
	(*plotter)->LabelY1 = ly;
}

DLLEXPORT(void) setPlotterLabelY2W(HPLOTTER plotter, const wchar_t* label)
{
	if (!checkHandle(plotter))
		return;
	auto str = gcnew String(label);
	(*plotter)->LabelY2 = str;
}

DLLEXPORT(void) setPlotterLabelY2A(HPLOTTER plotter, const char* label)
{
	if (!checkHandle(plotter))
		return;
	auto str = gcnew String(label);
	(*plotter)->LabelY2 = str;
}

DLLEXPORT(void) setPlotTitleW(HPLOTDATA plot, const wchar_t* title)
{
	if (!checkHandle(plot))
		return;
	auto str = gcnew String(title);
	(*plot)->DataTitle = str;
}

DLLEXPORT(void) setPlotTitleA(HPLOTDATA plot, const char* title)
{
	if (!checkHandle(plot))
		return;
	auto str = gcnew String(title);
	(*plot)->DataTitle = str;
}

DLLEXPORT(void) setPlotColor(HPLOTDATA plot, ChartColor color)
{
	if (!checkHandle(plot))
		return;
	(*plot)->DataColor = Color::FromArgb(color.r, color.g, color.b);
}

DLLEXPORT(void) setPlotStyle(HPLOTDATA plot, char type)
{
	if (!checkHandle(plot))
		return;
	(*plot)->SetStyle(type);
}

DLLEXPORT(void) showPlotW(HPLOTTER plotter, const wchar_t* windowTitle)
{
	String^ title = nullptr;
	if (windowTitle != NULL)
		title = gcnew String(windowTitle);
	if (checkHandle(plotter))
	{
		XYPlotWindow::ShowWindow(*plotter, title);
	}
	else
	{
		MessageBox::Show(nullptr, "Invalid Handle in plot", "Plotter", MessageBoxButtons::OK, MessageBoxIcon::Error);
	}
}

DLLEXPORT(void) showPlotA(HPLOTTER plotter, const char* windowTitle)
{
	String^ title = nullptr;
	if (windowTitle != NULL)
		title = gcnew String(windowTitle);
	if (checkHandle(plotter))
	{
		XYPlotWindow::ShowWindow(*plotter, title);
	}
	else
	{
		MessageBox::Show(nullptr, "Invalid Handle in plot", "Plotter", MessageBoxButtons::OK, MessageBoxIcon::Error);
	}
}

DLLEXPORT(void) setPlotIndex(HPLOTDATA plot, int index)
{
	(*plot)->SetPlotIndex(index);
}

DLLEXPORT(void) setPlotWidth(HPLOTDATA plot, float width)
{
	(*plot)->SetWidth(width);
}

DLLEXPORT(void) setPlotShowInLegend(HPLOTDATA plot, bool visible)
{
	if (visible)
		(*plot)->ShowInLegend();
	else
		(*plot)->HideFromLegend();
}

DLLEXPORT(void) setPlotterShowLegend(HPLOTTER plotter, bool visible)
{
	(*plotter)->ShowLegend = visible;
}

DLLEXPORT(void) setPlotterColor(HPLOTTER plotter, ChartColor foreground, ChartColor background)
{
	(*plotter)->ForegroundColor = Color::FromArgb(foreground.r, foreground.g, foreground.b);
	(*plotter)->BackgroundColor = Color::FromArgb(background.r, background.g, background.b);
}

DLLEXPORT(void) setPlotterRangeX(HPLOTTER plotter, double min, double max)
{
	(*plotter)->RangeX = gcnew ChartRange(min, max);
}

DLLEXPORT(void) setPlotterRangeY1(HPLOTTER plotter, double min, double max)
{
	(*plotter)->RangeY1 = gcnew ChartRange(min, max);
}

DLLEXPORT(void) setPlotterRangeY2(HPLOTTER plotter, double min, double max)
{
	(*plotter)->RangeY2 = gcnew ChartRange(min, max);
}

DLLEXPORT(ChartColor) colorFromRGB(unsigned char r, unsigned char g, unsigned char b)
{
	return { r, g, b };
}

ChartColor colorFromName(System::String^ name)
{
	auto color = ColorTranslator::FromHtml(name);
	return { color.R, color.G, color.B };
}

DLLEXPORT(ChartColor) colorFromNameA(const char* name)
{
	return colorFromName(gcnew String(name));
}

DLLEXPORT(ChartColor) colorFromNameW(const wchar_t* name)
{
	return colorFromName(gcnew String(name));
}

DLLEXPORT(void) plotAddPoint(HPLOTDATA plot, double x, double y)
{
	(*plot)->AddPoint(x, y);
}

DLLEXPORT(void) plotXY(double* x, double* y, int length, const char* title, const char* labelX, const char* labelY)
{
	HPLOTDATA plot = createPlotData(x, y, length);
	HPLOTTER plotter = createPlotter();
	setPlotterLabelXA(plotter, labelX);
	setPlotterLabelYA(plotter, labelY);
	setPlotterTitleA(plotter, title);
	setPlotterShowLegend(plotter, false);
	addPlotData(plotter, plot);
	showPlotW(plotter);
	deletePlotter(plotter);
	deletePlotData(plot);
}

DLLEXPORT(HPLOTDATA) createPlotDataY(double xstart, double xstep, double xend, functionY function)
{
	if (xend < xstart) throw 0;
	if (xstep <= 0) throw 0;

	int length = (int)ceil((xend - xstart) / xstep) + 1;
	double* x = new double[length];
	double* y = new double[length];
	for (int i = 0; i < length; i++)
	{
		x[i] = xstart + i * xstep;
		y[i] = function(x[i]);
	}

	HPLOTDATA plot = createPlotData(x, y, length);
	delete[] x;
	delete[] y;
	return plot;
}

DLLEXPORT(HPLOTDATA) createPlotDataXY(double tstart, double tstep, double tend, functionXY function)
{
	if (tend < tstart) throw 0;
	if (tstep <= 0) throw 0;

	int length = (int)ceil((tend - tstart) / tstep) + 1;
	double* x = new double[length];
	double* y = new double[length];
	for (int i = 0; i < length; i++)
	{
		double t = tstart + i * tstep;
		function(t, &x[i], &y[i]);
	}

	HPLOTDATA plot = createPlotData(x, y, length);
	delete[] x;
	delete[] y;
	return plot;
}