using Flower.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Flower.ViewModels
{
    class ComponentViewModel : BaseViewModel
    {
        const int MIN_SIZE = 50;
        const int MAX_SIZE = 500;

        public Component Component { get; set; }

        public double Width 
        {
            get { return Component.Width; }
            set
            {
                Component.Width = value;
                OnRaiseProperty(nameof(Width));
            }
        }

        public double Height
        {
            get { return Component.Height; }
            set
            {
                Component.Height = value;
                OnRaiseProperty(nameof(Height));
            }
        }

        public Thickness Location
        {
            get { return new Thickness(Component.X, Component.Y, 0, 0); }
            set
            {
                Component.X = value.Left;
                Component.Y = value.Top;

                OnRaiseProperty(nameof(Location));
            }
        }

        public double X
        {
            get { return Component.X; }
            set
            {
                Component.X = value;
                OnRaiseProperty(nameof(X));
            }
        }

        public double Y
        {
            get { return Component.Y; }
            set
            {
                Component.Y = value;
                OnRaiseProperty(nameof(Y));
            }
        }

        public Brush Background
        {
            get { return Component.Background; }
            set
            {
                Component.Background = value;
                OnRaiseProperty(nameof(Background));
            }
        }

        public string Content
        {
            get { return Component.Content; }
            set
            {
                Component.Content = value;
                OnRaiseProperty(nameof(Content));
            }
        }

        public bool IsSelected
        {
            get { return Component.IsSelected; }
            set
            {
                Component.IsSelected = value;
                OnRaiseProperty(nameof(IsSelected));
            }
        }

        public bool CanVResize
        {
            get { return Component.CanVResize; }
            set
            {
                Component.CanVResize = value;
                OnRaiseProperty(nameof(CanVResize));
            }
        }

        public bool CanHResize
        {
            get { return Component.CanHResize; }
            set
            {
                Component.CanHResize = value;
                OnRaiseProperty(nameof(CanHResize));
            }
        }

        private ICommand selectCommand;
        public ICommand SelectCommand
        {
            get
            {
                if (selectCommand == null)
                {
                    selectCommand = new RelayCommand(SelectComponent);
                }

                return selectCommand;
            }
        }

        private void SelectComponent()
        {
            IsSelected = !IsSelected;
        }

        private ICommand moveCommand;
        public ICommand MoveCommand
        {
            get
            {
                if (moveCommand == null)
                {
                    moveCommand = new RelayCommand<DragDeltaEventArgs>(MoveComponent);
                }

                return moveCommand;
            }
        }

        private void MoveComponent(DragDeltaEventArgs e)
        {
            X += e.HorizontalChange;
            Y += e.VerticalChange;
        }

        public ICommand ResizeTopLeftCommand { get; private set; }
        public ICommand ResizeTopCommand { get; private set; }
        public ICommand ResizeTopRightCommand { get; private set; }

        public ICommand ResizeLeftCommand { get; private set; }
        public ICommand ResizeRightCommand { get; private set; }

        public ICommand ResizeBottomLeftCommand { get; private set; }
        public ICommand ResizeBottomCommand { get; private set; }
        public ICommand ResizeBottomRightCommand { get; private set; }

        private void ResizeTop(DragDeltaEventArgs e)
        {
            double newHeight = Height - e.VerticalChange;
            if (newHeight >= MIN_SIZE)
            {
                Height = newHeight;
                Y = Y + e.VerticalChange;
            }
        }

        private void ResizeBottom(DragDeltaEventArgs e)
        {
            double newHeight = Height + e.VerticalChange;
            if (newHeight >= MIN_SIZE)
            {
                Height = newHeight;
            }

        }

        private void ResizeLeft(DragDeltaEventArgs e)
        {
            double newWidth = Width - e.HorizontalChange;
            if (newWidth >= MIN_SIZE)
            {
                Width = newWidth;
                X = X + e.HorizontalChange;
            }
        }

        private void ResizeRight(DragDeltaEventArgs e)
        {
            double newWidth = Width + e.HorizontalChange;
            if (newWidth >= MIN_SIZE)
            {
                Width = newWidth;
            }
        }

        private void ResizeTopLeftComponent(DragDeltaEventArgs e)
        {
            ResizeTop(e);
            ResizeLeft(e);
        }

        private void ResizeTopComponent(DragDeltaEventArgs e)
        {
            ResizeTop(e);
        }

        private void ResizeTopRightComponent(DragDeltaEventArgs e)
        {
            ResizeTop(e);
            ResizeRight(e);
        }

        private void ResizeLeftComponent(DragDeltaEventArgs e)
        {
            ResizeLeft(e);
        }

        private void ResizeRightComponent(DragDeltaEventArgs e)
        {
            ResizeRight(e);
        }

        private void ResizeBottomLeftComponent(DragDeltaEventArgs e)
        {
            ResizeBottom(e);
            ResizeLeft(e);
        }

        private void ResizeBottomComponent(DragDeltaEventArgs e)
        {
            ResizeBottom(e);
        }

        private void ResizeBottomRightComponent(DragDeltaEventArgs e)
        {
            ResizeBottom(e);
            ResizeRight(e);
        }

        public ComponentViewModel(Component component)
        {
            Component = component;

            ResizeTopLeftCommand = new RelayCommand<DragDeltaEventArgs>(ResizeTopLeftComponent);
            ResizeTopCommand = new RelayCommand<DragDeltaEventArgs>(ResizeTopComponent);
            ResizeTopRightCommand = new RelayCommand<DragDeltaEventArgs>(ResizeTopRightComponent);

            ResizeLeftCommand = new RelayCommand<DragDeltaEventArgs>(ResizeLeftComponent);
            ResizeRightCommand = new RelayCommand<DragDeltaEventArgs>(ResizeRightComponent);

            ResizeBottomLeftCommand = new RelayCommand<DragDeltaEventArgs>(ResizeBottomLeftComponent);
            ResizeBottomCommand = new RelayCommand<DragDeltaEventArgs>(ResizeBottomComponent);
            ResizeBottomRightCommand = new RelayCommand<DragDeltaEventArgs>(ResizeBottomRightComponent);
        }
    }
}
