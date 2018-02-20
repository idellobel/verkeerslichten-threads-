using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EE_BesturingsSystemen_Start
{
    public class Auto
    {
        Rectangle WagenContour;



        public Canvas MaakWagen()
        {

            Canvas Wagen;
            WagenContour = new Rectangle();
            Wagen = new Canvas();


            WagenContour.Height = 30;
            WagenContour.Width = 15;
            WagenContour.Stroke = new SolidColorBrush(Colors.Black);
            WagenContour.StrokeThickness = 2;
            WagenContour.Fill = new SolidColorBrush(Colors.Green);
            WagenContour.SetValue(Canvas.ZIndexProperty, (int)0);
            WagenContour.SetValue(Canvas.LeftProperty, (double)0);
            WagenContour.SetValue(Canvas.TopProperty, (double)0);

            Wagen.Children.Add(WagenContour);

            return Wagen;


        }
    }
}
