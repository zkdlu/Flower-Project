using FLOWeR_Garden_Ver02.Explorer;
using FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Component;
using FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FLOWeR_Garden_Ver02.FLOWeR.UIDesigner
{
    /// <summary>
    /// UIDesigner.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UIDesigner : UserControl
    {
        public UIDesigner()
        {
            InitializeComponent();

            FWControlList = new List<FWControl>();

            string upperDirectory = string.Format("{0}/{1}", ProjectManager.Instance.ProjectPath, ProjectManager.Instance.ProjectName);

            FilePath = string.Format("{0}/{1}/{2}.ud", upperDirectory, "UI Designer", "UI Designer");
        }

        public string FilePath
        {
            get;
            private set;
        }

        public Window DockedWindow
        {
            get;
            set;
        }

        public List<FWControl> FWControlList
        {
            get;
            private set;
        }

        public void ChangeBackgroundImage(BitmapImage bitmap)
        {
            imageBackground.Stretch = Stretch.Fill;
            imageBackground.ImageSource = bitmap;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Type type = UIManager.Instance.FlowerToolBox.SelectedFWControl;

                if (type != null)
                {
                    UIComponent uIComponent = null;
                    Point point = e.GetPosition(this);
                    FWControl fwControl = null;

                    if (type.Equals(typeof(Control.FWButton)))
                    {
                        fwControl = new FWButton
                        {
                            Text = "Button",
                        };
                    }
                    else if (type.Equals(typeof(Control.FWTextBox)))
                    {
                        fwControl = new FWTextBox
                        {
                            Text = "TextBox",
                        };
                    }
                    else if (type.Equals(typeof(Control.FWListBox)))
                    {
                        fwControl = new FWListBox();
                    }

                    fwControl.Left = point.X;
                    fwControl.Top = point.Y;
                    fwControl.Width = 100;
                    fwControl.Height = 30;

                    FWControlList.Add(fwControl);

                    uIComponent = new UIComponent(fwControl);
                    uIComponent.PropertyChanged += UIComponent_PropertyChanged;

                    canvasArea.Children.Add(uIComponent);

                    UIManager.Instance.FlowerToolBox.Deselection();

                    ConvertToData();
                }
            }

            e.Handled = true;
        }

        private void UIComponent_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ConvertToData();
        }

        private void ConvertToData()
        {
            
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach (UIElement control in canvasArea.Children)
            {
                ((UIComponent)control).IsSelected = false;
            }
        }

        public void DeleteControl(UIComponent uiComponent)
        {
            FWControl fwControl = uiComponent.FWControl;

            FWEventList fwEventList = fwControl.FWEventList;
            foreach (Event ev in fwEventList.Events)
            {
                if (!string.IsNullOrWhiteSpace(ev.EventValue))
                {
                    //제거.
                }
            }

            FWControlList.Remove(fwControl);
            canvasArea.Children.Remove(uiComponent);
        }
    }
}
