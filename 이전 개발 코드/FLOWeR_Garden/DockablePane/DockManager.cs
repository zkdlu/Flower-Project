using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FLOWeR_Garden_Ver02.DockablePane
{
    public class DockManager
    {
        static DockManager()
        {
            Instance = new DockManager();
        }

        public static DockManager Instance
        {
            get;
            private set;
        }

        private DockManager()
        {
            DockedPanes = new List<DockPane>();
        }

        public DockPane MainDockPane
        {
            get;
            set;
        }

        public DockPane Sample
        {
            get;
            set;
        }

        public Window MainWindow
        {
            get;
            set;
        }

        public DockPane CurrentDockPane
        {
            get;
            private set;
        }

        public List<DockPane> DockedPanes
        {
            get;
            private set;
        }

        public void RegistDockPane(DockPane dockPane)
        {
            if (!DockedPanes.Contains(dockPane))
            {
                DockedPanes.Add(dockPane);
            }
        }

        public void UnregistDockPane(DockPane dockPane)
        {
            if (DockedPanes.Contains(dockPane))
            {
                DockedPanes.Remove(dockPane);
            }
        }

        private int GetZIndex(Control control)
        {
            int zIndex = Panel.GetZIndex(control);

            return zIndex;
        }

        public void ShowOverlay(FloatingWindow floatingWindow, Point point)
        {
            foreach (DockPane dockPane in DockedPanes)
            {
                Rect rect = dockPane.DockingArea;

                if (rect.Contains(point))
                {
                    //if (CurrentDockPane != null)
                    //{
                    //    int z1 = GetZIndex(dockPane);
                    //    int z2 = GetZIndex(CurrentDockPane);

                    //    if (z1 > z2)
                    //    {
                    //        CurrentDockPane.InactivationOverlay();
                    //        CurrentDockPane = dockPane;
                    //        dockPane.ActivationOverlay();
                    //    }
                    //    else
                    //    {
                    //        CurrentDockPane.ActivationOverlay();
                    //    }
                    //}
                    //else
                    {
                        CurrentDockPane = dockPane;
                        dockPane.ActivationOverlay();
                    }
                }
                else
                {
                    dockPane.InactivationOverlay();
                }
            }
        }

        public void HideOverlay()
        {
            foreach (DockPane dockPane in DockedPanes)
            {
                CurrentDockPane = null;

                dockPane.InactivationOverlay();
            }
        }

        public bool CheckDockable(Point point)
        {
            if (CurrentDockPane != null)
            {
                OverlayWindow overlay = CurrentDockPane.OverlayWindow;

                if (overlay.LeftDock.Contains(point) || overlay.RightDock.Contains(point) ||
                    overlay.TopDock.Contains(point) || overlay.BottomDock.Contains(point) || overlay.CenterDock.Contains(point))
                {
                    return true;
                }
            }

            return false;
        }

        public void ForceDocking(DockPane targetDockPane, FloatingWindow floatingWindow, EDock dock)
        {
            try
            {
                DockPane dockPane = floatingWindow.MainDockPane;
                dockPane.RemoveSelf();

                if (dockPane != null)
                {
                    targetDockPane.Docking(dockPane, dock);

                    floatingWindow.Close();
                }
            }
            catch
            {
                return;
            }
        }

        public void Docking(FloatingWindow floatingWindow, Point point)
        {
            if (CurrentDockPane != null)
            {
                OverlayWindow overlay = CurrentDockPane.OverlayWindow;

                DockPane dockPane = floatingWindow.MainDockPane;
                dockPane.RemoveSelf();

                GridSplitter splitter = new GridSplitter();
                splitter.Background = Brushes.Gray;

                if (dockPane != null)
                {
                    if (overlay.LeftDock.Contains(point))
                    {
                        CurrentDockPane.Docking(dockPane, EDock.Left);
                    }
                    else if (overlay.RightDock.Contains(point))
                    {
                        CurrentDockPane.Docking(dockPane, EDock.Right);
                    }
                    else if (overlay.TopDock.Contains(point))
                    {
                        CurrentDockPane.Docking(dockPane, EDock.Top);
                    }
                    else if (overlay.BottomDock.Contains(point))
                    {
                        CurrentDockPane.Docking(dockPane, EDock.Bottom);
                    }
                    else if (overlay.CenterDock.Contains(point))
                    {
                        CurrentDockPane.Docking(dockPane, EDock.Center);
                    }

                    floatingWindow.Close();
                }
            }
        }
    }
}
