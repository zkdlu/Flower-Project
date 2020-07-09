using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol;
using System.Windows;
using System.Windows.Controls;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Action
{
    /// <summary>
    /// ActionTool.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ActionTool : UserControl
    {
        public ActionTool()
        {
            InitializeComponent();
        }

        public Window DockedWindow
        {
            get;
            set;
        }

        public FWProcess SelectedAction
        {
            get;
            private set;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                SelectedAction = listBox.SelectedItem as FWProcess;

                FlowManager.Instance.FlowTool.Deselection();
            }
        }

        public void DeselectListBox()
        {
            lboxAction.SelectedIndex = -1;
            SelectedAction = null;
        }
    }
}
