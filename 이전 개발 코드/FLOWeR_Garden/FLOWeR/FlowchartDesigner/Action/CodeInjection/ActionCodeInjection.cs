using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Action.CodeInjection
{
    public class ActionCodeInjection : FWProcess
    {
        public ActionCodeInjection() : base()
        {
            Description = "코드 삽입";

            SymbolActionWindow = new ActionCodeInjectionWindow(this);
        }

        private string code;
        public string Code
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged("Code");
            }
        }
    }
}
