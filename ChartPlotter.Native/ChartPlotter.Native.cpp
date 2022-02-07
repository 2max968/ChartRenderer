#include "pch.h"
#include <cmath>
#include <inttypes.h>
#include <map>
#include <Windows.h>
#include <vcclr.h>
#include <iostream>

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace System::Windows::Forms;
using namespace ChartPlotter;
using namespace ChartPlotter::WinForms;

#define DLLEXPORT(type)             extern "C" _declspec(dllexport) type _cdecl
typedef gcroot<XYPlotRenderer^>* HPLOTTER;
typedef gcroot<XYPlotData^>* HPLOTDATA;

DLLEXPORT(HPLOTTER) createPlotter()
{
    XYPlotRenderer^ plot = gcnew XYPlotRenderer();
    auto* reference = new gcroot< XYPlotRenderer^>(plot);
    return reference;
}

DLLEXPORT(void) deletePlotter(HPLOTTER plotter)
{
    delete plotter;
}

DLLEXPORT(HBITMAP) renderPlot(HPLOTTER plotter)
{
    auto bmp = (*plotter)->RenderChart(600, 400);
    return (HBITMAP)bmp->GetHbitmap().ToPointer();
}

DLLEXPORT(void) showPlot(HPLOTTER plotter)
{
    auto frm = gcnew Form();
    auto plot = gcnew XYPlot(*plotter);
    frm->ClientSize = System::Drawing::Size(600, 400);
    plot->Dock = DockStyle::Fill;
    frm->Controls->Add(plot);
    Application::Run(frm);
}

DLLEXPORT(HPLOTDATA) createPlotData(double* x, double* y, int length)
{
    auto _x = gcnew array<double>(length);
    auto _y = gcnew array<double>(length);
    for (int i = 0; i < length; i++)
    {
        _x[i] = x[i];
        _y[i] = y[i];
    }
    auto data = gcnew XYPlotData(_x, _y);
    return new gcroot<XYPlotData^>(data);
}

DLLEXPORT(void) deletePlotData(HPLOTDATA data)
{
    delete data;
}

DLLEXPORT(void) addPlotData(HPLOTTER plotter, const HPLOTDATA data)
{
    (*plotter)->AddPlot(*data);
}

DLLEXPORT(void) setTitleW(HPLOTTER plotter, const wchar_t* title)
{
    (*plotter)->Title = gcnew String(title);
}

DLLEXPORT(void) setTitleA(HPLOTTER plotter, const char* title)
{
    (*plotter)->Title = gcnew String(title);
}

DLLEXPORT(void) setAxisLabelW(HPLOTTER plotter, const wchar_t* xlabel, const wchar_t* ylabel)
{
    (*plotter)->LabelX = gcnew String(xlabel);
    (*plotter)->LabelY1 = gcnew String(ylabel);
}

DLLEXPORT(void) setAxisLabelA(HPLOTTER plotter, const char* xlabel, const char* ylabel)
{
    (*plotter)->LabelX = gcnew String(xlabel);
    (*plotter)->LabelY1 = gcnew String(ylabel);
}

DLLEXPORT(void) setAxisRangeX(HPLOTTER plotter, double min, double max)
{
    auto range = gcnew ChartRange(min, max);
    (*plotter)->RangeX = range;
}

DLLEXPORT(void) setAxisRangeY1(HPLOTTER plotter, double min, double max)
{
    auto range = gcnew ChartRange(min, max);
    (*plotter)->RangeY1 = range;
}

DLLEXPORT(void) setAxisRangeY2(HPLOTTER plotter, double min, double max)
{
    auto range = gcnew ChartRange(min, max);
    (*plotter)->RangeY2 = range;
}

DLLEXPORT(void) setDataLabelW(HPLOTDATA data, const wchar_t* label)
{
    (*data)->DataTitle = gcnew String(label);
}


#ifdef DLLRUN
int main(array<System::String^>^ args)
{
    double x[628];
    double y[628];
    for (int i = 0; i < 628; i++)
    {
        x[i] = i / 100.0;
        y[i] = sin(x[i]);
    }

    auto plotter = createPlotter();
    auto plot = createPlotData(x, y, 628);
    addPlotData(plotter, plot);
    auto bmp = renderPlot(plotter);
    setTitleA(plotter, "Hallo Welt");
    showPlot(plotter);
    deletePlotter(plotter);
    deletePlotData(plot);
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