using FLOWeR_Garden_Ver02.DockablePane;

namespace FLOWeR_Garden_Ver02.FLOWeR.UIDesigner
{
    public class ToolBoxWindow : FloatingWindow
    {
        public ToolBoxWindow() : base()
        {
            Pane toolBoxPane = new Pane("도구상자");
            toolBoxPane.MainContent = UIManager.Instance.FlowerToolBox;
            UIManager.Instance.FlowerToolBox.DockedWindow = this;

            ContentPane contentPane = new ContentPane();
            contentPane.AddPane(toolBoxPane);

            base.MainContent = contentPane;

            base.Width = 800;
            base.Height = 600;
        }
    }
}
