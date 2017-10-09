using Microsoft.Kinect;
using Microsoft.Kinect.Wpf.Controls;
using System.Windows;

namespace WpfKinectV2CustomButton
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            KinectRegion.SetKinectRegion(this, kinectRegion);
            kinectRegion.KinectSensor = KinectSensor.GetDefault();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                KinectV2CustomButton button1 = new KinectV2CustomButton();
                button1.Name = "Button_" + i;
                button1.Content = "button " + i;
                button1.Margin = new Thickness(10);
                button1.Width = 150;
                button1.Height = 150;
                button1.HandPointerEnter += Button1_HandPointerEnter;
                button1.HandPointerLeave += Button1_HandPointerLeave;
                KinectButtons.Children.Add(button1);
            }
        }
        private void Button1_HandPointerEnter(object sender, System.EventArgs e)
        {
            txtMessage.Text = ((KinectV2CustomButton)sender).Name + " hand enter";
        }
        private void Button1_HandPointerLeave(object sender, System.EventArgs e)
        {
            txtMessage.Text = ((KinectV2CustomButton)sender).Name + " hand leave";
        }
    }
}
