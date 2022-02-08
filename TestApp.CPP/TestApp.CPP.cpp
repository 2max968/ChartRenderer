// TestApp.CPP.cpp : Diese Datei enthält die Funktion "main". Hier beginnt und endet die Ausführung des Programms.
//

#include <iostream>
#include "ChartPlotter.h"
#include <vector>
#include <cmath>
#include <exception>

int main()
{
    std::vector<double> x;
    std::vector<double> y;

    for (double t = 0; t < 3.1415 * 2; t+=.01)
    {
        x.push_back(cos(t));
        y.push_back(sin(t));
    }

    initChartPlotter();
    HPLOTDATA circle = createPlotData(x.data(), y.data(), x.size());
    HPLOTTER plotter = createPlotter();

    setPlotterColor(plotter, colorFromNameA("white"), colorFromNameA("black"));
    addPlotData(plotter, circle);

    showPlotW(plotter);
}