using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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




namespace EE_BesturingsSystemen_Start
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Maaklichten();

            MaakWagens();
            licht1.RoodlichtBrandt();
            licht2.RoodlichtBrandt();
            licht3.GroenlichtBrandt();
            licht4.GroenlichtBrandt();
        }
        /// <summary>
        /// eindopdracht : 3 threads, één voor elke wagen en één voor de verkeerslichten => analoog met Wedstrijd, 3 methoden, 2 beweeg wagens, 1 werking lichten.
        /// Theorie: Processen kunnen meerdere sub-processen (threads) “tegelijkertijd” uitvoeren
        /// Multithreading
        /// Proces = main-thread + sub thread
        /// Thread = “Rode draad” van een reeks instructies. Thread=letterlijk draad.
        /// Principe van de Thread klasse:
        /// Koppel een methode aan een instantie van de Thread klasse !!
        /// Deze methode noemen we de Thread Procedure of ThreadProc.
        /// De methode wordt uitgevoerd in een aparte thread m.b.v.Thread.Start()
        /// </summary>

        Thread WagenHorizontaal;
        Thread WagenVerticaal;
        Thread Verkeerslichten;

        /// <summary>
        /// 3 jun : niet werken met timer, wel teller initiëren => tijd van rijden auto's
        /// </summary>
        private int globaleTeller = 1000;

        private int teller; //initiëren teller.

        /// <summary>
        ///Mag horizontaal rijden?= initiëren met bool
        /// </summary>
        private bool horizontaleWagen;
        /// <summary>
        ///Mag Vertikaal rijden? = initiëren met bool
        /// </summary>
        private bool vertikaleWagen;

        Canvas Wagen1 = new Canvas();
        Canvas Wagen2 = new Canvas();
        Verkeerslicht licht1;
        Verkeerslicht licht2;
        Verkeerslicht licht3;
        Verkeerslicht licht4;

        // Voorkomen van MemoryLeacks bij lopende Threads
       
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            // Voorzie hier de threads die alles opstarten 
            btnStart.IsEnabled = false;
            //fase1: Laat wagen horizontaal rijden
            //WagenHorizontaal = new Thread(BeweegWagenHorizontaal);
            //WagenHorizontaal.Start(Wagen2);

            //fase2: Laat wagen verticaal rijden.
            //WagenVerticaal = new Thread(BeweegWagenVerticaal);
            //WagenVerticaal.Start(Wagen1);
        
            ////Fase3: Werking van de lichten
            Verkeerslichten = new Thread(WerkingVerkeerslichten);
            Verkeerslichten.Start();


        }

        private void btnVoetganger_Click(object sender, RoutedEventArgs e)
        {
            // Voorzie hier de nodige logica om voetgangers/fietsers niet te lang te hoeven wachten
            VoetgangerBepaaltTijd();
        }



        private void BeweegWagenHorizontaal(object Wagen)//(ontvangt parameter object)
        {
            Canvas gekozenWagen = (Canvas)Wagen; //Wagen2
            //Positie horizontale wagen binnen de Canvas.panel 'CnvZone'
            //Wagen2.SetValue(Canvas.LeftProperty, (double)195); Canvas.Left = "195" (afstand van links)
            //Wagen2.SetValue(Canvas.TopProperty, (double)165); Canvas.Top = "165" (afstand van boven)

            int locatie = 195;  //startpositie
            horizontaleWagen = true;
            while (horizontaleWagen)// Fase4A: (Zolang) horzontale wagen (mag) blijven rijden. scenario = wagen rijdt 10 seconden. scenario wagen rijdt 10 seconden. zie 'TijdWagenRijdt()'
            {                           // 3/6 : lus maken voor wagens.

                while (locatie < 390) //code analoog volgens voorbeeld Wedstrijd Fase1. Bewegen auto 'CnvZone Width='515' baanbreedte = '350' Canvas.left ='50'
                                      //WagenContour.Height = 30 - WagenContour.Width = 15 , rectangle wagen
                {
                    locatie = locatie + 5;//beweegt naar rechts met 5px per keer
                    Thread.Sleep(60);//met een pause van 60ms (constant <=> wedstrijd)
                    {
                        Dispatcher.Invoke(delegate ()//voor duidelijkheid F12(Go to definition) -- Delegate representeert één of meerdere methodes met zelfde signatuur
                                                     //"Dispatcher.Invoke Method: Executes the specified delegate synchronously 
                                                     //on the thread the Dispatcher is associated with."
                      {
                            gekozenWagen.SetValue(Canvas.LeftProperty, (double)locatie);
                        });
                    }
                }
                while (locatie > 85) // baan1 width=350
                {
                    locatie = locatie - 5;//beweegt naar links met 5px per keer
                    Thread.Sleep(60);
                    {
                        Dispatcher.Invoke(delegate ()
                        {
                            gekozenWagen.SetValue(Canvas.LeftProperty, (double)locatie);
                        });
                    }
                }
            }
            if (horizontaleWagen == false)// Voorwaarden wanneer horizontale wagen niet meer mag rijden, t.o. zijn startpositie= locatie.
            {
                while (locatie > 195)
                {
                    locatie -= 5;
                    Thread.Sleep(60);
                    Dispatcher.Invoke(delegate ()
                    {
                        gekozenWagen.SetValue(Canvas.LeftProperty, (double)locatie);
                    });
                }

                while (locatie < 195)
                {
                    locatie += 5;
                    Thread.Sleep(60);
                    Dispatcher.Invoke(delegate ()
                    {
                        gekozenWagen.SetValue(Canvas.LeftProperty, (double)locatie);
                    });
                }
            }

        }

        private void BeweegWagenVerticaal(object Wagen)
        {
            Canvas gekozenWagen = (Canvas)Wagen; //Wagen1

            //Positie vertikale wagen:
            //Wagen1.SetValue(Canvas.LeftProperty, (double)230);
            //Wagen1.SetValue(Canvas.TopProperty, (double)190);

            int locatie = 190; //startpositie
            vertikaleWagen = true;
            while (vertikaleWagen) // Fase4B: (Zolang) vertikale wagen (mag) blijven rijden. scenario wagen rijdt 10 seconden zie 'TijdWagenRijdt()'
            {
                while (locatie > 20) //code analoog volgens voorbeeld Wedstrijd =  Fase2. Bewegen auto 'Cnv Height="320" baanhoogte = '300' Canvas.Top ='10'
                                     //WagenContour.Height = 30 - WagenContour.Width = 15 , rectangle wagen
                {
                    locatie = locatie - 5; //beweegt naar boven met 5px per keer
                    Thread.Sleep(60); //met een pause van 60ms (constant <=> wedstrijd)
                    {
                        Dispatcher.Invoke(delegate () //"Dispatcher.Invoke Method: Executes the specified delegate synchronously 
                                                      //on the thread the Dispatcher is associated with."
                        {
                            gekozenWagen.SetValue(Canvas.TopProperty, (double)locatie);//Waarde van wagen binnnen de canvas: Canvas.Top="locatie"
                        });
                    }
                }
                while (locatie < 275) // Baan2 height = 300
                {
                    locatie = locatie + 5; // beweegt naar beneden
                    Thread.Sleep(60);
                    {
                        Dispatcher.Invoke(delegate ()
                        {
                            gekozenWagen.SetValue(Canvas.TopProperty, (double)locatie);
                        });
                    }
                }
            }
            
            if (vertikaleWagen == false)// Voorwaarden wanneer verticale wagen niet meer mag rijden, t.o. zijn startpositie= locatie.
            {
                while (locatie > 190)
                {
                    locatie -= 5;
                    Thread.Sleep(60);
                    Dispatcher.Invoke(delegate ()
                    {
                        gekozenWagen.SetValue(Canvas.TopProperty, (double)locatie);
                    });
                }

                while (locatie < 190)
                {
                    locatie += 5;
                    Thread.Sleep(60);
                    Dispatcher.Invoke(delegate ()
                    {
                        gekozenWagen.SetValue(Canvas.TopProperty, (double)locatie);
                    });
                }

            }

        }
        //private void WerkingVerkeerslichten() //Fase3.
        //{
        //    // Scenario lichten
        //    // Lichten vertikaal schakelen over naar oranje(= 0). lichtenverandering met delegate.

        //     Dispatcher.Invoke(delegate ()
        //    {
        //        licht3.OranjelichtBrandt();
        //        licht4.OranjelichtBrandt();
        //    });

        //    Thread.Sleep(2000);

        //    //Lichten vertikaal schakelen over naar rood (na tijdsinterval van 2"). lichtenverandering met delegate.
        //    Dispatcher.Invoke(delegate ()
        //    {
        //        licht3.RoodlichtBrandt();
        //        licht4.RoodlichtBrandt();
        //    });
        //    Thread.Sleep(2000);


        //    //Lichten horizontaal schakelen over naar groen (na tijdsinterval van 2"). lichtenverandering met delegate.
        //    Dispatcher.Invoke(delegate ()
        //    {
        //        licht1.GroenlichtBrandt();
        //        licht2.GroenlichtBrandt();
        //    });
        //    Thread.Sleep(2000);

        //    //Lichten horizontaal schakelen over naar oranje (na tijdsinterval van 2"). lichtenverandering met delegate.
        //    Dispatcher.Invoke(delegate ()
        //    {
        //        licht1.OranjelichtBrandt();
        //        licht2.OranjelichtBrandt();
        //    });

        //    Thread.Sleep(2000);

        //    //Lichten horizontaal schakelen over naar rood (na tijdsinterval van 2"). lichtenverandering met delegate.
        //    Dispatcher.Invoke(delegate ()
        //    {
        //        licht1.RoodlichtBrandt();
        //        licht2.RoodlichtBrandt();
        //    });
        //    Thread.Sleep(2000);


        //    //Lichten vertikaal schakelen over naar groen (na tijdsinterval van 2"). lichtenverandering met delegate.
        //    Dispatcher.Invoke(delegate ()
        //    {
        //        licht3.GroenlichtBrandt();
        //        licht4.GroenlichtBrandt();
        //    });
        //    Thread.Sleep(2000);


        //}


        private void WerkingVerkeerslichten()
        {
            // 3/6 : lichten met while & teller. teller initiëren. sleep toepassen wanneer lichten veranderen.

            bool zolangApplicatieLoopt = true; //Fase4C: Om de lichten blijvend te doen werken, analoog met auto's blijvend doen rijden.     

            while (zolangApplicatieLoopt) //3/6: Lus maken voor de lichten.

            {
                WagenVerticaal = new Thread(BeweegWagenVerticaal);
                WagenVerticaal.Start(Wagen1);

                TijdWagenRijdt(); //= 10" zonder gebruik Timer-klasse

                //Lichten vertikaal schakelen over naar oranje. lichtenverandering met oproepen delegate.(delegate representeert een of meerdere methoden,
                // voorwaarde zelfde signatuur 
                Dispatcher.Invoke(delegate () 
                {
                    licht3.OranjelichtBrandt();
                    licht4.OranjelichtBrandt();
                });

                vertikaleWagen = false; // Vertikale wagen mag niet langer rijden. Bool op false.
                horizontaleWagen = true; // Horizontale wagen mag straks rijden

                Thread.Sleep(2000); //tijdsinterval van 2000 ms = 2seconden
                                    //Thread.Sleep Method (Int32):Suspends the current thread for the specified number of milliseconds.

                //Lichten vertikaal schakelen over naar rood (na tijdsinterval van 2"). lichtenverandering met delegate.
                Dispatcher.Invoke(delegate ()
                {
                    licht3.RoodlichtBrandt();
                    licht4.RoodlichtBrandt();
                });
                Thread.Sleep(2000);


                //Lichten horizontaal schakelen over naar groen (na tijdsinterval van 2"). lichtenverandering met delegate.
                Dispatcher.Invoke(delegate ()
                {
                    licht1.GroenlichtBrandt();
                    licht2.GroenlichtBrandt();
                });
                Thread.Sleep(2000);

                // Met behulp van een nieuwe thread op WagenHorizontaal kan deze gestart worden
                WagenHorizontaal = new Thread(BeweegWagenHorizontaal);
                WagenHorizontaal.Start(Wagen2);

                TijdWagenRijdt();

                //Lichten vertikaal schakelen over naar oranje. lichtenverandering met oproepen delegate.
                Dispatcher.Invoke(delegate ()
                {
                    licht1.OranjelichtBrandt();
                    licht2.OranjelichtBrandt();
                });

                vertikaleWagen = true;
                horizontaleWagen = false;

                Thread.Sleep(2000);

                //Lichten horizontaal schakelen over naar rood (na tijdsinterval van 2"). lichtenverandering met delegate.
                Dispatcher.Invoke(delegate ()
                {
                    licht1.RoodlichtBrandt();
                    licht2.RoodlichtBrandt();
                });
                Thread.Sleep(2000);


                //Lichten vertikaal schakelen over naar groen (na tijdsinterval van 2"). lichtenverandering met delegate.
                Dispatcher.Invoke(delegate ()
                {
                    licht3.GroenlichtBrandt();
                    licht4.GroenlichtBrandt();
                });
                Thread.Sleep(2000);

                WagenVerticaal = new Thread(BeweegWagenVerticaal);  // Met behulp van een nieuwe thread op WagenVertikaal
                WagenVerticaal.Start(Wagen1);   // kan deze gestart worden

            }

        }


        private void TijdWagenRijdt()
        {
            for ( teller = globaleTeller; teller > 0; teller--)
            {
                Thread.Sleep(10);
            }
        }

        private void VoetgangerBepaaltTijd()
        {
            
            int voetgangerteller = 50; //= verkorten tijd dat wagen mag rijden.
            for ( teller = voetgangerteller; teller > 0; teller--)
            {
                Thread.Sleep(10);
            }

        }


        //Code om wagens te tekenen
        private void MaakWagens()
        {
            Auto Auto1 = new Auto();
            Wagen1 = Auto1.MaakWagen(); //Wagen vertikaal
            Wagen1.SetValue(Canvas.LeftProperty, (double)230);
            Wagen1.SetValue(Canvas.TopProperty, (double)190);
            Wagen1.SetValue(Canvas.ZIndexProperty, (int)2);
            CnvZone.Children.Add(Wagen1);

            Auto Auto2 = new Auto();
            Wagen2 = Auto2.MaakWagen(); //Wagen horizontaal
            RotateTransform rt1 = new RotateTransform((double)90);
            Wagen2.RenderTransform = rt1;
            Wagen2.SetValue(Canvas.LeftProperty, (double)195);
            Wagen2.SetValue(Canvas.TopProperty, (double)165);
            Wagen2.SetValue(Canvas.ZIndexProperty, (int)2);
            CnvZone.Children.Add(Wagen2);



        }


        //Code om verkeerlichten te tekenen op de bestaande Canvas
        private void Maaklichten()
        {
            licht1 = new Verkeerslicht();
            Canvas Verkeerslicht1 = new Canvas();
            Verkeerslicht1 = licht1.Maakverkeerslicht();
            RotateTransform rt1 = new RotateTransform((double)90);
            Verkeerslicht1.RenderTransform = rt1;
            Verkeerslicht1.SetValue(Canvas.LeftProperty, (double)195);
            Verkeerslicht1.SetValue(Canvas.TopProperty, (double)190);

            licht2 = new Verkeerslicht();
            Canvas Verkeerslicht2 = new Canvas();
            Verkeerslicht2 = licht2.Maakverkeerslicht();
            RotateTransform rt2 = new RotateTransform((double)-90);
            Verkeerslicht2.RenderTransform = rt2;
            Verkeerslicht2.SetValue(Canvas.LeftProperty, (double)255);
            Verkeerslicht2.SetValue(Canvas.TopProperty, (double)130);

            licht3 = new Verkeerslicht();
            Canvas Verkeerslicht3 = new Canvas();
            Verkeerslicht3 = licht3.Maakverkeerslicht();
            Verkeerslicht3.SetValue(Canvas.LeftProperty, (double)255);
            Verkeerslicht3.SetValue(Canvas.TopProperty, (double)190);

            licht4 = new Verkeerslicht();
            Canvas Verkeerslicht4 = new Canvas();
            Verkeerslicht4 = licht4.Maakverkeerslicht();
            Verkeerslicht4.SetValue(Canvas.LeftProperty, (double)175);
            Verkeerslicht4.SetValue(Canvas.TopProperty, (double)80);

            Rectangle Baan1 = new Rectangle(); //Horizontaal
            Baan1.Height = 50;
            Baan1.Width = 350;
            Baan1.Fill = new SolidColorBrush(Colors.Gray);
            Baan1.SetValue(Canvas.LeftProperty, (double)50);
            Baan1.SetValue(Canvas.TopProperty, (double)135);
            Baan1.SetValue(Canvas.ZIndexProperty, (int)1);

            Rectangle Baan2 = new Rectangle(); //Vertikaal
            Baan2.Height = 300;
            Baan2.Width = 50;
            Baan2.Fill = new SolidColorBrush(Colors.Gray);
            Baan2.SetValue(Canvas.LeftProperty, (double)200);
            Baan2.SetValue(Canvas.TopProperty, (double)10);

            CnvZone.Children.Add(Verkeerslicht1);
            CnvZone.Children.Add(Verkeerslicht2);
            CnvZone.Children.Add(Verkeerslicht3);
            CnvZone.Children.Add(Verkeerslicht4);
            CnvZone.Children.Add(Baan1);
            CnvZone.Children.Add(Baan2);

        }


        
    }
}

