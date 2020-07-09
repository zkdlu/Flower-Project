using FLOWeR_Garden_Ver02.DockablePane;
using FLOWeR_Garden_Ver02.Explorer;
using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner;
using FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Control;
using System.Windows;
using System.Windows.Controls;

namespace FLOWeR_Garden_Ver02.FLOWeR.UIDesigner
{
    /// <summary>
    /// FWEvent.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FWEvent : UserControl
    {
        public FWEvent()
        {
            InitializeComponent();
        }

        public Window DockedWindow
        {
            get;
            set;
        }

        private FWControl fwControl; 
        public FWControl TargetObject
        {
            get
            {
                return fwControl;
            }
            set
            {
                fwControl = value;

                OnChangedTargetControl();
            }
        }

        private void OnChangedTargetControl()
        {
            lboxEvent.ItemsSource = null;
            lboxEvent.Items.Clear();

            if (TargetObject != null)
            {
                lboxEvent.ItemsSource = TargetObject.FWEventList.Events;
            }
        }

        private void OnAddEventAtLostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void OnAddEventAtEnter(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (sender is TextBox textBox)
                {
                    AddEventFlow(textBox);
                }
            }
        }

        private void AddEventFlow(TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                return;
            }

            if (textBox.Tag is string eventName)
            {
                string eventValue = textBox.Text;

                foreach (var _flowchartDesigner in FlowManager.Instance.FlowchartDesigners)
                {
                    if (_flowchartDesigner.FlowName.Equals(eventName) && 
                        _flowchartDesigner.FlowValue.Equals(eventValue))
                    {
                        return;
                    }
                }

                FlowchartDesigner.FlowchartDesigner flowchartDesigner = new FlowchartDesigner.FlowchartDesigner(eventName, eventValue);

                FlowManager.Instance.FlowchartDesigners.Add(flowchartDesigner);

                FlowManager.Instance.ShowFlowDesigner(flowchartDesigner);

                ProjectManager.Instance.CreateFlowchart(flowchartDesigner);
            }
        }
    }
}
