using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Component
{
    /// <summary>
    /// FlowComponent.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FlowComponent : UserControl, INotifyPropertyChanged
    {
        private static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(FlowComponent), new FrameworkPropertyMetadata(false));

        private readonly double minHeight;
        private readonly double minWidth;

        private readonly double maxHeight;
        private readonly double maxWidth;

        public bool CanVResize
        {
            get;
            private set;
        }

        public bool CanHResize
        {
            get;
            private set;
        }

        public new object Content
        {
            get
            {
                return contentPresenter.Content;
            }
            protected set
            {
                Shape shape = (Shape)value;

                contentGrid.Children.Insert(0, shape);
            }
        }

        public bool IsSelected
        {
            get
            {
                return (bool)GetValue(IsSelectedProperty);
            }
            set
            {
                SetValue(IsSelectedProperty, value);
            }
        }

        public Point[] RectPoints
        {
            get
            {
                return FWSymbol.RectPoints;
            }
        }

        public FWSymbol FWSymbol
        {
            get;
            private set;
        }

        public FlowchartDesigner FlowchartDesigner
        {
            get;
            set;
        }

        public FlowComponent(FWSymbol fwSymbol, FlowchartDesigner flowchartDesigner)
        {
            InitializeComponent();

            FlowchartDesigner = flowchartDesigner;

            Shape content = fwSymbol.SymbolShape;
            content.Stretch = Stretch.Fill;

            //최소값 최대값 설정 확인
            minWidth = content.MinWidth < 10.0 ? 10.0 : content.MinWidth;
            minHeight = content.MinHeight < 10.0 ? 10.0 : content.MinHeight;
            maxWidth = content.MaxWidth;
            maxHeight = content.MaxHeight;

            CanHResize = true;
            CanVResize = true;

            this.Width = fwSymbol.Width;
            this.Height = fwSymbol.Height;

            SetValue(Canvas.TopProperty, fwSymbol.Top);
            SetValue(Canvas.LeftProperty, fwSymbol.Left);

            this.Content = content;
            this.FWSymbol = fwSymbol;

            this.DataContext = fwSymbol;

            FWSymbol.PropertyChanged += FWSymbol_PropertyChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged = null;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FWSymbol_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        public FlowComponent(FWLine fwLine, FlowchartDesigner flowchartDesigner)
        {
            InitializeComponent();

            FlowchartDesigner = flowchartDesigner;

            SetValue(Canvas.TopProperty, fwLine.Top);
            SetValue(Canvas.LeftProperty, fwLine.Left);

            this.Content = fwLine.Line;
            this.FWSymbol = fwLine;

            HideSelection();

            this.MouseDown += FlowComponent_MouseDown;
        }

        private void FlowComponent_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FWLine fwLine = FWSymbol as FWLine;
            fwLine.IsSelected = true;
            fwLine.Thickness = 3;

            FlowchartDesigner.ReconnectLine();

            FlowManager.Instance.ActionWindow.ChangePanel(fwLine.SymbolActionWindow);
        }

        private void HideSelection()
        {
            SelectionThumb.Visibility = Visibility.Hidden;
            TopLeftThumb.Visibility = Visibility.Hidden;
            TopThumb.Visibility = Visibility.Hidden;
            TopRightThumb.Visibility = Visibility.Hidden;

            LeftThumb.Visibility = Visibility.Hidden;
            RightThumb.Visibility = Visibility.Hidden;

            BottomLeftThumb.Visibility = Visibility.Hidden;
            BottomThumb.Visibility = Visibility.Hidden;
            BottomRightThumb.Visibility = Visibility.Hidden;
        }

        private void SelectionThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            FWSymbol.Left = (double)GetValue(Canvas.LeftProperty) + e.HorizontalChange;
            FWSymbol.Top = (double)GetValue(Canvas.TopProperty) + e.VerticalChange;

            SetValue(Canvas.TopProperty, FWSymbol.Top);
            SetValue(Canvas.LeftProperty, FWSymbol.Left);
        }

        private void ResizeThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            string name = ((Thumb)sender).Name;

            if (name.Contains("Top"))
            {
                double newHeight = this.Height - e.VerticalChange;
                if (newHeight >= minHeight && newHeight <= maxHeight)
                {
                    this.Height = newHeight;
                    SetValue(Canvas.TopProperty, (double)GetValue(Canvas.TopProperty) + e.VerticalChange);
                }
            }

            if (name.Contains("Right"))

            {
                double newWidth = this.Width + e.HorizontalChange;
                if (newWidth >= minWidth && newWidth <= maxWidth)
                {
                    this.Width = newWidth;
                }
            }

            if (name.Contains("Bottom"))
            {
                double newHeight = this.Height + e.VerticalChange;
                if (newHeight >= minHeight && newHeight <= maxHeight)
                {
                    this.Height = newHeight;
                }
            }

            if (name.Contains("Left"))
            {
                double newWidth = this.Width - e.HorizontalChange;
                if (newWidth >= minWidth && newWidth <= maxWidth)
                {
                    this.Width = newWidth;
                    SetValue(Canvas.LeftProperty, (double)GetValue(Canvas.LeftProperty) + e.HorizontalChange);
                }
            }

            FWSymbol.Width = this.Width;
            FWSymbol.Height = this.Height;
        }

        private void Grid_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FlowManager.Instance.ActionWindow.ChangePanel(FWSymbol.SymbolActionWindow);

            IsSelected = true;
        }
    }
}
