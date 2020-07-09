using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using FLOWeR_Garden_Ver02.DockablePane;

namespace FLOWeR_Garden_Ver02.FLOWeR.UIDesigner
{
    // 메인이 되는 UI Designer 관리
    public class UIManager
    {
        public static UIManager Instance
        {
            get;
            private set;
        }
        static UIManager()
        {
            Instance = new UIManager();
        }
        private UIManager()
        {
        }

        public string BackgroundImage
        {
            get;
            set;
        }

        private UIDesigner _flowerUIDesigner;
        public UIDesigner FlowerUIDesigner
        {
            get
            {
                if (_flowerUIDesigner == null)
                {
                    _flowerUIDesigner = new UIDesigner();
                }
                return _flowerUIDesigner;
            }
            set
            {
                _flowerUIDesigner = value;
            }
        }

        private ToolBox _flowerToolBox;
        public ToolBox FlowerToolBox
        {
            get
            {
                if (_flowerToolBox == null)
                {
                    _flowerToolBox = new ToolBox();
                }
                return _flowerToolBox;
            }
            set
            {
                _flowerToolBox = value;
            }
        }

        private FWProperty _flowerProperty;
        public FWProperty FlowerProperty
        {
            get
            {
                if (_flowerProperty == null)
                {
                    _flowerProperty = new FWProperty();
                }
                return _flowerProperty;
            }
            set
            {
                _flowerProperty = value;
            }
        }

        private FWEvent _flowerEvent;
        public FWEvent FlowerEvent
        {
            get
            {
                if (_flowerEvent == null)
                {
                    _flowerEvent = new FWEvent();
                }
                return _flowerEvent;
            }
            set
            {
                _flowerEvent = value;
            }
        }

        public void ShowUIDesigner(DockPane dockPane)
        {
            UIWindow uiWindow = new UIWindow();
            uiWindow.Show();
            DockManager.Instance.ForceDocking(dockPane, uiWindow, EDock.Center);
        }

        public void ShowToolBox(DockPane dockPane)
        {
            ToolBoxWindow toolBoxWindow = new ToolBoxWindow();
            toolBoxWindow.Show();
            DockManager.Instance.ForceDocking(dockPane, toolBoxWindow, EDock.Left);
        }

        public void ShowPropertyGrid(DockPane dockPane)
        {
            FWPropertyWindow propertyWindow = new FWPropertyWindow();
            propertyWindow.Show();
            DockManager.Instance.ForceDocking(dockPane, propertyWindow, EDock.Right);
        }

        public void ShowEventGrid(DockPane dockPane)
        {
            FWEventWindow eventWindow = new FWEventWindow();
            eventWindow.Show();
            DockManager.Instance.ForceDocking(dockPane, eventWindow, EDock.Right);
        }

        public void ChangeBackgroundImage(BitmapImage bitmapImage)
        {
            _flowerUIDesigner.ChangeBackgroundImage(bitmapImage);
        }

        internal void SaveUIDesigner()
        {
           
        }
    }
}
