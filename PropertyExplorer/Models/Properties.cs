using System;
using System.Windows.Media;

namespace PropertyExplorer.Models
{
    public class DoubleProperty : Property<double>
    {
        public DoubleProperty(string name, Func<double> getter, Action<double> setter) : base(name, getter, setter)
        {
        }
    }

    public class BrushProperty : Property<Brush>
    {
        public BrushProperty(string name, Func<Brush> getter, Action<Brush> setter) : base(name, getter, setter)
        {
        }
    }
}
