using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EE_BesturingsSystemen_Start
{
    class Verkeerslicht
    {
        Rectangle Omkadering;
        Ellipse Roodlicht;
        Ellipse Oranjelicht;
        Ellipse Groenlicht;

      


        public Canvas Maakverkeerslicht()
        {

            Canvas Verkeerslicht;
            Omkadering = new Rectangle();
            Verkeerslicht = new Canvas();
            Verkeerslicht.Width = 20;
            Verkeerslicht.Height = 50;



            Groenlicht = new Ellipse();
            Groenlicht.Stroke = new SolidColorBrush(Colors.Green);
            Groenlicht.Width = 10;
            Groenlicht.Height = 10;
            Groenlicht.SetValue(Canvas.ZIndexProperty, (int)1);
            Groenlicht.SetValue(Canvas.LeftProperty, (double)5);
            Groenlicht.SetValue(Canvas.TopProperty, (double)35);
            Verkeerslicht.Children.Add(Groenlicht);

            Oranjelicht = new Ellipse();
            Oranjelicht.Stroke = new SolidColorBrush(Colors.Orange);
            Oranjelicht.Width = 10;
            Oranjelicht.Height = 10;
            Oranjelicht.SetValue(Canvas.ZIndexProperty, (int)1);
            Oranjelicht.SetValue(Canvas.LeftProperty, (double)5);
            Oranjelicht.SetValue(Canvas.TopProperty, (double)20);
            Verkeerslicht.Children.Add(Oranjelicht);

            Roodlicht = new Ellipse();
            Roodlicht.Stroke = new SolidColorBrush(Colors.Red);
            Roodlicht.Width = 10;
            Roodlicht.Height = 10;
            Roodlicht.SetValue(Canvas.ZIndexProperty, (int)1);
            Roodlicht.SetValue(Canvas.LeftProperty, (double)5);
            Roodlicht.SetValue(Canvas.TopProperty, (double)5);
            Verkeerslicht.Children.Add(Roodlicht);

            Omkadering.Height = 50;
            Omkadering.Width = 20;
            Omkadering.Stroke = new SolidColorBrush(Colors.Black);
            Omkadering.StrokeThickness = 2;
            Omkadering.SetValue(Canvas.ZIndexProperty, (int)0);
            Omkadering.SetValue(Canvas.LeftProperty, (double)0);
            Omkadering.SetValue(Canvas.TopProperty, (double)0);
            Verkeerslicht.Background = new SolidColorBrush(Colors.LightGray);

            Verkeerslicht.Children.Add(Omkadering);

            return Verkeerslicht;


        }

        public void RoodlichtBrandt()
        {
            Roodlicht.Fill = new SolidColorBrush(Colors.Red);
            Oranjelicht.Fill = new SolidColorBrush(Colors.LightGray);
            Groenlicht.Fill = new SolidColorBrush(Colors.LightGray);
        }

        public void OranjelichtBrandt()
        {

            Roodlicht.Fill = new SolidColorBrush(Colors.LightGray);
            Groenlicht.Fill = new SolidColorBrush(Colors.LightGray);
            Oranjelicht.Fill = new SolidColorBrush(Colors.Orange);
        }
        public void GroenlichtBrandt()
        {

            Oranjelicht.Fill = new SolidColorBrush(Colors.LightGray);
            Roodlicht.Fill = new SolidColorBrush(Colors.LightGray);
            Groenlicht.Fill = new SolidColorBrush(Colors.Green);
        }

    }
}
