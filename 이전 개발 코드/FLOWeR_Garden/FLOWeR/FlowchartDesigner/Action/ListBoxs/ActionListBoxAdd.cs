using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Action.ListBoxs
{
    public class ActionListBoxAdd : FWProcess
    {
        public ActionListBoxAdd() : base()
        {
            Description = "리스트박스 아이템 추가";

            SymbolActionWindow = new ActionListBoxAddWindow(this);
        }

        private string listBoxName;
        public string ListBoxName
        {
            get { return listBoxName; }
            set
            {
                listBoxName = value;
                OnPropertyChanged("ListBoxName");
            }
        }

        private object listBoxItem;
        public object ListBoxItem
        {
            get { return listBoxItem; }
            set
            {
                listBoxItem = value;
                OnPropertyChanged("ListBoxItem");
            }
        }
    }
}
