using System.Windows;

namespace FLOWeR_Garden_Ver02.DockablePane
{
    /// <summary>
    /// OverlayWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OverlayWindow : Window
    {
        public OverlayWindow()
        {
            InitializeComponent();
        }

        public Size RectSize
        {
            get
            {
                return new Size(ActualWidth, ActualHeight);
            }
        }

        public Rect LeftDock
        {
            get
            {
                return new Rect(new Point(this.Left, this.Top + RectSize.Height / 2 - 16), new Size(32, 32));
            }
        }

        public Rect RightDock
        {
            get
            {
                return new Rect(new Point(this.Left + RectSize.Width - 32, this.Top + RectSize.Height / 2 - 16), new Size(32, 32));
            }
        }

        public Rect TopDock
        {
            get
            {
                return new Rect(new Point(this.Left + RectSize.Width / 2 - 16, this.Top), new Size(32, 32));
            }
        }

        public Rect BottomDock
        {
            get
            {
                return new Rect(new Point(this.Left + RectSize.Width / 2 - 16, this.Top + RectSize.Height - 32), new Size(32, 32));
            }
        }

        public Rect CenterDock
        {
            get
            {
                return new Rect(new Point(this.Left + RectSize.Width / 2 - 48, this.Top + RectSize.Height / 2 - 48), new Size(96, 96));
            }
        }
    }
}
