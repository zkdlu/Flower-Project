using System.Windows.Controls;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol.Window
{
    /// <summary>
    /// FWDecisionWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FWDecisionWindow : UserControl
    {
        public FWDecisionWindow(FWSymbol fwSymbol)
        {
            InitializeComponent();

            this.DataContext = fwSymbol;
        }
    }
}
