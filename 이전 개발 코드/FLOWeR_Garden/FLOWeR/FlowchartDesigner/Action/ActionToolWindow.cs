using FLOWeR_Garden_Ver02.DockablePane;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Action
{
    public class ActionToolWindow : FloatingWindow
    {
        public ActionToolWindow() : base()
        {
            Pane actionPane = new Pane("액션 도구");
            actionPane.MainContent = FlowManager.Instance.ActionTool;
            FlowManager.Instance.ActionTool.DockedWindow = this;

            ContentPane contentPane = new ContentPane();
            contentPane.AddPane(actionPane);

            base.MainContent = contentPane;

            base.Width = 800;
            base.Height = 600;
        }
    }
}
