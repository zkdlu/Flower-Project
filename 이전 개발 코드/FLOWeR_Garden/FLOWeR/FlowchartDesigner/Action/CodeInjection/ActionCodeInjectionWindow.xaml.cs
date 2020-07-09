using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol;
using System.Windows.Controls;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Action.CodeInjection
{
    /// <summary>
    /// ActionCodeInjectionWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ActionCodeInjectionWindow : UserControl
    {
        public ActionCodeInjectionWindow(FWProcess fwProcess)
        {
            InitializeComponent();

            this.DataContext = fwProcess;
        }
    }
}
