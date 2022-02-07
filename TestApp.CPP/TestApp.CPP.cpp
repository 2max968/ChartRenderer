// TestApp.CPP.cpp : Diese Datei enthält die Funktion "main". Hier beginnt und endet die Ausführung des Programms.
//

#include <iostream>
#include "ChartPlotter.h"

int main()
{
    initChartPlotter();
    HPLOTTER plotter = createPlotter();
    //HPLOTDATA sine = createPlotData(NULL, NULL, 0);

    setPlotterColor(plotter, colorFromNameA("white"), colorFromNameA("black"));

    showPlotW(plotter);
}