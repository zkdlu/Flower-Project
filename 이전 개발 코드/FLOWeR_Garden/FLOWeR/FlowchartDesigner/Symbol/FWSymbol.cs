using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol
{
    public abstract class FWSymbol : INotifyPropertyChanged
    {
        public FWSymbol()
        {

        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public Shape SymbolShape
        {
            get;
            set;
        }

        public FWSymbol StartConnector
        {
            get;
            set;
        }

        public FWSymbol EndConnector
        {
            get;
            set;
        }

        public Point[] RectPoints
        {
            get
            {
                return new Point[4]
                {
                    new Point(Left, Top + Height / 2),
                    new Point(Left + Width / 2, Top),
                    new Point(Left + Width, Top + Height / 2),
                    new Point(Left + Width / 2, Top + Height)
                };
            }
        }

        private string name;
        private double left;
        private double top;
        private double width;
        private double height;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public double Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
                OnPropertyChanged("Left");
            }
        }
        public double Top
        {
            get
            {
                return top;
            }
            set
            {
                top = value;
                OnPropertyChanged("Top");
            }
        }
        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                OnPropertyChanged("Width");
            }
        }
        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                OnPropertyChanged("Height");
            }
        }

        public UserControl SymbolActionWindow
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
