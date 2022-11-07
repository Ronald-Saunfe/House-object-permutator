using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace object_permutator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        class Point
        {
            public double x, y;
        }

        Object ronaldbed = new Object() { Text = "Ronald_Bed", bgColor = new SolidColorBrush(Colors.LightYellow),Width=202,Height=135 ,
        src= new BitmapImage(new Uri("ms-appx:///Assets/bed.png"))  };
        Object russelbed = new Object() { Text = "Russel_Bed", bgColor = new SolidColorBrush(Colors.DarkKhaki), Width = 204, Height = 195,
            src = new BitmapImage(new Uri("ms-appx:///Assets/bed.png"))
        };
        Object ronaldtable = new Object() { Text = "Ronald_Table", bgColor = new SolidColorBrush(Colors.Yellow), Width = 124, Height = 64,
            src = new BitmapImage(new Uri("ms-appx:///Assets/table.png"))
        };
        Object russeltable = new Object() { Text = "Russel_Table", bgColor = new SolidColorBrush(Colors.Green), Width = 103, Height = 54,
            src = new BitmapImage(new Uri("ms-appx:///Assets/table.png"))
        };
        Object house = new Object() { Text = "House", bgColor = new SolidColorBrush(Colors.Brown), Width = 341, Height = 399 };

        Random rand = new Random();

        public MainPage()
        {
            this.InitializeComponent();
            cv.Children.Add(house); 
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            startPermutation();
        }

        private async Task startPermutation()
        {

            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                cv.Children.Clear();
                lv.Items.Clear();
                cv.Children.Add(house);
                cv.Children.Add(ronaldbed);
                cv.Children.Add(russelbed);
                cv.Children.Add(russeltable);
                cv.Children.Add(ronaldtable);
            });
            switch (rand.Next(1, 4))
            {
                case 1:
                    await UpdateObjectConfiguration(russeltable);
                    break;
                case 2:
                    await UpdateObjectConfiguration(ronaldtable);
                    break;
                case 3:
                    await UpdateObjectConfiguration(ronaldbed);
                    break;
                case 4:
                    await UpdateObjectConfiguration(russelbed);
                    break;
            }

            // place the object in random positions until
            for (int i = 0; i < cv.Children.Count; i++)
            {
                Object o1 = (Object)cv.Children[i];
                if (o1.Text.Equals("House"))
                {
                    continue;
                }
                for (int j = 0; j < cv.Children.Count; j++)
                {
                    Object o2 = (Object)cv.Children[j]; 
                    if (o2.Text.Equals("House"))
                    {
                        continue;
                    }
                    if (o2.Text.Equals(o1.Text))
                    {
                        continue;
                    }
                    await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        lv.Items.Add("INTERSECTION PREVENTION: "+o1.Text+" with "+o2.Text);
                    });
                    while (await intersect(o1, o2))
                    {
                        await UpdateObjectConfiguration(o1);
                    }
                }
                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    lv.Items.Add("SOLUTION: "+o1.Text+" position ("+Canvas.GetLeft(o1)+","+ Canvas.GetTop(o1)+")");
                });
                await Task.Delay(1);
            }

            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                lv.Items.Add("SOLUTION: SUCCESSFULLY FOUND!");
            });
        }

        async Task UpdateObjectConfiguration(Object o1)
        {
            int x, y;
            x = rand.Next(0, (int)(341 -o1.Width));
            y = rand.Next(0, (int)(399 -o1.Height));
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                Canvas.SetLeft(o1, x);
                Canvas.SetTop(o1, y);

            });
            if (o1.spinangle() == 90)
            {
                o1.rotate0();
            }
            else if(o1.spinangle() == 0)
            {
                o1.rotate90();
            }

            await Task.Delay(1);
        }


        async Task<bool> intersect(Object o1, Object o2)
        {
            Point l1 = new Point(), r1 = new Point(), l2 = new Point(), r2 = new Point();
            l1.x = Canvas.GetLeft(o1);
            l1.y = Canvas.GetTop(o1);
            r1.x = l1.x + o1.Width;
            r1.y = l1.y + o1.Height;

            l2.x = Canvas.GetLeft(o2);
            l2.y = Canvas.GetTop(o2);
            r2.x = l2.x + o2.Width;
            r2.y = l2.y + o2.Height;


            if ((l1.x < r2.x) && (r1.x > l2.x) &&
               (l1.y < r2.y) &&
                 (r1.y > l2.y))
            {
                Debug.WriteLine("=======intersects===");
                return true;
            }
            else
            {
                Debug.WriteLine("=======intersects===");
                return false;

            }
        }
    }
}
