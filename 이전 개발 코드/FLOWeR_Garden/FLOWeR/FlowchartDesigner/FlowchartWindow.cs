using FLOWeR_Garden_Ver02.DockablePane;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner
{
    public class FlowchartWindow : FloatingWindow
    {
        public FlowchartWindow(FlowchartDesigner flowchartDesigner) : base()
        {
            FlowDesigner = flowchartDesigner;

            Pane flowchartPane = new Pane(FlowDesigner.FlowValue);
            flowchartPane.MainContent = flowchartDesigner;
            //FlowManager.Instance.FlowchartDesigner.DockedWindow = this;

            ContentPane contentPane = new ContentPane();
            contentPane.AddPane(flowchartPane);

            base.MainContent = contentPane;
        }

        public FlowchartDesigner FlowDesigner
        {
            get;
            set;
        }
    }
}
