using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Action.InputValue
{
    public class ActionInputValue : FWProcess
    {
        public ActionInputValue() : base()
        {
            Description = "값 대입";

            SymbolActionWindow = new ActionInputValueWindow(this);
        }

        private string varName;
        public string VarName
        {
            get { return varName; }
            set
            {
                varName = value;
                OnPropertyChanged("VarName");
            }
        }

        private string varValue;
        public string VarValue
        {
            get { return varValue; }
            set
            {
                varValue = value;
                OnPropertyChanged("VarValue");
            }
        }
    }
}
