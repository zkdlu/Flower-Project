using System;
using System.Windows.Media;

namespace PropertyExplorer.Models
{
    public class DoublePropertySet : PropertySet<double>
    {
        public DoublePropertySet(string name, Func<double> getter, Action<double> setter) : base(name, getter, setter)
        {
        }
    }

    public class BrushPropertySet : PropertySet<Brush>
    {
        public BrushPropertySet(string name, Func<Brush> getter, Action<Brush> setter) : base(name, getter, setter)
        {
        }
    }
}
