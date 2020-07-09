using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FLOWeR_Garden_Ver02.DockablePane
{
    /// <summary>
    /// ContentPAne.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ContentPane : UserControl
    {
        public ContentPane(string paneTitle) : this()
        {
            Pane pane = new Pane(paneTitle);
            AddPane(pane);
        }

        public ContentPane()
        {
            InitializeComponent();

            DockedPanes = new List<Pane>();
        }

        public DockPane ParentDockPane
        {
            get;
            set;
        }

        public List<Pane> DockedPanes
        {
            get;
            set;
        }

        public void AddPane(Pane pane)
        {
            TabItem item = new TabItem();
            item.Header = pane.PaneTitle;

            ContentPresenter contentPresenter = new ContentPresenter();
            contentPresenter.Content = pane;

            item.Content = contentPresenter;

            tbcContents.Items.Add(item);

            DockedPanes.Add(pane);
        }

        public void RemovePane(Pane pane)
        {
            foreach (TabItem item in tbcContents.Items)
            {
                if (item.Content is ContentPresenter contentPresenter)
                {
                    if (contentPresenter.Content is Pane currentPane)
                    {
                        tbcContents.Items.Remove(item);

                        DockedPanes.Remove(currentPane);

                        break;
                    }
                }
            }
        }

        private void OnTabChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (tbcContents.SelectedItem is TabItem item)
            {
                if (item.Content is ContentPresenter contentPresenter)
                {
                    if (contentPresenter.Content is Pane currentPane)
                    {
                        tbTitle.Text = currentPane.PaneTitle;
                    }
                }
            }
        }

        private void OnBtnClose(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (tbcContents.Items.Count > 1)
            {
                TabItem currentItem = tbcContents.SelectedItem as TabItem;
                tbcContents.Items.Remove(currentItem);
            }
            else
            {
                ParentDockPane.RemoveContentPane(this);
                ParentDockPane.RemoveSelf();
            }
        }

        Point startPoint;

        private void OnGridMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            paneHeader.ReleaseMouseCapture();
        }

        private void OnGridMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!paneHeader.IsMouseCaptured)
            {
                startPoint = e.GetPosition(this);
                paneHeader.CaptureMouse();
            }
        }

        private void OnGridMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (paneHeader.IsMouseCaptured &&
               Math.Abs(startPoint.X - e.GetPosition(this).X) > 4)
            {
                paneHeader.ReleaseMouseCapture();

                //플로팅 윈도우

                if (!(ParentDockPane.ParentWindow is FloatingWindow))
                {
                    ParentDockPane.UnDocking(this);
                }
            }
        }
    }
}
