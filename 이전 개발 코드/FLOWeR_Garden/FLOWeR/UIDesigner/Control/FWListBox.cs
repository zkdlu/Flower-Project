using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Control
{
    [Serializable]
    public class FWListBox : FWControl
    {
        static int index = 1;

        public FWListBox()
        {
            Width = 120;
            Height = 88;
            Name = "listbox1" + index;
            Items = new List<object>();
            ItemHeight = 12;

            index++;

            PropertyInfo[] propertyInfos = typeof(FWListBox).GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                object[] attributes = propertyInfo.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    if (attribute is BrowsableAttribute browsableAttribute)
                    {
                        string propertyName = propertyInfo.Name;

                        Event ev = new Event(propertyName);

                        FWEventList.Events.Add(ev);
                    }
                }
            }
        }

        [PropertyAttribute]
        [CategoryAttribute("데이터"), DescriptionAttribute("목록상자의 항목"), DisplayNameAttribute("SelectedItem")]
        public object SelectedItem
        {
            get;
            set;
        }

        [CategoryAttribute("데이터"), DescriptionAttribute("목록상자의 항목"), DisplayNameAttribute("Items")]
        public List<object> Items
        {
            get;
            set;
        }

        [CategoryAttribute("동작"), DescriptionAttribute("항목의 높이"), DisplayNameAttribute("ItemHeight")]
        public int ItemHeight
        {
            get;
            set;
        }
        
        public override string TypeName => "ListBox";

        [Browsable(false)]
        public string SelectedIndexChanged { get; set; }
    }
}
