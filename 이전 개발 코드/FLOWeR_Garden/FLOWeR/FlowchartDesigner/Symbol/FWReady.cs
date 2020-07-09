using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol.Window;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol
{
    public class FWReady : FWSymbol
    {
        public FWReady() : base()
        {
            Name = "Ready";

            Description = "준비";

            SymbolShape = new Polygon
            {
                Points = new PointCollection(new Point[]
                        {
                            new Point(10,0),
                            new Point(30,0),
                            new Point(40,10),
                            new Point(30,20),
                            new Point(10,20),
                            new Point(0,10)
                        }),
                Fill = Brushes.LightBlue
            };

            SymbolActionWindow = new FWReadyWindow(this);
        }

        private string varType;
        public string VarType
        {
            get { return varType; }
            set
            {
                varType = value;
                OnPropertyChanged("VarType");
            }
        }

        private string varName;
        public string VarName
        {
            get { return varName; }
            set
            {
                varName = value;
                OnPropertyChanged("VarName");
            }
        }

        public override string ToString()
        {
            return VarName;
        }
    }
}
