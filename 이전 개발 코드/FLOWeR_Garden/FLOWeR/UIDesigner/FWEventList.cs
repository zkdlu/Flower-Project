using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FLOWeR_Garden_Ver02.FLOWeR.UIDesigner
{
    [Serializable]
    public class FWEventList
    {
        public FWEventList()
        {
            Events = new ObservableCollection<Event>();
        }

        public ObservableCollection<Event> Events
        {
            get;
            set;
        }
    }

    public class Event
    {
        public Event(string eventName)
        {
            EventName = eventName;
            EventValue = string.Empty;
        }

        public string EventName
        {
            get;
            set;
        }

        public string EventValue
        {
            get;
            set;
        }
    }
}
