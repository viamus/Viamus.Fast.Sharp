using Syncfusion.Maui.Toolkit.Charts;

namespace Viamus.Fast.Sharp.Dispatcher.Viewer.Pages.Controls;

public class LegendExt : ChartLegend
{
    protected override double GetMaximumSizeCoefficient()
    {
        return 0.5;
    }
}