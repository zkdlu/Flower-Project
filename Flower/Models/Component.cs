using System.Windows.Media;

namespace Flower.Models
{
    class Component
    {
        public double Width { get; set; } = 100;

        public double Height { get; set; } = 100;

        public double X { get; set; } = 100;

        public double Y { get; set; } = 100;

        public Brush Background { get; set; } = Brushes.Red;

        public string Content { get; set; } = "TEXT";

        public bool IsSelected { get; set; }

        public bool CanVResize { get; set; }

        public bool CanHResize { get; set; }
    }
}
