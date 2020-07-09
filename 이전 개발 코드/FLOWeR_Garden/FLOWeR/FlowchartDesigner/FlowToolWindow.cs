using FLOWeR_Garden_Ver02.DockablePane;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner
{
    public class FlowToolWindow : FloatingWindow
    {
        public FlowToolWindow() : base()
        {
            Pane flowToolPane = new Pane("순서도 도구");
            flowToolPane.MainContent = FlowManager.Instance.FlowTool;
            FlowManager.Instance.FlowTool.DockedWindow = this;

            ContentPane contentPane = new ContentPane();
            contentPane.AddPane(flowToolPane);

            base.MainContent = contentPane;

            base.Width = 800;
            base.Height = 600;
        }
    }
}
