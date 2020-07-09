using System.Windows.Controls;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol.Window
{
    /// <summary>
    /// FWReadyWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FWReadyWindow : UserControl
    {
        public FWReadyWindow(FWSymbol fwSymbol)
        {
            InitializeComponent();

            this.DataContext = fwSymbol;
        }
    }
}
