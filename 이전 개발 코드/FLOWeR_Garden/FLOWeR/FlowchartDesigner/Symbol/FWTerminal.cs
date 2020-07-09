using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol.Window;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol
{
    public class FWTerminal : FWSymbol
    {
        public FWTerminal() : base()
        {
            Name = "Terminal";

            Description = "시작/끝";

            SymbolShape = new Rectangle
            {
                RadiusX = 30,
                RadiusY = 30,
                Fill = Brushes.LightBlue
            };

            SymbolActionWindow = new FWTerminalWindow();
        }
    }
}
