using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol.Window;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol
{
    public class FWDecision : FWSymbol
    {
        public FWDecision() : base()
        {
            Name = "Decision";

            Description = "조건";

            SymbolShape = new Polygon
            {
                Points = new PointCollection(new Point[]
                        {
                            new Point(20,0),
                            new Point(40,10),
                            new Point(20,20),
                            new Point(0,10)
                        }),
                Fill = Brushes.LightBlue
            };

            SymbolActionWindow = new FWDecisionWindow(this);

            EndConnector = new FWSymbol[2] { null, null };
        }

        private string conditionalExpression;
        public string ConditionalExpression
        {
            get { return conditionalExpression; }
            set
            {
                conditionalExpression = value;
                OnPropertyChanged("ConditionalExpression");
            }
        }

        public new FWSymbol[] EndConnector
        {
            get;
            set;
        }

        public FWSymbol DecisionTrue
        {
            get
            {
                if (EndConnector == null)
                {
                    return null;
                }

                if (EndConnector[0] != null)
                {
                    if (EndConnector[0] is FWLine fwLine)
                    {
                        if (fwLine.FlowTypeTrueOrFalse)
                        {
                            return EndConnector[0];
                        }
                    }
                }

                if (EndConnector[1] != null)
                {
                    if (EndConnector[1] is FWLine fwLine)
                    {
                        if (fwLine.FlowTypeTrueOrFalse)
                        {
                            return EndConnector[1];
                        }
                    }
                }

                return null;
            }
        }

        public FWSymbol DecisionFalse
        {
            get
            {
                if (EndConnector == null)
                {
                    return null;
                }

                if (EndConnector[0] != null)
                {
                    if (EndConnector[0] is FWLine fwLine)
                    {
                        if (!fwLine.FlowTypeTrueOrFalse)
                        {
                            return EndConnector[0];
                        }
                    }
                }

                if (EndConnector[1] != null)
                {
                    if (EndConnector[1] is FWLine fwLine)
                    {
                        if (!fwLine.FlowTypeTrueOrFalse)
                        {
                            return EndConnector[1];
                        }
                    }
                }

                return null;
            }
        }
    }
}
