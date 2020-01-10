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
using System.Windows.Shapes;
using baba_is_you.Model;

namespace baba_is_you
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        //instances for baba is you
        
        //var to store controller
        private Controller.controller c;
        // previous canvas list
        public List<double> coor_x { private set; get; }
        public List<double> coor_y { private set; get; }
        public List<List<double>> x_list { private set; get; }
        public List<List<double>> y_list { private set; get; }
        public Game(Controller.controller control)
        {
            InitializeComponent();
            c=control;
            x_list = new List<List<double>>();
            y_list = new List<List<double>>();
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            //store the canvas into list before move
            int x = 0, y = 0;
            switch (e.Key)
            {
                case Key.W:
                case Key.Up:
                    y = -1;
                    store();
                    c.store();
                    break;
                case Key.S:
                case Key.Down:
                    y = 1;
                    store();
                    c.store();
                    break;
                case Key.A:
                case Key.Left:
                    x = -1;
                    store();
                    c.store();
                    break;
                case Key.D:
                case Key.Right:
                    x = 1;
                    store();
                    c.store();
                    break;
                //Reset at current level
                case Key.R:
                    MessageBoxResult RestartChoice = MessageBox.Show("Do you want to reset the game?", "Reset?", MessageBoxButton.YesNo);
                    if (RestartChoice == MessageBoxResult.Yes)
                    {
                        coor_x.Clear();
                        coor_y.Clear();
                        c.Reload();
                    }
                    break;
                // Press Z to redo
                case Key.Z:
                    goback();
                    break;
                // press ESC to exit
                case Key.Escape:
                    this.Close();
                    break;
            }
            // call the move function via various conditions
            if ((x != 0 || y != 0) && c.move.Count != 0)
            {
                foreach (Instance i in c.move)
                {
                    Instance block = c.CurrentData.getBlock(i, c.movedir, 0);
                    if (block != null)
                    {
                        if ((c.movedir == 1 && ((c.CurrentData.getindex(block)) % 20 == 0)) || (c.movedir == -1 && (((c.CurrentData.getindex(block) + 1) % 20) == 0)))
                        {
                            c.inverse();
                        }
                        else
                        {
                            if (block.getpushed(c.movedir, 0, i, block))
                            {

                            }
                            else
                            {
                                c.inverse();
                            }
                        }
                    }
                    else
                    {
                        c.inverse();
                    }
                }
            }
            if ((x != 0 || y != 0) && c.you.Count != 0)
            {
                foreach (Instance i in c.you.ToList())
                {
                    
                    Instance block = c.CurrentData.getBlock(i, x, y);
                    int index = c.CurrentData.getindex(block);
                    if (block != null)
                    {
                        for (int j = 0; j < c.CurrentData.elements[index].Count; j++)
                        {
                            if (c.CurrentData.elements[index].Count == 1)
                            {
                                if ((x == 1 && ((c.CurrentData.getindex(block)) % 20 == 0)) || (x == -1 && (((c.CurrentData.getindex(block) + 1) % 20) == 0)))
                                {

                                }
                                else
                                {
                                    c.CurrentData.elements[index][j].getpushed(x, y, i, c.CurrentData.elements[index][j]);
                                }
                            }
                            else if (c.CurrentData.elements[index].Count > 1)
                            {
                                if (c.CurrentData.elements[index][j].Logic.Count > 1 || c.CurrentData.elements[index][j].ObjectType!=InstanceType.NULL)
                                {
                                    if ((x == 1 && (c.CurrentData.getindex(block) % 20 == 0)) || (x == -1 && (((c.CurrentData.getindex(block) + 1) % 20) == 0)))
                                    {

                                    }
                                    else
                                    {
                                        c.CurrentData.elements[index][j].getpushed(x, y, i, c.CurrentData.elements[index][j]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        //store the current stage before moving
        private void store()
        {
            coor_x = new List<double>();
            coor_y = new List<double>();
            foreach (UIElement i in Presenter.Children)
            {
                double x = Canvas.GetLeft(i);
                double y = Canvas.GetTop(i);
                coor_x.Add(x);
                coor_y.Add(y);
            }
            x_list.Add(coor_x);
            y_list.Add(coor_y);
        }
        // read the previous canvas for ctrl z function
        private void goback()
        {
                c.undo();
        }
        // close the game window will return to the starting window
        private void Window_Closed(object sender, EventArgs e)
        {
            c.Exit();
        }
    }
}
