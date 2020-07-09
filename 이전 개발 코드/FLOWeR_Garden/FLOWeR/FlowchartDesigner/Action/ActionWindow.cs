using System;
using System.Windows.Controls;
using FLOWeR_Garden_Ver02.DockablePane;
using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Action
{
    public class ActionWindow : FloatingWindow
    {
        public ActionWindow() : base()
        {
            Pane = new Pane("액션");

            ContentPane contentPane = new ContentPane();
            contentPane.AddPane(Pane);

            base.MainContent = contentPane;

            base.Width = 800;
            base.Height = 600;
        }

        public Pane Pane
        {
            get;
            set;
        }

        internal void ChangePanel(UserControl symbolActionWindow)
        {
            Pane.MainContent = symbolActionWindow;
        }
    }
}
