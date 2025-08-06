using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ParallelismLab._2DRendering
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Simple2DRenderer : UserControl
    {
        double graphZeroX;
        double graphZeroY;

        public Simple2DRenderer()
        {
            InitializeComponent();
        }

        public void Render2DGraph(Dictionary<int, int> xyPoints)
        {
            this.RenderGrid.Children.Add(RenderGraph(xyPoints));

            this.UpdateDefaultStyle();
            this.UpdateLayout();
            this.InvalidateVisual();
            this.InvalidateArrange();
        }

        private Canvas RenderGraph(Dictionary<int,int> xyPoints)
        {
            ConcurrentBag<Ellipse> ellipses = new ConcurrentBag<Ellipse>();
            Canvas GraphCanvas = new Canvas();
            Ellipse ellipseTemp;

            GraphCanvas.Width = this.RenderGrid.Width;
            GraphCanvas.Height = this.RenderGrid.Height;

            this.graphZeroX = GraphCanvas.Width / 2;
            this.graphZeroY = GraphCanvas.Height / 2;

            foreach (KeyValuePair<int,int> kvp in xyPoints)
            {
                ellipseTemp = new Ellipse
                {
                    Width = 5,
                    Height = 5,
                    Fill = Brushes.Blue,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };
                //TODO: Clamp/Lerp this so everything stays on the graph/canvas.
                Canvas.SetLeft(ellipseTemp, graphZeroX + kvp.Key);
                Canvas.SetTop(ellipseTemp, graphZeroY - kvp.Value);
                Canvas.SetZIndex(ellipseTemp, 0);

                GraphCanvas.Children.Add(ellipseTemp);
            }

            //Draw DEBUG ellipses
            ellipseTemp = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };

            Canvas.SetLeft(ellipseTemp, 0);
            Canvas.SetTop(ellipseTemp, 0);
            Canvas.SetZIndex(ellipseTemp, 1);
            GraphCanvas.Children.Add(ellipseTemp);

            ellipseTemp = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Green,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };

            Canvas.SetLeft(ellipseTemp, GraphCanvas.Width);
            Canvas.SetTop(ellipseTemp, 0);
            Canvas.SetZIndex(ellipseTemp, 1);
            GraphCanvas.Children.Add(ellipseTemp);

            ellipseTemp = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Yellow,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };

            Canvas.SetLeft(ellipseTemp, 0);
            Canvas.SetTop(ellipseTemp, GraphCanvas.Height);
            Canvas.SetZIndex(ellipseTemp, 1);
            GraphCanvas.Children.Add(ellipseTemp);

            ellipseTemp = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Orange,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };

            Canvas.SetLeft(ellipseTemp, GraphCanvas.Width);
            Canvas.SetTop(ellipseTemp, GraphCanvas.Height);
            Canvas.SetZIndex(ellipseTemp, 1);
            GraphCanvas.Children.Add(ellipseTemp);

            return GraphCanvas;
        }
    }
}
