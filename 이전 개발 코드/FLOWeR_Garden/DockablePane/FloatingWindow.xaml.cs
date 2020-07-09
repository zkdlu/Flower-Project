using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace FLOWeR_Garden_Ver02.DockablePane
{
    /// <summary>
    /// FloatingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FloatingWindow : Window, IQuit
    {
        public FloatingWindow()
        {
            InitializeComponent();
        }

        private bool _isMoveStart = true;

        public Point CurrentMousePos
        {
            get;
            private set;
        }

        public DockPane MainDockPane
        {
            get;
            set;
        }

        public ContentPane MainContent
        {
            get;
            set;
        }

        public void AppendContent(ContentPane contentPane)
        {
            List<Pane> dockedPanes = contentPane.DockedPanes;
            foreach (Pane pane in dockedPanes.ToArray())
            {
                contentPane.RemovePane(pane);
                MainContent.AddPane(pane);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            DockPane dockPane = new DockPane();
            mainPanel.Children.Add(dockPane);
            dockPane.ParentWindow = this;

            MainDockPane = dockPane;
            MainDockPane.SetContentPane(MainContent);
            MainContent.ParentDockPane = MainDockPane;

            DockManager.Instance.RegistDockPane(dockPane);
        }

        private void OnBtnClose(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void OnGridMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (_isMoveStart)
                {
                    _isMoveStart = false;

                    this.DragMove();
                }
            }
        }

        private void OnGridMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                _isMoveStart = true;

                Point point = CurrentMousePos;

                if (DockManager.Instance.CheckDockable(point))
                {
                    DockManager.Instance.Docking(this, point);
                }

                DockManager.Instance.HideOverlay();
            }
        }

        private void OnLocationChanged(object sender, EventArgs e)
        {
            if (!_isMoveStart)
            {
                Point point = PointToScreen(Mouse.GetPosition(this));
                CurrentMousePos = point;

                DockManager.Instance.ShowOverlay(this, CurrentMousePos);
            }
        }

        public void RemoveSelf(DockPane dockPane)
        {
            mainPanel.Children.Remove(dockPane);
            this.Close();
        }

    }
}
