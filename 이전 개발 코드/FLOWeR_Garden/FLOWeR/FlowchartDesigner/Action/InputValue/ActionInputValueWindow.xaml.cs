using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol;
using FLOWeR_Garden_Ver02.FLOWeR.UIDesigner;
using FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Control;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Action.InputValue
{
    /// <summary>
    /// ActionInputValueWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ActionInputValueWindow : UserControl
    {
        public ActionInputValueWindow(FWProcess fwProcess)
        {
            InitializeComponent();

            this.DataContext = fwProcess;

            List<FWControl> fwControls = FlowerManager.Instance.FWControls;
            foreach (FWControl fwControl in fwControls)
            {
                cboxVarName.Items.Add(fwControl.Name);
                cboxType.Items.Add(fwControl);
            }

            List<FWSymbol> fwSymbols = FlowerManager.Instance.FWSymbols;
            foreach (FWSymbol fwSymbol in fwSymbols)
            {
                if (fwSymbol is FWReady fwReady)
                {
                    cboxVarName.Items.Add(fwReady.VarName);
                    cboxType.Items.Add(fwSymbol);
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

            cboxVarValue.Items.Clear();

            bool isEditable = selectedIndex != 0;

            if (isEditable)
            {
                var item = cboxType.SelectedItem;
                Type type = item.GetType();

                if (item is FWSymbol fwSymbol)
                {
                    cboxVarValue.Text = fwSymbol.ToString();
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
                                cboxVarValue.Items.Add(propertyInfo.Name);
                            }
                        }
                    }
                }
            }
        }

        private void cboxVarValue_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            int selectedIndex = cboxType.SelectedIndex;

            if (selectedIndex == -1)
            {
                return;
            }

            if (selectedIndex == 0)
            {
                string msg = cboxVarValue.Text;
                if (msg != null)
                {
                    if (!msg.StartsWith("\"") || !msg.EndsWith("\""))
                    {
                        cboxVarValue.Text = $"\"{cboxVarValue.Text}\"";
                    }
                }
            }
            else
            {
                var item = cboxType.SelectedItem;
                if (item is FWControl fwControl)
                {
                    string msg = cboxVarValue.Text;
                    if (msg != null)
                    {
                        cboxVarValue.Text = $"{item.ToString()}.{msg}";
                    }
                }
            }
        }
    }
}
