using baba_is_you.Model;
using baba_is_you.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace baba_is_you.Controller
{
    public class controller
    {
        private int killed = 0;
        private Instance Killed;
        public int movedir { private set; get; }
        public void inverse()
        {
            movedir = -movedir;
        }
        public List<Instance> move { private set; get; }
        public List<Instance> you { private set; get; }
        //store current data instance
        public Element CurrentData { private set; get; }
        // store previous data
        private List<Element> PreviousData;
        //store objects
        public Element objects { private set; get; }
        private int CurrentLevel;
        private List<Instance> CurrentMap;
        private Element Map;
        //From the starting window;
        private MainWindow from;
        //New the gamwindow
        private Game GameWindow;
        private TextMode TextWindow;
        public int Mode { private set; get; }
        public controller(int level,MainWindow main,int mode)
        {
            you = new List<Instance>();
            move = new List<Instance>();
            from = main;
            Mode = mode;
            CurrentLevel = level;
            MapData.InitMapData(this);
            if (!MapData.mapdata.TryGetValue(CurrentLevel, out CurrentMap))
            {
                throw new Exception("Out of Range");
            }
            //Game mode switcher
            if (Mode == 1)
            {
                GameWindow = new Game(this);
                GameWindow.Show();
            }
            else if (Mode == 2)
            {
                TextWindow = new TextMode(this);
                TextWindow.Show();
            }
            else
            {
                MessageBox.Show("Fatal Error!");
                return;
            }
            GameInit();
        }
        //Initialize for every level
        private void GameInit()
        {
            //store current elements in CurrentData
            movedir = -1;
            PreviousData = new List<Element>();
            CurrentData = new Element(CurrentMap);
            objects = new Element(this, CurrentMap);
            Map = new Element(CurrentMap);
            InitMap();
            CheckIs();
        }
        //Initialize the game map
        private void InitMap()
        {
            if (Mode == 1)
            {
                GameWindow.Presenter.Children.Clear();
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        Image m = CurrentData.getBlock(CurrentData.elements[0][0], j, i).image;
                        GameWindow.Presenter.Children.Add(m);
                        Canvas.SetLeft(m, j * 24 + 45);
                        Canvas.SetTop(m, i * 24 + 45);
                        if (CurrentData.getBlock(CurrentData.elements[0][0], j, i).ObjectType == InstanceType.NULL)
                            Canvas.SetZIndex(m, 2);
                        else if (CurrentData.getBlock(CurrentData.elements[0][0], j, i).ObjectType == InstanceType.Baba)
                            Canvas.SetZIndex(m, 4);
                        else
                            Canvas.SetZIndex(m, 3);
                    }
                }
                //Add BEST.gif in the bottom layer
                for(int i = 0; i < 15; i++)
                {
                    for(int j = 0; j < 20; j++)
                    {
                        Image best = new Image();
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.UriSource = new Uri(@"..\..\Resources\Images\object\BEST.gif", UriKind.Relative);
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.EndInit();
                        best.Source = bi;
                        GameWindow.Presenter.Children.Add(best);
                        Canvas.SetLeft(best, j * 24 + 45);
                        Canvas.SetTop(best, i * 24 + 45);
                        Canvas.SetZIndex(best, 0);
                    }
                }
                for(int index = 300; index < 600; index++)
                {
                    GameWindow.Presenter.Children[index].Visibility = Visibility.Hidden;
                }
            }
            else if (Mode == 2)
            {
                TextWindow.PresenBox.Text = null;
                //to do: how to display the map via text
                for (int i = 0; i < 15; i++) 
                {
                    for (int j = 0; j < 20; j++)
                    {
                        TextWindow.PresenBox.Text += CurrentData.getBlock(CurrentData.elements[0][0], j, i).text;
                    }
                    TextWindow.PresenBox.Text += "\n";
                }
            }
        }
        //Change the certain presented block.
        private void ChangeView(Instance t, int index, String text)
        {
            if (Mode == 1)
            {
                GameWindow.Presenter.Children[index] = t.image;
            }
            else if (Mode == 2)
            {
                //to do: how to display the map via text
                TextWindow.PresenBox.Text.Replace(text, t.text);
            }
        }
        private void CheckIs()
        {
            foreach(Instance i in objects.InActiveIs.ToList())
            {
                Instance block1 = CurrentData.getBlock(i, -1, 0);
                Instance block2 = CurrentData.getBlock(i, 1, 0);
                Instance block3 = CurrentData.getBlock(i, 0, -1);
                Instance block4 = CurrentData.getBlock(i, 0, 1);
                if (block1 != null && block2 != null && block1.pType == P_Type.ObjectBlock && block2.pType == P_Type.LogicBlock)
                {
                    objects.RowActiveIs.Add(i);
                    objects.InActiveIs.Remove(i);
                    i.AddType(block2.ObjectType, 1);                                     //Row logic formation will store the logic and object type in LogicType[1] of Is for future reference.
                    i.AddObject(block1.ObjectType, 0);
                    foreach (List<Instance> list in objects.elements)
                    {
                        if (list.Count != 0 && list[0].ObjectType == block1.ObjectType)
                        {
                            foreach(Instance j in list)
                            {
                                j.Add = block2.ObjectType;
                            }
                            if (block2.ObjectType == InstanceType.YOU)
                            {
                                for (int k = 1; k < list.Count; k++)
                                {
                                    you.Add(list[k]);
                                }
                            }
                            else if (block2.ObjectType == InstanceType.MOVE)
                            {
                                for (int k = 1; k < list.Count; k++)
                                {
                                    move.Add(list[k]);
                                }
                            }
                            else if (block2.ObjectType == InstanceType.BEST)
                            {
                                for (int k = 1; k < list.Count; k++)
                                {
                                    int index = CurrentData.getindex(list[k]);
                                    Canvas.SetZIndex(GameWindow.Presenter.Children[index+300], 3);
                                    GameWindow.Presenter.Children[index + 300].Visibility = Visibility.Visible;
                                }
                            }
                            break;
                        }
                    }
                }
                else if (block1 != null && block2 != null && block1.pType == P_Type.ObjectBlock && block2.pType == P_Type.ObjectBlock)
                {
                    objects.RowActiveIs.Add(i);
                    objects.InActiveIs.Remove(i);
                    List<Instance> tem = new List<Instance>();
                    String old = null;
                    //Change Object will not need stored logic and object type.
                    foreach (List<Instance> list1 in objects.elements)
                    {
                        if (list1.Count != 0 && list1[0].ObjectType == block1.ObjectType)
                        {
                            tem = list1;
                            old = list1[0].text;
                            break;
                        }
                    }
                    foreach (List<Instance> list0 in objects.elements)
                    {
                        if (list0.Count != 0 && list0[0].ObjectType == block2.ObjectType)
                        {
                            foreach(Instance j in tem)
                            {
                                if (tem.IndexOf(j) != 0)
                                {
                                    j.ChangeAttri(list0[0].ObjectType, list0[0].image, list0[0].Logic, list0[0].text);
                                    list0.Add(j);
                                    tem.Remove(j);
                                    int index = CurrentData.getindex(j);
                                    ChangeView(j, index, old);
                                }
                            }
                            break;
                        }
                    }
                }
                if (block3 != null && block4 != null && block3.pType == P_Type.ObjectBlock && block4.pType == P_Type.LogicBlock)
                {
                    objects.ColumnActiveIs.Add(i);
                    objects.InActiveIs.Remove(i);
                    i.AddType(block4.ObjectType, 2);                                     //Column logic formation will store the logic and object type in LogicType[2] of Is for future reference.
                    i.AddObject(block3.ObjectType, 1);
                    foreach (List<Instance> list in objects.elements)
                    {
                        if (list.Count != 0 && list[0].ObjectType == block3.ObjectType)
                        {
                            foreach (Instance j in list)
                            {
                                j.Add = block4.ObjectType;
                            }
                            if (block4.ObjectType == InstanceType.YOU)
                            {
                                for (int k = 1; k < list.Count; k++)
                                {
                                    you.Add(list[k]);
                                }
                            }
                            else if (block4.ObjectType == InstanceType.MOVE)
                            {
                                for (int k = 1; k < list.Count; k++)
                                {
                                    move.Add(list[k]);
                                }
                            }
                            else if (block4.ObjectType == InstanceType.BEST)
                            {
                                for (int k = 1; k < list.Count; k++)
                                {
                                    int index = CurrentData.getindex(list[k]);
                                    Canvas.SetZIndex(GameWindow.Presenter.Children[index + 300], 3);
                                    GameWindow.Presenter.Children[index + 300].Visibility = Visibility.Visible;
                                }
                            }
                            break;
                        }
                    }
                }
                else if (block3 != null && block4 != null && block3.pType == P_Type.ObjectBlock && block4.pType == P_Type.ObjectBlock)
                {
                    objects.ColumnActiveIs.Add(i);
                    objects.InActiveIs.Remove(i);
                    List<Instance> tem = new List<Instance>();
                    String old = null;
                    //Change Object will not need stored logic and object type.
                    foreach (List<Instance> list1 in objects.elements)
                    {
                        if (list1.Count != 0 && list1[0].ObjectType == block3.ObjectType)
                        {
                            tem = list1;
                            old = list1[0].text;
                            break;
                        }
                    }
                    foreach (List<Instance> list0 in objects.elements)
                    {
                        if (list0.Count != 0 && list0[0].ObjectType == block4.ObjectType)
                        {
                            foreach (Instance j in tem)
                            {
                                if (tem.IndexOf(j) != 0)
                                {
                                    j.ChangeAttri(list0[0].ObjectType, list0[0].image, list0[0].Logic, list0[0].text);
                                    list0.Add(j);
                                    tem.Remove(j);
                                    int index = CurrentData.getindex(j);
                                    ChangeView(j, index, old);
                                }
                            }
                            break;
                        }
                    }
                }
            }
            foreach (Instance i in objects.RowActiveIs.ToList())
            {
                Instance block3 = CurrentData.getBlock(i, 0, -1);
                Instance block4 = CurrentData.getBlock(i, 0, 1);
                if (block3 != null && block4 != null && block3.pType == P_Type.ObjectBlock && block4.pType == P_Type.LogicBlock)
                {
                    objects.DoubleActiveIs.Add(i);
                    objects.RowActiveIs.Remove(i);
                    i.AddType(block4.ObjectType, 2);                                     //Column logic formation will store the logic and object type in LogicType[2] of Is for future reference.
                    i.AddObject(block3.ObjectType, 1);
                    foreach (List<Instance> list in objects.elements)
                    {
                        if (list.Count != 0 && list[0].ObjectType == block3.ObjectType)
                        {
                            foreach (Instance j in list)
                            {
                                j.Add = block4.ObjectType;
                            }
                            if (block4.ObjectType == InstanceType.YOU)
                            {
                                for (int k = 1; k < list.Count; k++)
                                {
                                    you.Add(list[k]);
                                }
                            }
                            else if (block4.ObjectType == InstanceType.MOVE)
                            {
                                for (int k = 1; k < list.Count; k++)
                                {
                                    move.Add(list[k]);
                                }
                            }
                            else if (block4.ObjectType == InstanceType.BEST)
                            {
                                for (int k = 1; k < list.Count; k++)
                                {
                                    int index = CurrentData.getindex(list[k]);
                                    Canvas.SetZIndex(GameWindow.Presenter.Children[index + 300], 3);
                                    GameWindow.Presenter.Children[index + 300].Visibility = Visibility.Visible;
                                }
                            }
                            break;
                        }
                    }
                }
                else if (block3 != null && block4 != null && block3.pType == P_Type.ObjectBlock && block4.pType == P_Type.ObjectBlock)
                {
                    objects.DoubleActiveIs.Add(i);
                    objects.RowActiveIs.Remove(i);
                    List<Instance> tem = new List<Instance>();
                    String old = null;
                    //Change Object will not need stored logic and object type.
                    foreach (List<Instance> list1 in objects.elements)
                    {
                        if (list1.Count != 0 && list1[0].ObjectType == block3.ObjectType)
                        {
                            tem = list1;
                            old = list1[0].text;
                            break;
                        }
                    }
                    foreach (List<Instance> list0 in objects.elements)
                    {
                        if (list0.Count != 0 && list0[0].ObjectType == block4.ObjectType)
                        {
                            foreach (Instance j in tem)
                            {
                                if (tem.IndexOf(j) != 0)
                                {
                                    j.ChangeAttri(list0[0].ObjectType, list0[0].image, list0[0].Logic, list0[0].text);
                                    list0.Add(j);
                                    tem.Remove(j);
                                    int index = CurrentData.getindex(j);
                                    ChangeView(j, index, old);
                                }
                            }
                            break;
                        }
                    }
                }
            }
            foreach (Instance i in objects.ColumnActiveIs.ToList())
            {
                Instance block1 = CurrentData.getBlock(i, -1, 0);
                Instance block2 = CurrentData.getBlock(i, 1, 0);
                if (block1 != null && block2 != null && block1.pType == P_Type.ObjectBlock && block2.pType == P_Type.LogicBlock)
                {
                    objects.DoubleActiveIs.Add(i);
                    objects.ColumnActiveIs.Remove(i);
                    i.AddType(block2.ObjectType, 1);                                     //Row logic formation will store the logic and object type in LogicType[1] of Is for future reference.
                    i.AddObject(block1.ObjectType, 0);
                    foreach (List<Instance> list in objects.elements)
                    {
                        if (list.Count != 0 && list[0].ObjectType == block1.ObjectType)
                        {
                            foreach (Instance j in list)
                            {
                                j.Add = block2.ObjectType;
                            }
                            if (block2.ObjectType == InstanceType.YOU)
                            {
                                for (int k = 1; k < list.Count; k++)
                                {
                                    you.Add(list[k]);
                                }
                            }
                            else if (block2.ObjectType == InstanceType.MOVE)
                            {
                                for (int k = 1; k < list.Count; k++)
                                {
                                    move.Add(list[k]);
                                }
                            }
                            else if (block2.ObjectType == InstanceType.BEST)
                            {
                                for (int k = 1; k < list.Count; k++)
                                {
                                    int index = CurrentData.getindex(list[k]);
                                    Canvas.SetZIndex(GameWindow.Presenter.Children[index + 300], 3);
                                    GameWindow.Presenter.Children[index + 300].Visibility = Visibility.Visible;
                                }
                            }
                            break;
                        }
                    }
                }
                else if (block1 != null && block2 != null && block1.pType == P_Type.ObjectBlock && block2.pType == P_Type.ObjectBlock)
                {
                    objects.DoubleActiveIs.Add(i);
                    objects.ColumnActiveIs.Remove(i);
                    List<Instance> tem = new List<Instance>();
                    String old = null;
                    //Change Object will not need stored logic and object type.
                    foreach (List<Instance> list1 in objects.elements)
                    {
                        if (list1.Count != 0 && list1[0].ObjectType == block1.ObjectType)
                        {
                            tem = list1;
                            old = list1[0].text;
                            break;
                        }
                    }
                    foreach (List<Instance> list0 in objects.elements)
                    {
                        if (list0.Count != 0 && list0[0].ObjectType == block2.ObjectType)
                        {
                            foreach (Instance j in tem)
                            {
                                if (tem.IndexOf(j) != 0)
                                {
                                    j.ChangeAttri(list0[0].ObjectType, list0[0].image, list0[0].Logic, list0[0].text);
                                    list0.Add(j);
                                    tem.Remove(j);
                                    int index = CurrentData.getindex(j);
                                    ChangeView(j, index, old);
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
        private void CheckActiveIs()
        {
            foreach (Instance i in objects.DoubleActiveIs.ToList())
            {
                Instance block1 = CurrentData.getBlock(i, -1, 0);
                Instance block2 = CurrentData.getBlock(i, 1, 0);
                Instance block3 = CurrentData.getBlock(i, 0, -1);
                Instance block4 = CurrentData.getBlock(i, 0, 1);
                //Check Row
                if (i.Logic[1] != InstanceType.NULL)
                {
                    if (block1 == null || block2 == null || block1.pType != P_Type.ObjectBlock || block2.pType != P_Type.LogicBlock || block1.ObjectType != i.StoredObjectType[0] || block2.ObjectType != i.Logic[1])
                    {
                        objects.ColumnActiveIs.Add(i);
                        objects.DoubleActiveIs.Remove(i);
                        foreach (List<Instance> list in objects.elements)
                        {
                            if (list.Count != 0 && list[0].ObjectType == i.StoredObjectType[0])
                            {
                                foreach (Instance j in list)
                                {
                                    j.Remove = i.Logic[1];
                                }
                                if (i.Logic[1] == InstanceType.YOU)
                                {
                                    you.Clear();
                                }
                                else if (i.Logic[1] == InstanceType.MOVE)
                                {
                                    move.Clear();
                                }
                                else if (i.Logic[1] == InstanceType.BEST)
                                {
                                    for (int k = 1; k < list.Count; k++)
                                    {
                                        int index = CurrentData.getindex(list[k]);
                                        Canvas.SetZIndex(GameWindow.Presenter.Children[index + 300], 0);
                                        GameWindow.Presenter.Children[index + 300].Visibility = Visibility.Hidden;
                                    }
                                }
                                break;
                            }
                        }
                        //Erase the stored value.
                        i.DeleteType(1);
                        i.DeleteObject(0);
                    }
                }
                //Check Column
                if (i.Logic[2] != InstanceType.NULL)
                {
                    if (block3 == null || block4 == null || block3.pType != P_Type.ObjectBlock || block4.pType != P_Type.LogicBlock || block3.ObjectType != i.StoredObjectType[1] || block4.ObjectType != i.Logic[2])
                    {
                        objects.RowActiveIs.Add(i);
                        objects.DoubleActiveIs.Remove(i);
                        foreach (List<Instance> list in objects.elements)
                        {
                            if (list.Count != 0 && list[0].ObjectType == i.StoredObjectType[1])
                            {
                                foreach (Instance j in list)
                                {
                                    j.Remove = i.Logic[2];
                                }
                                if (i.Logic[2] == InstanceType.YOU)
                                {
                                    you.Clear();
                                }
                                else if (i.Logic[2] == InstanceType.MOVE)
                                {
                                    move.Clear();
                                }
                                else if (i.Logic[2] == InstanceType.BEST)
                                {
                                    for (int k = 1; k < list.Count; k++)
                                    {
                                        int index = CurrentData.getindex(list[k]);
                                        Canvas.SetZIndex(GameWindow.Presenter.Children[index + 300], 0);
                                        GameWindow.Presenter.Children[index + 300].Visibility = Visibility.Hidden;
                                    }
                                }
                                break;
                            }
                        }
                        i.DeleteType(2);
                        i.DeleteObject(1);
                    }
                }
            }
            foreach (Instance i in objects.RowActiveIs.ToList())
            {
                Instance block1 = CurrentData.getBlock(i, -1, 0);
                Instance block2 = CurrentData.getBlock(i, 1, 0);
                if (i.Logic[1] != InstanceType.NULL)
                {
                    if (block1 == null || block2 == null || block1.pType != P_Type.ObjectBlock || block2.pType != P_Type.LogicBlock || block1.ObjectType != i.StoredObjectType[0] || block2.ObjectType != i.Logic[1])
                    {
                        objects.InActiveIs.Add(i);
                        objects.RowActiveIs.Remove(i);
                        foreach (List<Instance> list in objects.elements)
                        {
                            if (list.Count != 0 && list[0].ObjectType == i.StoredObjectType[0])
                            {
                                foreach (Instance j in list)
                                {
                                    j.Remove = i.Logic[1];
                                }
                                if (i.Logic[1] == InstanceType.YOU)
                                {
                                    you.Clear();
                                }
                                else if (i.Logic[1] == InstanceType.MOVE)
                                {
                                    move.Clear();
                                }
                                else if (i.Logic[1] == InstanceType.BEST)
                                {
                                    for (int k = 1; k < list.Count; k++)
                                    {
                                        int index = CurrentData.getindex(list[k]);
                                        Canvas.SetZIndex(GameWindow.Presenter.Children[index + 300], 0);
                                        GameWindow.Presenter.Children[index + 300].Visibility = Visibility.Hidden;
                                    }
                                }
                                break;
                            }
                        }
                        i.DeleteType(1);
                        i.DeleteObject(0);
                    }
                }
            }
            foreach (Instance i in objects.ColumnActiveIs.ToList())
            {
                Instance block3 = CurrentData.getBlock(i, 0, -1);
                Instance block4 = CurrentData.getBlock(i, 0, 1);
                if (i.Logic[2] != InstanceType.NULL)
                {
                    if (block3 == null || block4 == null || block3.pType != P_Type.ObjectBlock || block4.pType != P_Type.LogicBlock || block3.ObjectType != i.StoredObjectType[1] || block4.ObjectType != i.Logic[2])
                    {
                        objects.InActiveIs.Add(i);
                        objects.ColumnActiveIs.Remove(i);
                        foreach (List<Instance> list in objects.elements)
                        {
                            if (list.Count != 0 && list[0].ObjectType == i.StoredObjectType[1])
                            {
                                foreach (Instance j in list)
                                {
                                    j.Remove = i.Logic[2];
                                }
                                if (i.Logic[2] == InstanceType.YOU)
                                {
                                    you.Clear();
                                }
                                else if (i.Logic[2] == InstanceType.MOVE)
                                {
                                    move.Clear();
                                }
                                else if (i.Logic[2] == InstanceType.BEST)
                                {
                                    for (int k = 1; k < list.Count; k++)
                                    {
                                        int index = CurrentData.getindex(list[k]);
                                        Canvas.SetZIndex(GameWindow.Presenter.Children[index + 300], 0);
                                        GameWindow.Presenter.Children[index + 300].Visibility = Visibility.Hidden;
                                    }
                                }
                                break;
                            }
                        }
                        i.DeleteType(2);
                        i.DeleteObject(1);
                    }
                }
            }
        }
        //judging the win condtions
        public void Victory()
        {
            CurrentLevel++;
            int winCheck = CurrentLevel - 1;
            if (winCheck == 2)
            {
                MessageBoxResult RestartChoice = MessageBox.Show("Congratulation! You win! Restart?", "Restart?", MessageBoxButton.YesNo);
                // if yes, restart
                if (RestartChoice == MessageBoxResult.Yes)
                {
                    CurrentLevel = 1;
                    Reload();
                }
                // if no, close the window
                else
                {
                    Exit();
                }
            }
            else if (MapData.mapdata.TryGetValue(CurrentLevel, out CurrentMap))
            {
                Reload();
            }
            else
            {
                throw new Exception("Out of Range");
            }
        }
        //data undo and set the display correspondingly
        public void undo()
        {
            if (Mode == 1)
            {
                if (GameWindow.x_list.Count != 0)
                {
                    for (int index = 0; index < 300; index++)
                    {
                        int Zindex = Canvas.GetZIndex(GameWindow.Presenter.Children[index]);
                        if (GameWindow.Presenter.Children[index].Visibility == Visibility.Hidden && Zindex > 1)
                            GameWindow.Presenter.Children[index].Visibility = Visibility.Visible;
                        Canvas.SetLeft(GameWindow.Presenter.Children[index], GameWindow.x_list.Last()[index]);
                        Canvas.SetTop(GameWindow.Presenter.Children[index], GameWindow.y_list.Last()[index]);
                    }
                    // after redo, remove the stored data from the list
                    GameWindow.x_list.Last().Clear();
                    GameWindow.y_list.Last().Clear();
                    GameWindow.x_list.RemoveAt(GameWindow.x_list.Count - 1);
                    GameWindow.y_list.RemoveAt(GameWindow.y_list.Count - 1);
                    CurrentData = new Element();
                    CurrentData.AddE(PreviousData[PreviousData.Count - 1]);
                    PreviousData.RemoveAt(PreviousData.Count - 1);
                    if (killed == 1)
                    {
                        killed = 0;
                        you.Add(Killed);
                    }
                    //PreviousData
                    CheckActiveIs();
                    CheckIs();
                }
            }
            else if (Mode == 2)
            {
                if (PreviousData.Count > 0)
                {
                    CurrentData = new Element();
                    CurrentData.AddE(PreviousData[PreviousData.Count - 1]);
                    PreviousData.RemoveAt(PreviousData.Count - 1);
                    if (killed == 1)
                    {
                        killed = 0;
                        you.Add(Killed);
                    }
                    //PreviousData
                    CheckActiveIs();
                    CheckIs();
                }
            }
        }
        //after trigger, remove the corresponding instance
        public void kill(Instance poor)
        {
            killed = 1;
            int index = CurrentData.getindex(poor);
            if (Mode == 1)
            {
                int dex = Map.getindex(poor);
                GameWindow.Presenter.Children[dex].Visibility=Visibility.Hidden;
            }
            else if (Mode == 2)
            {
                TextWindow.PresenBox.Text = TextWindow.PresenBox.Text.Remove(4 * index + (index / 20), 4);
                TextWindow.PresenBox.Text = TextWindow.PresenBox.Text.Insert(4 * index + (index / 20), "    ");
            }
            CurrentData.elements[index].Remove(poor);
            foreach (List<Instance> list in objects.elements)
            {
                if (list[0].ObjectType == poor.ObjectType)
                {
                    list.Remove(poor);
                }
                break;
            }
            if (poor.Logic.Contains(InstanceType.YOU))
            {
                Killed = poor;
                you.Remove(poor);
            }
        }
        //move accordingly
        public void MapMove(Instance source, int dest)
        {
            if (Mode == 1)
            { 
                UIElement m = source.image;
                int index = GameWindow.Presenter.Children.IndexOf(m);
                int x = dest % 20;
                int y = dest / 20;
                Canvas.SetLeft(GameWindow.Presenter.Children[index], x * 24 + 45);
                Canvas.SetTop(GameWindow.Presenter.Children[index], y * 24 + 45);
            }
            else if (Mode == 2)
            {
                int index = CurrentData.getindex(source);
                if (index < 300 && index > -1)
                {
                    int count = CurrentData.elements[index].Count;
                    String s1;
                    String s2 = source.text;
                    if (count > 1)
                    {
                        if (CurrentData.elements[index][count - 1].Equals(source))
                        {
                            s1 = CurrentData.elements[index][count - 2].text;
                        }
                        else
                        {
                            s1 = CurrentData.elements[index][count - 1].text;
                        }
                    }
                    else
                    {
                        s1 = "    ";
                    }
                    TextWindow.PresenBox.Text = TextWindow.PresenBox.Text.Remove(4 * index + (index / 20), 4);
                    TextWindow.PresenBox.Text = TextWindow.PresenBox.Text.Insert(4 * index + (index / 20), s1);
                    TextWindow.PresenBox.Text = TextWindow.PresenBox.Text.Remove(4 * dest + (dest / 20), 4);
                    TextWindow.PresenBox.Text = TextWindow.PresenBox.Text.Insert(4 * dest + (dest / 20), s2);
                }
            }
            CurrentData.move(source, dest);
            CurrentData.CheckEmpty(this);
            CheckActiveIs();
            CheckIs();
        }
        //store the previous data
        public void store()
        {
            Element NewData = new Element();
            NewData.AddE(CurrentData);
            PreviousData.Add(NewData);
        }
        // reload the map
        public void Reload()
        {
            if (MapData.mapdata.TryGetValue(CurrentLevel, out CurrentMap))
            {
                //clear all the current data
                if (PreviousData != null)
                {
                    PreviousData.Clear();
                }
                if (Mode == 1)
                {
                    if (GameWindow.x_list != null)
                        GameWindow.x_list.Clear();
                    if (GameWindow.y_list != null)
                        GameWindow.y_list.Clear();
                    for (int index = 0; index < 300; index++)
                    {
                        int Zindex = Canvas.GetZIndex(GameWindow.Presenter.Children[index]);
                        if (GameWindow.Presenter.Children[index].Visibility == Visibility.Hidden && Zindex > 1)
                            GameWindow.Presenter.Children[index].Visibility = Visibility.Visible;
                    }
                }
                if (you != null)
                {
                    you.Clear();
                    
                }
                else
                {
                    you = new List<Instance>();
                }
                if (move != null)
                    move.Clear();
                else
                    move = new List<Instance>();
                objects.ClearIs();
                
                //PreviousData.Clear();
                //call the init func
                GameInit();
            }
            else
            {
                throw new Exception("Read Error!");
            }
        }
        //exit 
        public void Exit()
        {
            if (Mode == 1)
            {
                GameWindow.Close();
                from.Close();
            }
            else if (Mode == 2)
            {
                TextWindow.Close();
                from.Close();
            }
        }
    }
}
