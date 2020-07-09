using System;
using System.ComponentModel;
using System.Reflection;

namespace FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Control
{
    [Serializable]
    public class FWButton : FWControl
    {
        static int index = 1;

        public FWButton()
        {
            Width = 75;
            Height = 23;
            Name = "button" + index;

            index++;

            PropertyInfo[] propertyInfos = typeof(FWButton).GetProperties();
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
        [CategoryAttribute("모양"), DescriptionAttribute("컨트롤의 텍스트"), DisplayNameAttribute("Text")]
        public string Text
        {
            get;
            set;
        }

        public override string TypeName => "Button";
    }
}
