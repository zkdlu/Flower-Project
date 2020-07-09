using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner;
using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol;
using FLOWeR_Garden_Ver02.FLOWeR.Interpreter;
using FLOWeR_Garden_Ver02.FLOWeR.UIDesigner;
using FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Control;
using System.Collections.Generic;

namespace FLOWeR_Garden_Ver02.FLOWeR
{
    // UI, Flowchart 디자이너 관리
    public class FlowerManager
    {
        public static FlowerManager Instance
        {
            get;
            private set;
        }
        static FlowerManager()
        {
            Instance = new FlowerManager();
        }
        private FlowerManager()
        {
        }

        public List<FWControl> FWControls
        {
            get
            {
                return UIManager.Instance.FlowerUIDesigner.FWControlList;
            }
        }

        public List<FWSymbol> FWSymbols
        {
            get
            {
                List<FWSymbol> fwSymbols = new List<FWSymbol>();

                foreach (FlowchartDesigner.FlowchartDesigner flowDesigner in 
                    FlowManager.Instance.FlowchartDesigners)
                {
                    fwSymbols.AddRange(flowDesigner.FWSymbolList);
                }

                return fwSymbols;
            }
        }
    }
}
