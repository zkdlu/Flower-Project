using System.Windows.Media;
using System.Windows.Shapes;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol
{
    public class FWProcess : FWSymbol
    {
        public FWProcess() : base()
        {
            Name = "Process";

            SymbolShape = new Rectangle
            {
                Fill = Brushes.LightBlue
            };
        }
    }
}
