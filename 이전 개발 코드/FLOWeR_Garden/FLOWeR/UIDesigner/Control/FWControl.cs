using System;
using System.ComponentModel;
using System.Windows;

namespace FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Control
{
    /// <summary>
    /// FLOWeR custom control
    /// </summary>
    [Serializable]
    public abstract class FWControl : INotifyPropertyChanged
    {
        private string name;
        private double left;
        private double top;
        private double width;
        private double height;

        [PropertyAttribute]
        [CategoryAttribute("디자인"), DescriptionAttribute("컨트롤의 이름"), DisplayNameAttribute("(Name)")]
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

        [CategoryAttribute("레이아웃"), DescriptionAttribute("Left"), DisplayNameAttribute("Left")]
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

        [CategoryAttribute("레이아웃"), DescriptionAttribute("Top"), DisplayNameAttribute("Top")]
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

        [CategoryAttribute("레이아웃"), DescriptionAttribute("컨트롤의 넓이"), DisplayNameAttribute("Width")]
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

        [CategoryAttribute("레이아웃"), DescriptionAttribute("컨트롤의 높이"), DisplayNameAttribute("Height")]
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

        [CategoryAttribute("레이아웃"), DescriptionAttribute("컨트롤의 위치"), DisplayNameAttribute("Location")]
        public Point Location
        {
            get
            {
                return new Point(Left, Top);
            }
        }

        [CategoryAttribute("레이아웃"), DescriptionAttribute("컨트롤의 사이즈"), DisplayNameAttribute("Size")]
        public Size Size
        {
            get
            {
                return new Size(Width, Height);                 
            }
        }

        public virtual string TypeName
        {
            get;
        }

        public FWControl()
        {
            FWEventList = new FWEventList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region
        public FWEventList FWEventList
        {
            get;
            set;
        }

        [Browsable(false)]
        public string Click { get; set; }
        [Browsable(false)]
        public string MouseClick { get; set; }
        [Browsable(false)]
        public string MouseDown { get; set; }
        [Browsable(false)]
        public string MouseEnter { get; set; }
        [Browsable(false)]
        public string MouseHover { get; set; }
        [Browsable(false)]
        public string MouseLeave { get; set; }
        [Browsable(false)]
        public string MouseMove { get; set; }
        [Browsable(false)]
        public string MouseUp { get; set; }
        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}
