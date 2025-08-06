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

namespace Utilities._2DRendering
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Simple2DRenderer : UserControl
    {
        public Simple2DRenderer()
        {
            InitializeComponent();
        }

        public void Render2DGraph(Dictionary<int, int> xyPoints)
        {
            this.RenderCanvas = RenderGraph(xyPoints);
            this.UpdateDefaultStyle();
            this.UpdateLayout();
        }

        private Canvas RenderGraph(Dictionary<int,int> xyPoints)
        {
            ConcurrentBag<Ellipse> ellipses = new ConcurrentBag<Ellipse>();
            Canvas Canvas = new Canvas();

            Canvas.Width = xyPoints.Keys.Max();
            Canvas.Height = xyPoints.Values.Max();

            Parallel.ForEach(xyPoints, kvp =>
            {
                Ellipse ellipse = new Ellipse
                {
                    Width = 5,
                    Height = 5,
                    Fill = Brushes.Blue
                };

                Canvas.SetLeft(ellipse, kvp.Key);
                Canvas.SetTop(ellipse, kvp.Value);
                ellipses.Add(ellipse);
            });

            foreach(Ellipse ellipse1 in ellipses)
            {
                Canvas.Children.Add(ellipse1);
            }

            return Canvas;
        }
    }
}
