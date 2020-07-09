using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol;
using FLOWeR_Garden_Ver02.FLOWeR.UIDesigner;
using FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Control;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Action.ListBoxs
{
    /// <summary>
    /// ActionListBoxAddWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ActionListBoxAddWindow : UserControl
    {
        public ActionListBoxAddWindow(FWProcess fwProcess)
        {
            InitializeComponent();

            this.DataContext = fwProcess;

            List<FWControl> fwControls = UIManager.Instance.FlowerUIDesigner.FWControlList;
            foreach (FWControl fwControl in fwControls)
            {
                if (fwControl is FWListBox fwListBox)
                {
                    cboxListBoxName.Items.Add(fwListBox.Name);
                }
                cboxType.Items.Add(fwControl);
            }

            List<FWSymbol> fwSymbols = FlowerManager.Instance.FWSymbols;
            foreach (FWSymbol fwSymbol in fwSymbols)
            {
                if (fwSymbol is FWReady fwReady)
                {
                    cboxType.Items.Add(fwReady);
                }
            }
        }

        private void OnTypeChagned(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = cboxType.SelectedIndex;

            if (selectedIndex == -1)
            {
                return;
            }

            cboxListBoxItem.Items.Clear();

            bool isEditable = selectedIndex != 0;

            if (isEditable)
            {
                var item = cboxType.SelectedItem;
                Type type = item.GetType();

                if (item is FWSymbol fwSymbol)
                {
                    cboxListBoxItem.Text = fwSymbol.ToString();
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
                                cboxListBoxItem.Items.Add(propertyInfo.Name);
                            }
                        }
                    }
                }
            }
        }

        private void cboxListBoxItem_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            int selectedIndex = cboxType.SelectedIndex;

            if (selectedIndex == -1)
            {
                return;
            }

            if (selectedIndex == 0)
            {
                string msg = cboxListBoxItem.Text;
                if (msg != null)
                {
                    if (!msg.StartsWith("\"") || !msg.EndsWith("\""))
                    {
                        cboxListBoxItem.Text = $"\"{cboxListBoxItem.Text}\"";
                    }
                }
            }
            else
            {
                var item = cboxType.SelectedItem;
                if (item is FWControl fwControl)
                {
                    string msg = cboxListBoxItem.Text;
                    if (msg != null)
                    {
                        cboxListBoxItem.Text = $"{item.ToString()}.{msg}";
                    }
                }
            }
        }
    }
}
