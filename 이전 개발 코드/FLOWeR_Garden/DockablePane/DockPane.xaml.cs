using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FLOWeR_Garden_Ver02.DockablePane
{
    /// <summary>
    /// DockPane.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DockPane : UserControl
    {
        public DockPane()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            OverlayWindow = new OverlayWindow();
            DockManager.Instance.RegistDockPane(this);
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            OverlayWindow.Close();
            DockManager.Instance.UnregistDockPane(this);
        }

        public IQuit ParentWindow
        {
            get;
            set;
        }

        public DockPane ParentDockPane
        {
            get;
            set;
        }

        public EDock DockedPos
        {
            get;
            set;
        }

        public Rect DockingArea
        {
            get
            {
                return new Rect(PointToScreen(new Point(0, 0)), new Size(panelCenter.ActualWidth, panelCenter.ActualHeight));
            }
        }

        public OverlayWindow OverlayWindow
        {
            get;
            set;
        }

        public void ActivationOverlay()
        {
            OverlayWindow.Owner = DockManager.Instance.MainWindow;
            OverlayWindow.Left = PointToScreen(new Point(0, 0)).X;
            OverlayWindow.Top = PointToScreen(new Point(0, 0)).Y;
            OverlayWindow.Width = ActualWidth;
            OverlayWindow.Height = ActualHeight;
            OverlayWindow.Show();
        }
        
        public void InactivationOverlay()
        {
            OverlayWindow.Owner = null;
            OverlayWindow.Hide();
        }

        public void SetContentPane(ContentPane contentPane)
        {
            panelCenter.Children.Add(contentPane);
        }

        public void RemoveContentPane(ContentPane contentPane)
        {
            panelCenter.Children.Remove(contentPane);
        }

        public void Docking(DockPane dockPane, EDock dockPos)
        {
            GridSplitter splitter = new GridSplitter();
            splitter.Background = Brushes.LightGray;

            dockPane.ParentWindow = null;
            dockPane.ParentDockPane = this;

            dockPane.DockedPos = dockPos;

            switch (dockPos)
            {
                case EDock.Top:
                    {
                        if (panelTop.Children.Count > 0)
                        {
                            if (panelTop.Children[0] is DockPane dockedPane)
                            {
                                dockedPane.Docking(dockPane, EDock.Center);
                            }
                        }
                        else
                        {
                            splitter.Height = 10;
                            splitter.HorizontalAlignment = HorizontalAlignment.Stretch;
                            splitter.SetValue(Grid.ColumnProperty, 0);
                            splitter.SetValue(Grid.ColumnSpanProperty, 5);
                            splitter.SetValue(Grid.RowProperty, 1);
                            panelTop.Children.Add(dockPane);
                        }
                    }
                    break;
                case EDock.Left:
                    {
                        if (panelLeft.Children.Count > 0)
                        {
                            if (panelLeft.Children[0] is DockPane dockedPane)
                            {
                                dockedPane.Docking(dockPane, EDock.Center);
                            }
                        }
                        else
                        {
                            splitter.Width = 10;
                            splitter.HorizontalAlignment = HorizontalAlignment.Stretch;
                            splitter.SetValue(Grid.ColumnProperty, 1);
                            splitter.SetValue(Grid.RowProperty, 2);
                            panelLeft.Children.Add(dockPane);
                        }
                    }
                    break;
                case EDock.Center:
                    {
                        if (panelCenter.Children.Count > 0)
                        {
                            if (panelCenter.Children[0] is ContentPane contentPane)
                            {
                                ContentPane dockedContentPane = dockPane.panelCenter.Children[0] as ContentPane;
                                foreach (Pane pane in dockedContentPane.DockedPanes.ToArray())
                                {
                                    dockedContentPane.RemovePane(pane);
                                    contentPane.AddPane(pane);
                                }
                            }
                        }
                        else
                        {
                            panelCenter.Children.Add(dockPane);
                        }
                    }
                    break;
                case EDock.Right:
                    {
                        if (panelRight.Children.Count > 0)
                        {
                            if (panelRight.Children[0] is DockPane dockedPane)
                            {
                                dockedPane.Docking(dockPane, EDock.Center);
                            }
                        }
                        else
                        {
                            splitter.Width = 10;
                            splitter.HorizontalAlignment = HorizontalAlignment.Stretch;
                            splitter.SetValue(Grid.ColumnProperty, 3);
                            splitter.SetValue(Grid.RowProperty, 2);
                            panelRight.Children.Add(dockPane);
                        }
                    }
                    break;
                case EDock.Bottom:
                    {
                        if (panelBottom.Children.Count > 0)
                        {
                            if (panelBottom.Children[0] is DockPane dockedPane)
                            {
                                dockedPane.Docking(dockPane, EDock.Center);
                            }
                        }
                        else
                        {
                            splitter.Height = 10;
                            splitter.HorizontalAlignment = HorizontalAlignment.Stretch;
                            splitter.SetValue(Grid.ColumnProperty, 0);
                            splitter.SetValue(Grid.ColumnSpanProperty, 5);
                            splitter.SetValue(Grid.RowProperty, 3);
                            panelBottom.Children.Add(dockPane);
                        }
                    }
                    break;
                default:
                    break;
            }
            DockManager.Instance.Sample = this;
            mainGrid.Children.Add(splitter);
        }
        
        public void UnDocking(ContentPane contentPane)
        {
            DockPane dockPane = contentPane.ParentDockPane;
            dockPane.Background = Brushes.Black;
            dockPane.RemoveContentPane(contentPane);

            Point point = PointToScreen(System.Windows.Input.Mouse.GetPosition(this));

            FloatingWindow floatingWindow = new FloatingWindow
            {
                MainContent = contentPane,
                Left = point.X,
                Top = point.Y
            };
            floatingWindow.Show();
            floatingWindow.Activate();

            //Arrange Grid
            ArrangeDockPane(dockPane);
            ArrangeGrid(dockPane.ParentDockPane);
        }

        private void ArrangeGrid(DockPane dockPane)
        {
            List<GridSplitter> splitters = new List<GridSplitter>();
            foreach (UIElement uiElem in dockPane.mainGrid.Children)
            {
                if (uiElem is GridSplitter splitter)
                {
                    splitters.Add(splitter);
                }
            }

            if (DockedPos == EDock.Left)
            {
                foreach (GridSplitter splitter in splitters)
                {
                    int column = (int)splitter.GetValue(Grid.ColumnProperty);
                    int row = (int)splitter.GetValue(Grid.RowProperty);

                    if (column == 1 && row == 2)
                    {
                        dockPane.mainGrid.Children.Remove(splitter);
                        dockPane.mainGrid.ColumnDefinitions[0].Width = new GridLength();
                        break;
                    }
                }
            }
            if (DockedPos == EDock.Right)
            {
                foreach (GridSplitter splitter in splitters)
                {
                    int column = (int)splitter.GetValue(Grid.ColumnProperty);
                    int row = (int)splitter.GetValue(Grid.RowProperty);

                    if (column == 3 && row == 2)
                    {
                        dockPane.mainGrid.Children.Remove(splitter);
                        dockPane.mainGrid.ColumnDefinitions[4].Width = new GridLength();
                        break;
                    }
                }
            }
            if (DockedPos == EDock.Top)
            {
                foreach (GridSplitter splitter in splitters)
                {
                    int column = (int)splitter.GetValue(Grid.ColumnProperty);
                    int row = (int)splitter.GetValue(Grid.RowProperty);

                    if (column == 0 && row == 1)
                    {
                        dockPane.mainGrid.Children.Remove(splitter);
                        dockPane.mainGrid.RowDefinitions[0].Height = new GridLength();
                        break;
                    }
                }
            }
            if (DockedPos == EDock.Bottom)
            {
                foreach (GridSplitter splitter in splitters)
                {
                    int column = (int)splitter.GetValue(Grid.ColumnProperty);
                    int row = (int)splitter.GetValue(Grid.RowProperty);

                    if (column == 0 && row == 3)
                    {
                        dockPane.mainGrid.Children.Remove(splitter);
                        dockPane.mainGrid.RowDefinitions[4].Height = new GridLength();
                        break;
                    }
                }
            }
        }

        public void ArrangeDockPane(DockPane dockPane)
        {
            if (dockPane.ParentDockPane != null)
            {
                dockPane.ParentDockPane.RemoveDockPane(dockPane);
            }
        }

        public void RemoveSelf()
        {
            if (ParentWindow != null)
            {
                ParentWindow.RemoveSelf(this);
            }
            else if (ParentDockPane != null)
            {
                ParentDockPane.RemoveDockPane(this);
                ArrangeGrid(ParentDockPane);
            }
        }

        private void RemoveDockPane(DockPane dockPane)
        {
            if (panelLeft.Children.Contains(dockPane))
            {
                panelLeft.Children.Remove(dockPane);
            }
            else if (panelRight.Children.Contains(dockPane))
            {
                panelRight.Children.Remove(dockPane);
            }
            else if (panelTop.Children.Contains(dockPane))
            {
                panelTop.Children.Remove(dockPane);
            }
            else if (panelBottom.Children.Contains(dockPane))
            {
                panelBottom.Children.Remove(dockPane);
            }
            else if (panelCenter.Children.Contains(dockPane))
            {
                panelCenter.Children.Remove(dockPane);
            }
        }
    }
}
