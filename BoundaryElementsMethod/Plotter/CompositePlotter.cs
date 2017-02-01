using System.Collections.Generic;
using System.IO;
using System.Linq;
using BEM.Common;

namespace BEM.Plotter
{
    public class CompositePlotter
    {
        private const string twoDimensionPlotScript = @"
set ylabel 'Tr(u)'
set xlabel 'x'
plot {0}";
        private string lineTemplate = "'{0}' with lines";

        public List<string> FileNames { get; private set; }

        public CompositePlotter()
        {
            FileNames = new List<string>();
        }

        public void Plot()
        {
            var formatedFileNames = string.Join(",", FileNames.Select(f => string.Format(lineTemplate, f)).ToArray());
            Writer.Output(string.Format(twoDimensionPlotScript, formatedFileNames), Path.Combine(PlotterTwoSphere.Directory, "TwoDimensionPlotScript.plt"));
        }
    }

    public class CopyOfCompositePlotter
    {
        private const string twoDimensionPlotScript = @"
set ylabel 'Tr(u)'
set xlabel 'x'
plot {0}";
        private string lineTemplate = "'{0}' with lines";

        public List<string> FileNames { get; private set; }

        public CopyOfCompositePlotter()
        {
            FileNames = new List<string>();
        }

        public void Plot()
        {
            var formatedFileNames = string.Join(",", FileNames.Select(f => string.Format(lineTemplate, f)).ToArray());
            Writer.Output(string.Format(twoDimensionPlotScript, formatedFileNames), Path.Combine(PlotterTwoSphere.Directory, "TwoDimensionPlotScript.plt"));
        }
    }
}