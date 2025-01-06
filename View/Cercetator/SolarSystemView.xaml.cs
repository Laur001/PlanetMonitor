using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Cosmos.View.Cercetator
{
    public partial class SolarSystemView : UserControl
    {
        public SolarSystemView()
        {
            InitializeComponent();
            StartAnimations();
        }

        private void StartAnimations()
        {
            AnimatePlanet(MercuryTransform, 60, 60, 5);   // Mercury
            AnimatePlanet(VenusTransform, 90, 90, 8);     // Venus
            AnimatePlanet(EarthTransform, 120, 120, 10);  // Earth
            AnimatePlanet(MarsTransform, 150, 150, 12);   // Mars
            AnimatePlanet(JupiterTransform, 200, 200, 15); // Jupiter
            AnimatePlanet(SaturnTransform, 250, 250, 20); // Saturn
            AnimatePlanet(UranusTransform, 300, 300, 25); // Uranus
            AnimatePlanet(NeptuneTransform, 350, 350, 30); // Neptune
            AnimatePlanet(PlutoTransform, 400, 400, 35);  // Pluto
        }

        private void AnimatePlanet(TranslateTransform transform, double radiusX, double radiusY, double durationInSeconds)
        {
            var centerX = 500; // Center of the canvas on X-axis
            var centerY = 400; // Center of the canvas on Y-axis

            // Create the elliptical path geometry
            var path = new PathGeometry();
            var figure = new PathFigure { StartPoint = new System.Windows.Point(centerX + radiusX, centerY) };

            // Add two arcs to complete the ellipse
            figure.Segments.Add(new ArcSegment
            {
                Point = new System.Windows.Point(centerX - radiusX, centerY),
                Size = new System.Windows.Size(radiusX, radiusY),
                IsLargeArc = true,
                SweepDirection = SweepDirection.Clockwise
            });
            figure.Segments.Add(new ArcSegment
            {
                Point = new System.Windows.Point(centerX + radiusX, centerY),
                Size = new System.Windows.Size(radiusX, radiusY),
                IsLargeArc = true,
                SweepDirection = SweepDirection.Clockwise
            });

            path.Figures.Add(figure);

            // Create the X-axis animation
            var animationX = new DoubleAnimationUsingPath
            {
                PathGeometry = path,
                Duration = TimeSpan.FromSeconds(durationInSeconds),
                RepeatBehavior = RepeatBehavior.Forever,
                Source = PathAnimationSource.X
            };

            // Create the Y-axis animation
            var animationY = new DoubleAnimationUsingPath
            {
                PathGeometry = path,
                Duration = TimeSpan.FromSeconds(durationInSeconds),
                RepeatBehavior = RepeatBehavior.Forever,
                Source = PathAnimationSource.Y
            };

            // Start the animations
            transform.BeginAnimation(TranslateTransform.XProperty, animationX);
            transform.BeginAnimation(TranslateTransform.YProperty, animationY);
        }
    }
}
