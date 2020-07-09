using System.Windows;
using System.Windows.Controls;

namespace FLOWeR_Garden_Ver02.DockablePane
{
    /// <summary>
    /// Pane.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Pane : UserControl
    {
        public Pane(string paneTitle)
        {
            InitializeComponent();
            PaneTitle = paneTitle;
        }

        public string PaneTitle
        {
            get;
            private set;
        }

        public Control MainContent
        {
            set
            {
                mainPanel.Content = value;
                //mainPanel.Children.Add(value);
            }
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size prevSize = e.PreviousSize;

            Size newSize = e.NewSize;

            MessageBox.Show(prevSize.ToString() + " , " + newSize.ToString());

            e.Handled = false;
        }
    }
}
