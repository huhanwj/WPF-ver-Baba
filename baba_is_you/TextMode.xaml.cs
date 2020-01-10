using baba_is_you.Model;
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

namespace baba_is_you
{
    /// <summary>
    /// Interaction logic for Text.xaml
    /// </summary>
    public partial class TextMode : Window
    {
        //var to store controller
        private Controller.controller c;
        // previous textbox list
        private List<string> previous=new List<string>();
        // moving direction indicator
        private int x = 0;
        private int y = 0;
        public TextMode(Controller.controller control)
        {
            InitializeComponent();
            c = control;
        }
        private void store()
        {
            string prev = PresenBox.Text;
            previous.Add(prev);
            c.store();
        }
        // reset the current state
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult RestartChoice = MessageBox.Show("Do you want to reset the game?", "Reset?", MessageBoxButton.YesNo);
            if (RestartChoice == MessageBoxResult.Yes)
            {
                previous.Clear();
                c.Reload();
            }
        }
        //move left
        private void Left_Click(object sender, RoutedEventArgs e)
        {
            x = -1;
            store();
            //below code need improvement
            if (c.move.Count != 0)
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
            if (c.you.Count != 0)
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
            //reset the indicator
            x = 0;
        }
        private void Right_Click(object sender, RoutedEventArgs e)
        {
            x = 1;
            store();
            //below code need improvement
            if (c.move.Count != 0)
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
            if (c.you.Count != 0)
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
            //reset the indicator
            x = 0;
        }
        private void Up_Click(object sender, RoutedEventArgs e)
        {
            y = -1;
            store();
            //below code need improvement
            if (c.move.Count != 0)
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
            if (c.you.Count != 0)
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
                                if (c.CurrentData.elements[index][j].Logic.Count > 1 || c.CurrentData.elements[index][j].ObjectType != InstanceType.NULL)
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
            //reset the indicator
            y = 0;
        }
        private void Down_Click(object sender, RoutedEventArgs e)
        {
            y = 1;
            store();
            //below code need improvement
            if (c.move.Count != 0)
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
            if (c.you.Count != 0)
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
                                if (c.CurrentData.elements[index][j].Logic.Count > 1 || c.CurrentData.elements[index][j].ObjectType != InstanceType.NULL)
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
            //reset the indicator
            y = 0;
        }
        // undo function
        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (previous.Count != 0)
            {
                PresenBox.Clear();
                PresenBox.Text = previous.Last();
                // after redo, remove the stored data from the list
                previous.RemoveAt(previous.Count - 1);
                c.undo();
            }
        }
        // close the game window will return to the starting window
        private void OnClosed(object sender, EventArgs e)
        {
            c.Exit();
        }
    }
}
