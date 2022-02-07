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