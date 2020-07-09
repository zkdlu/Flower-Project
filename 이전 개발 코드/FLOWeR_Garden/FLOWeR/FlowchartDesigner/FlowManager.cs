using FLOWeR_Garden_Ver02.DockablePane;
using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner
{
    public class FlowManager
    {
        public static FlowManager Instance
        {
            get;
            private set;
        }
        static FlowManager()
        {
            Instance = new FlowManager();
        }
        private FlowManager()
        {
        }

        private List<FlowchartDesigner> _flowchartDesigners;
        public List<FlowchartDesigner> FlowchartDesigners
        {
            get
            {
                if (_flowchartDesigners == null)
                {
                    _flowchartDesigners = new List<FlowchartDesigner>();
                }
                return _flowchartDesigners;
            }
            set
            {
                _flowchartDesigners = value;
            }
        }

        private FlowTool _flowTool;
        public FlowTool FlowTool
        {
            get
            {
                if (_flowTool == null)
                {
                    _flowTool = new FlowTool();
                }
                return _flowTool;
            }
            set
            {
                _flowTool = value;
            }
        }

        private ActionTool _actionTool;
        public ActionTool ActionTool
        {
            get
            {
                if (_actionTool == null)
                {
                    _actionTool = new ActionTool();
                }
                return _actionTool;
            }
            set
            {
                _actionTool = value;
            }
        }

        private ActionWindow _actionWindow;
        public ActionWindow ActionWindow
        {
            get
            {
                if (_actionWindow == null)
                {
                    _actionWindow = new ActionWindow();
                }
                return _actionWindow;
            }
            set
            {
                _actionWindow = value;
            }
        }

        public void ShowActionWindow(DockPane dockPane)
        {
            ActionWindow.Show();
            DockManager.Instance.ForceDocking(dockPane, ActionWindow, EDock.Right);
        }

        public void ShowActionTool(DockPane dockPane)
        {
            ActionToolWindow actionToolWindow = new ActionToolWindow();
            actionToolWindow.Show();
            DockManager.Instance.ForceDocking(dockPane, actionToolWindow, EDock.Left);
        }

        public void ShowFlowTool(DockPane dockPane)
        {
            FlowToolWindow flowToolWindow = new FlowToolWindow();
            flowToolWindow.Show();
            DockManager.Instance.ForceDocking(dockPane, flowToolWindow, EDock.Left);
        }

        public void ShowFlowDesigner(FlowchartDesigner flowchartDesigner)
        {
            FlowchartWindow flowchartWindow = new FlowchartWindow(flowchartDesigner);
            flowchartDesigner.MinHeight = 400;
            flowchartWindow.Show();

            DockManager.Instance.ForceDocking(DockManager.Instance.MainDockPane, flowchartWindow, EDock.Bottom);
        }
    }
}
