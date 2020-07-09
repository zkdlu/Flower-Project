using System;
using System.ComponentModel;
using FLOWeR_Garden_Ver02.DockablePane;

namespace FLOWeR_Garden_Ver02.FLOWeR.UIDesigner
{
    public class UIWindow : FloatingWindow
    {
        public UIWindow() : base()
        {
            Pane uiPane = new Pane("UI 디자이너");
            uiPane.MainContent = UIManager.Instance.FlowerUIDesigner;
            UIManager.Instance.FlowerUIDesigner.DockedWindow = this;

            ContentPane contentPane = new ContentPane();
            contentPane.AddPane(uiPane);
            
            base.MainContent = contentPane;
        }
    }
}
