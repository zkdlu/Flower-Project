using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol.Window;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol
{
    public class FWLine : FWSymbol
    {
        public FWLine()
        {
            Name = "FlowLine";

            Line = new Path();
            IsSelected = false;
            Thickness = 2;

            SymbolActionWindow = new FWLineWindow(this);
        }

        public Path Line
        {
            get;
            set;
        }

        public bool IsSelected
        {
            get;
            set;
        }

        public int Thickness
        {
            get;
            set;
        }

        public bool FlowTypeTrueOrFalse
        {
            get;
            set;
        }

        public void DrawLine(Point pt1, Point pt2)
        {
            GeometryGroup lineGroup = new GeometryGroup();
            double theta = Math.Atan2((pt2.Y - pt1.Y), (pt2.X - pt1.X)) * 180 / Math.PI;

            PathGeometry pathGeometry = new PathGeometry();

            PathFigure pathFigure = new PathFigure();
            Point p = new Point(pt2.X, pt2.Y);
            pathFigure.StartPoint = p;

            Point lpoint = new Point(p.X + 6, p.Y + 15);
            Point rpoint = new Point(p.X - 6, p.Y + 15);
            LineSegment seg1 = new LineSegment();
            seg1.Point = lpoint;
            pathFigure.Segments.Add(seg1);

            LineSegment seg2 = new LineSegment();
            seg2.Point = rpoint;
            pathFigure.Segments.Add(seg2);

            pathGeometry.Figures.Add(pathFigure);

            RotateTransform transform = new RotateTransform();
            transform.Angle = theta + 90;
            transform.CenterX = p.X;
            transform.CenterY = p.Y;
            pathGeometry.Transform = transform;
            lineGroup.Children.Add(pathGeometry);

            LineGeometry connectorGeometry = new LineGeometry();
            connectorGeometry.StartPoint = pt1;
            connectorGeometry.EndPoint = pt2;
            lineGroup.Children.Add(connectorGeometry);

            Line.Data = lineGroup;
            Line.StrokeThickness = Thickness;
            Line.Stroke = Line.Fill = Brushes.Black;
        }
    }
}
