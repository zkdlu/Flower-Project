using System;

namespace PropertyExplorer.Models
{
    public class PropertySet
    {
        public string Name { get; set; }
    }

    public class PropertySet<T> : PropertySet
    {
        private Func<T> getter;
        private Action<T> setter;

        public T Value
        {
            get
            {
                return this.getter.Invoke();
            }
            set
            {
                setter.Invoke(value);
            }
        }

        public PropertySet(string name, Func<T> getter, Action<T> setter)
        {
            Name = name;

            this.getter = getter;
            this.setter = setter;
        }
    }
}
