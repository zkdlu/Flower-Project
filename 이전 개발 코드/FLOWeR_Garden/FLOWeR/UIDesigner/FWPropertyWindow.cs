using FLOWeR_Garden_Ver02.DockablePane;

namespace FLOWeR_Garden_Ver02.FLOWeR.UIDesigner
{
    public class FWPropertyWindow : FloatingWindow
    {
        public FWPropertyWindow() : base()
        {
            Pane propertyPane = new Pane("속성");
            propertyPane.MainContent = UIManager.Instance.FlowerProperty;
            UIManager.Instance.FlowerProperty.DockedWindow = this;

            ContentPane contentPane = new ContentPane();
            contentPane.AddPane(propertyPane);

            base.MainContent = contentPane;

            base.Width = 800;
            base.Height = 600;
        }
    }
}
