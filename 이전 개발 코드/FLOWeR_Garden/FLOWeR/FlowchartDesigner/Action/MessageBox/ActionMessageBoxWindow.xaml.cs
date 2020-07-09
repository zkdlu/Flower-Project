using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol;
using FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Control;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Action.MessageBox
{
    /// <summary>
    /// ActionMessageBoxWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ActionMessageBoxWindow : UserControl
    {
        public ActionMessageBoxWindow(FWProcess fwProcess)
        {
            InitializeComponent();

            this.DataContext = fwProcess;

            List<FWSymbol> fwSymbols = FlowerManager.Instance.FWSymbols;
            foreach (FWSymbol fwSymbol in fwSymbols)
            {
                if (fwSymbol is FWReady fwReady)
                {
                    cboxType.Items.Add(fwReady);
                }
            }

            List<FWControl> fwControls = FlowerManager.Instance.FWControls;
            foreach (FWControl fwControl in fwControls)
            {
                cboxType.Items.Add(fwControl);
            }
        }

        private void OnTypeChagned(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = cboxType.SelectedIndex;

            if (selectedIndex == -1)
            {
                return;
            }

            cboxMsg.Items.Clear();

            bool isEditable = selectedIndex != 0;

            if (isEditable)
            {
                var item = cboxType.SelectedItem;
                Type type = item.GetType();

                if (item is FWSymbol fwSymbol)
                {
                    cboxMsg.Text = fwSymbol.ToString();
                }
                else if (item is FWControl)
                {
                    PropertyInfo[] propertyInfos = type.GetProperties();

                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        object[] attributes = propertyInfo.GetCustomAttributes(true);
                        foreach (object attribute in attributes)
                        {
                            if (attribute is PropertyAttribute)
                            {
                                cboxMsg.Items.Add(propertyInfo.Name);
                            }
                        }
                    }
                }
            }
        }

        private void cboxMsg_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            int selectedIndex = cboxType.SelectedIndex;

            if (selectedIndex == -1)
            {
                return;
            }

            if (selectedIndex == 0)
            {
                string msg = cboxMsg.Text;
                if (msg != null)
                {
                    if (!msg.StartsWith("\"") || !msg.EndsWith("\""))
                    {
                        cboxMsg.Text = $"\"{cboxMsg.Text}\"";
                    }
                }
            }
            else
            {
                var item = cboxType.SelectedItem;
                if (item is FWControl fwControl)
                {
                    string msg = cboxMsg.Text;
                    if (msg != null)
                    {
                        cboxMsg.Text = $"{item.ToString()}.{msg}";
                    }
                }
            }
        }
    }
}
