#include <Windows.h>
#include <msclr/gcroot.h>
#include <inttypes.h>
#include <vector>

using namespace System;
using namespace ChartPlotter;
using namespace ChartPlotter::WinForms;
using namespace System::Drawing;
using namespace System::Runtime::InteropServices;
using namespace System::Windows::Forms;

#define DLLEXPORT(type)		extern "C" _declspec(dllexport) type _cdecl
typedef msclr::gcroot<XYPlotRenderer^> PLOTTER;
typedef PLOTTER* HPLOTTER;
typedef msclr::gcroot<XYPlotData^> PLOTDATA;
typedef PLOTDATA* HPLOTDATA;

std::vector<HPLOTTER> plotterHandles;
std::vector<HPLOTDATA> dataHandles;

DLLEXPORT(HPLOTTER) createPlotter()
{
	XYPlotRenderer^ plotter = gcnew XYPlotRenderer();
	return new PLOTTER(plotter);
}

DLLEXPORT(void) deletePlotter(HPLOTTER plotter)
{
	delete plotter;
}

DLLEXPORT(HBITMAP) renderPlot(HPLOTTER plotter, int width, int height)
{
	Bitmap^ bmp = (*plotter)->RenderChart(width, height);
	return (HBITMAP)bmp->GetHbitmap().ToPointer();
}

DLLEXPORT(void) renderPlotToW(HPLOTTER plotter, const wchar_t* filename, int width, int height)
{
	Bitmap^ bmp = (*plotter)->RenderChart(width, height);
	bmp->Save(gcnew String(filename));
	delete bmp;
}

DLLEXPORT(HPLOTDATA) createPlotData(double* x, double* y, int length)
{
	auto _x = gcnew array<double>(length);
	auto _y = gcnew array<double>(length);
	Marshal::Copy(IntPtr(x), _x, 0, length);
	Marshal::Copy(IntPtr(y), _y, 0, length);
	XYPlotData^ data = gcnew XYPlotData(_x, _y);
	return new PLOTDATA(data);
}

DLLEXPORT(void) deletePlotData(HPLOTDATA plot)
{
	delete plot;
}

DLLEXPORT(void) clearAllPlotData(HPLOTTER plotter)
{
	(*plotter)->ClearPlots();
}

DLLEXPORT(void) addPlotData(HPLOTTER plotter, HPLOTDATA plot)
{
	(*plotter)->AddPlot(*plot);
}

DLLEXPORT(void) setTitleW(HPLOTTER plotter, const wchar_t* title)
{
	auto str = gcnew String(title);
	(*plotter)->Title = str;
}

DLLEXPORT(void) setPlotLabelW(HPLOTTER plotter, const wchar_t* labelX, const wchar_t* labelY)
{
	auto lx = gcnew String(labelX);
	(*plotter)->LabelX = lx;
	auto ly = gcnew String(labelY);
	(*plotter)->LabelY1 = ly;
}

DLLEXPORT(void) setPlotLabelY2W(HPLOTTER plotter, const wchar_t* label)
{
	auto str = gcnew String(label);
	(*plotter)->LabelY2 = str;
}

DLLEXPORT(void) setPlotTitle(HPLOTDATA plot, const wchar_t* title)
{
	auto str = gcnew String(title);
	(*plot)->DataTitle = str;
}

DLLEXPORT(void) setPlotColorA(HPLOTDATA plot, const char* color)
{
	auto str = gcnew String(color);
	auto rgb = ColorTranslator::FromHtml(str);
	(*plot)->DataColor = rgb;
}

DLLEXPORT(void) setPlotColor(HPLOTDATA plot, uint8_t r, uint8_t g, uint8_t b)
{
	auto color = Color::FromArgb(r, g, b);
	(*plot)->DataColor = color;
}

DLLEXPORT(void) setPlotStyle(HPLOTDATA plot, char type)
{
	(*plot)->SetStyle(type);
}

DLLEXPORT(void) showPlot(HPLOTTER plotter, const wchar_t* windowTitle = NULL)
{
	auto frm = gcnew Form();
	frm->ClientSize = Size(600, 400);
	auto plotControl = gcnew XYPlot(*plotter);
	plotControl->Dock = DockStyle::Fill;
	frm->Controls->Add(plotControl);
	if (windowTitle != NULL)
	{
		frm->Text = gcnew String(windowTitle);
	}
	else
	{
		frm->Text = "Plot - " + (*plotter)->Title;
	}
	Application::Run(frm);
}

#ifdef DLLRUN
int main()
{
	double x[628];
	double y[628];
	for (int i = 0; i < 628; i++)
	{
		x[i] = i / 100.0;
		y[i] = Math::Sin(x[i]);
	}

	HPLOTTER plotter = createPlotter();
	HPLOTDATA sine = createPlotData(x, y, 628);
	addPlotData(plotter, sine);
	setTitleW(plotter, L"Sinus");
	setPlotLabelW(plotter, L"time / s", NULL);
	setPlotTitle(sine, L"useless info");
	setPlotColorA(sine, "orange");
	showPlot(plotter);
}
#else
BOOL WINAPI DllMain(
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
}
#endif