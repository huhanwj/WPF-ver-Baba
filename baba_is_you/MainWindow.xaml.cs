using System;
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

namespace baba_is_you
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool Normal_Check;
        private bool Text_Check;
        private int NORMAL = 1;
        private int TEXT = 2;
        MediaPlayer p = new MediaPlayer();
        public MainWindow()
        {
            InitializeComponent();
            p.Open(new Uri(@"../../Resources/BabaIsYou.mp3", UriKind.RelativeOrAbsolute));
            p.MediaEnded += new EventHandler(Media_Ended);
            p.Play();
        }
        private void Media_Ended(object sender, EventArgs e)
        {
            p.Position = TimeSpan.Zero;
            p.Play();
        }
        private void Normal_Checked(object sender, RoutedEventArgs e)
        {
            Normal_Check = true;
            Text_Check = false;
        }

        private void Text_Checked(object sender, RoutedEventArgs e)
        {
            Text_Check = true;
            Normal_Check = false;
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            // if the player checked the normal radio button
            if (Normal_Check)
            {
                Controller.controller c = new Controller.controller(1, this,NORMAL);
                //after clicking start, hide this window
                this.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (Text_Check)
            {
                Controller.controller c = new Controller.controller(1, this, TEXT);
                //after clicking start, hide this window
                this.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Please select the game mode!");
            }
        }
        //useless func
        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
