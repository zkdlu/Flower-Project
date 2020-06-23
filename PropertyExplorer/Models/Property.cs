using System;

namespace PropertyExplorer.Models
{
    public class Property
    {

    }

    public class Property<T> : Property
    {
        public string Name { get; set; }

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

        public Property(string name, Func<T> getter, Action<T> setter)
        {
            Name = name;

            this.getter = getter;
            this.setter = setter;
        }
    }
}
