using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace SolarPark.DrawSolarPark
{
    public static class DrawPark
    {
        public static List<(int, int)> Return_Park_Coordinates()
        {
            return new List<(int, int)>() 
            {
                (83,136),
                (124,186),
                (252,155),
                (277,47),
                (183,82),
                (163,4),
                (80,25),
                (83,136)
            };
        }

        public static List<(int, int)> Return_Restriction_Coordinates()
        {
            return  new List<(int, int)>()
            {
                (173,123),
                (182,143),
                (145,147),
                (134,121)
            };
        }

        public static List<RectangleGeometry> Draw_Panels_On_Park(Canvas drawingCanvas, Path solarParkShape, Path restrictionZone, Rectangle panel, double columnSpacing, double rowSpacing, double tiltAngle)
        {
            var topLeftPoint = solarParkShape.Data.Bounds.TopLeft;

            var listOfPanels = new List<RectangleGeometry>();

            //Start at topLeftPoint
            var drawingPoint = topLeftPoint;

            // Set to 1, but will be set to rowspacing and columnspacing once first panel placed in new row
            double moveY = 1;
            double moveX = 1;

            //Left to 1, since smaller values would take forever to load :)
            while(drawingPoint.Y <= solarParkShape.Data.Bounds.BottomRight.Y)
            {
                //-------------------------------------
                drawingPoint.X = topLeftPoint.X;
                moveX = 1;
                double offSetY = 0;

                while (drawingPoint.X <= solarParkShape.Data.Bounds.TopRight.X)
                {
                    var placingPoint = new Point(drawingPoint.X, drawingPoint.Y);


                    var newPanel = Create_Panel(panel, placingPoint, tiltAngle);

                    if (solarParkShape.RenderedGeometry.FillContains(newPanel) && restrictionZone.RenderedGeometry.FillContainsWithDetail(newPanel) == IntersectionDetail.Empty)
                    {
                        Path panelPath = new Path();
                        panelPath.Data = newPanel;
                        panelPath.Stroke = Brushes.Blue;

                        drawingCanvas.Children.Add(panelPath);
                        listOfPanels.Add(newPanel);

                        drawingPoint.Offset(newPanel.Bounds.Width, 0);
                        offSetY = newPanel.Bounds.Height;

                        moveY = rowSpacing;
                        moveX = columnSpacing;
                    }

                    //-----------------------------------
                    drawingPoint.Offset(moveX, 0);
                }
                //-------------------------------------
                drawingPoint.Offset(0, moveY + offSetY);
            }

            return listOfPanels;
        }

        public static Path Create_Polygon_Park_Shape_Geometries(Canvas drawingCanvas, List<(int, int)> zoneCoordinates, SolidColorBrush color)
        {
            var zonePG = new PathGeometry();
            var zonePf = new PathFigure();

            var parkCoordinates = zoneCoordinates;
            var coordinatePoints = Return_Point_Collection(parkCoordinates);

            var solarParkMinXPoint = coordinatePoints.Min(s => s.X);
            zonePf.StartPoint = coordinatePoints.Where(s => s.X == solarParkMinXPoint).First();

            foreach (var polygon in coordinatePoints)
            {
                zonePf.Segments.Add(new LineSegment(polygon, true));
            }

            zonePG.Figures.Add(zonePf);
            Path p1 = new Path();
            p1.Data = zonePG;
            p1.Stroke = color;

            drawingCanvas.Children.Add(p1);

            return p1;
        }

        //Pythagor's theorem and trigonometry
        private static Rect Rotate_Solar_Panel(Rectangle panel, double tiltAngle, Point placingPoint)
        {
            double c = panel.Height;
            double a = 0;
            double b = 0;

            //Need to multiply by Pi/180 to convert degrees to radians
            a = Math.Abs(Math.Sin(tiltAngle * (Math.PI / 180)) * c);

            b = Math.Sqrt(Math.Pow(c, 2) - Math.Pow(a, 2));

            Rect square = new Rect(placingPoint.X, placingPoint.Y, panel.Width, b);

            return square;
        }

        private static RectangleGeometry Create_Panel(Rectangle panel, Point placingPoint, double tiltAngle)
        {
            var square = Rotate_Solar_Panel(panel, tiltAngle, placingPoint);

            var eg1 = new RectangleGeometry(square);

            return eg1;
        }

        private static PointCollection Return_Point_Collection(List<(int, int)> coordinates)
        {
            var parkPoints = coordinates.Select(s => new Point
            {
                X = s.Item1,
                Y = s.Item2
            });

            return new PointCollection(parkPoints);

            //Leave this for later for the rectangles
            //SolarParkShape.RenderTransform = new RotateTransform(45);
        }
    }

}
