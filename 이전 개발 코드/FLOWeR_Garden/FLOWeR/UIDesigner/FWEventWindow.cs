using FLOWeR_Garden_Ver02.DockablePane;

namespace FLOWeR_Garden_Ver02.FLOWeR.UIDesigner
{
    public class FWEventWindow : FloatingWindow
    {
        public FWEventWindow() : base()
        {
            Pane eventPane = new Pane("이벤트");
            eventPane.MainContent = UIManager.Instance.FlowerEvent;
            UIManager.Instance.FlowerEvent.DockedWindow = this;

            ContentPane contentPane = new ContentPane();
            contentPane.AddPane(eventPane);

            base.MainContent = contentPane;

            base.Width = 800;
            base.Height = 600;
        }
    }
}
