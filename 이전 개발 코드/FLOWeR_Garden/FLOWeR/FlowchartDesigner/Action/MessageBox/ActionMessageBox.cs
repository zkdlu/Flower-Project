using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Action.MessageBox
{
    public class ActionMessageBox : FWProcess
    {
        public ActionMessageBox() : base()
        {
            Description = "메시지 박스";

            SymbolActionWindow = new ActionMessageBoxWindow(this);
        }

        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

    }
}
