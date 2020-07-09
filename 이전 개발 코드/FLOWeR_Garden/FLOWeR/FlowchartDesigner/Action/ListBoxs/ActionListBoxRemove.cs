using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Action.ListBoxs
{
    public class ActionListBoxRemove : FWProcess
    {
        public ActionListBoxRemove() : base()
        {
            Description = "리스트박스 아이템 삭제";

            SymbolActionWindow = new ActionListBoxRemoveWindow(this);
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
