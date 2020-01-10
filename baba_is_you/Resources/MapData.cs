using baba_is_you.Controller;
using baba_is_you.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace baba_is_you.Resources
{
    public static class MapData
    {
        private static Dictionary<int, List<Instance>> data = new Dictionary<int, List<Instance>>();
        public static Dictionary<int, List<Instance>> mapdata
        {
            get
            {
                return data;
            }
        }
        private static string baba = " xd ";
        private static string wall = "|__|";
        private static string goop = "~~~~";
        private static string lava = "####";
        private static string flag = " |~ ";
        private static string rock = " () ";
        private static string love = " 99 ";
        private static string ice = "----";
        private static string keke = " :( ";
        private static string grass = " ^^ ";
        private static string bone = " @@ ";
        private static string Baba = "baba";
        private static string Is = " is ";
        private static string You = " you";
        private static string Wall = "wall";
        private static string Stop = "stop";
        private static string Goop = "goop";
        private static string Sink = "sink";
        private static string Lava = "lava";
        private static string Kill = "kill";
        private static string Hot = " hot";
        private static string Melt = "melt";
        private static string Keke = "keke";
        private static string Move = "move";
        private static string Love = "love";
        private static string Win = " win";
        private static string Flag = "flag";
        private static string Push = "push";
        private static string Best = "best";
        private static string Bone = "bone";
        private static string Ice = " ice";
        private static string Rock = "rock";
        private static string Grass = "herb";
        private static string Empty = "void";
        private static string empty = "    ";
        public static void InitMapData(controller c)
        {
            if (data.Count == 0)
            {
                for (int i = 1; i < 11; i++)
                {
                    List<Instance> map = new List<Instance>();
                    InitMap(c, i, map);
                    data.Add(i, map);
                }
            }
        }
        private static void InitMap(controller c, int level, List<Instance> map)
        {
            if (level == 1)
            {
                Fillempty(c, map, 0, 46);
                AddBaba(c, map);
                Fillempty(c, map, 47, 53);
                AddRock(c, map);
                Fillempty(c, map, 54, 66);
                AddIs(c, map);
                Fillempty(c, map, 67, 73);
                AddIs(c, map);
                Fillempty(c, map, 74, 86);
                AddYou(c, map);
                Fillempty(c, map, 87, 93);
                AddPush(c, map);
                Fillempty(c, map, 94, 100);
                for (int i = 100; i < 120; i++)
                {
                    Addwall(c, map);
                }
                Fillempty(c, map, 120, 130);
                Addrock(c, map);
                Fillempty(c, map, 131, 145);
                Addbaba(c, map);
                Fillempty(c, map, 146, 150);
                Addrock(c, map);
                Fillempty(c, map, 151, 155);
                Addflag(c, map);
                Fillempty(c, map, 156, 170);
                Addrock(c, map);
                Fillempty(c, map, 171, 180);
                for (int i = 180; i < 200; i++)
                {
                    Addwall(c, map);
                }
                Fillempty(c, map, 200, 206);
                AddWall(c, map);
                Fillempty(c, map, 207, 213);
                AddFlag(c, map);
                Fillempty(c, map, 214, 226);
                AddIs(c, map);
                Fillempty(c, map, 227, 233);
                AddIs(c, map);
                Fillempty(c, map, 234, 246);
                AddStop(c, map);
                Fillempty(c, map, 247, 253);
                AddWin(c, map);
                Fillempty(c, map, 254, 300);
            }
            else if (level == 2)
            {
                //FillBest(c, map, 0, 300);
                Fillempty(c, map, 0, 9);
                AddSkull(c, map);
                AddIs(c, map);
                AddDefeat(c, map);
                Fillempty(c, map, 12, 28);
                Addwall(c, map);
                Fillempty(c, map, 29, 39);
                AddWall(c, map);
                Fillempty(c, map, 40, 44);
                Addwall(c, map);
                Fillempty(c, map, 45, 54);
                AddBest(c, map);
                Fillempty(c, map, 55, 59);
                AddIs(c, map);
                Fillempty(c, map, 60, 79);
                AddStop(c, map);
                Addwall(c, map);
                Fillempty(c, map, 81, 108);
                Addbaba(c, map);
                Fillempty(c, map, 109, 131);
                for(int i = 131; i < 135; i++)
                {
                    Addskull(c, map);
                }
                AddWin(c, map);
                for (int i = 136; i < 140; i++)
                {
                    Addskull(c, map);
                }
                Fillempty(c, map, 140, 151);
                Addskull(c, map);
                for(int i = 152; i < 160; i++)
                {
                    Addgrass(c, map);
                }
                Fillempty(c, map, 160, 164);
                AddBaba(c, map);
                Fillempty(c, map, 165, 171);
                Addskull(c, map);
                for (int i = 172; i < 175; i++)
                {
                    Addgrass(c, map);
                }
                Addwall(c, map);
                for (int i = 176; i < 180; i++)
                {
                    Addgrass(c, map);
                }
                Fillempty(c, map, 180, 184);
                AddIs(c, map);
                Fillempty(c, map, 185, 191);
                Addskull(c, map);
                for (int i = 192; i < 200; i++)
                {
                    Addgrass(c, map);
                }
                Fillempty(c, map, 200, 204);
                AddYou(c, map);
                Fillempty(c, map, 205, 211);
                Addskull(c, map);
                for (int i = 212; i < 220; i++)
                {
                    if (i == 216)
                    {
                        Addkeke(c, map);
                    }
                    else
                    {
                        Addgrass(c, map);
                    }
                }
                Fillempty(c, map, 220, 231);
                Addskull(c, map);
                for (int i = 232; i < 240; i++)
                {
                    Addgrass(c, map);
                }
                Addwall(c, map);
                Fillempty(c, map, 241, 251);
                Addskull(c, map);
                for (int i = 252; i < 260; i++)
                {
                    Addgrass(c, map);
                }
                Fillempty(c, map, 260, 266);
                AddKeke(c, map);
                AddIs(c, map);
                AddMove(c, map);
                Fillempty(c, map, 269, 271);
                Addskull(c, map);
                for (int i = 272; i < 280; i++)
                {
                    Addgrass(c, map);
                }
                Fillempty(c, map, 280, 291);
                Addskull(c, map);
                for (int i = 292; i < 300; i++)
                {
                    Addgrass(c, map);
                }
            }
        }
        private static void Fillempty(controller c, List<Instance> map, int StartIn, int EndIn)
        {
            for (int i = StartIn; i < EndIn; i++)
            {
                Addempty(c, map);
            }
        }
        private static void AddBaba(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();  
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\ObjectBlock\BA.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Baba, P_Type.ObjectBlock, m, Baba);
            map.Add(obj);
        }

        public static void Addbaba(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();  
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\object\Baba.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Baba, P_Type.ObjectC, m, baba);
            map.Add(obj);
        }
        private static void AddIs(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\LogicBlock\Is.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Is, P_Type.IsBlock, m, Is);
            map.Add(obj);
        }
        private static void AddYou(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\LogicBlock\You.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.YOU, P_Type.LogicBlock, m, You);
            map.Add(obj);
        }
        private static void AddWall(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\ObjectBlock\WA.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Wall, P_Type.ObjectBlock, m, Wall);
            map.Add(obj);
        }
        public static void Addwall(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\object\Wall.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Wall, P_Type.ObjectC, m, wall);
            map.Add(obj);
        }
        private static void AddStop(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\LogicBlock\Stop.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.STOP, P_Type.LogicBlock, m, Stop);
            map.Add(obj);
        }
        private static void AddRock(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\ObjectBlock\RO.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Rock, P_Type.ObjectBlock, m, Rock);
            map.Add(obj);
        }
        public static void Addrock(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\object\Rock.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Rock, P_Type.ObjectC, m, rock);
            map.Add(obj);
        }
        private static void AddPush(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\LogicBlock\Push.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.PUSH, P_Type.LogicBlock, m, Push);
            map.Add(obj);
        }
        private static void AddFlag(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\ObjectBlock\FL.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Flag, P_Type.ObjectBlock, m, Flag);
            map.Add(obj);
        }
        public static void Addflag(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\object\flag.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Flag, P_Type.ObjectC, m, flag);
            map.Add(obj);
        }
        private static void AddWin(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\LogicBlock\Win.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.WIN, P_Type.LogicBlock, m, Win);
            map.Add(obj);
        }
        private static void AddLava(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\ObjectBlock\LA.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Lava, P_Type.ObjectBlock, m, Lava);
            map.Add(obj);
        }
        public static void Addlava(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\object\Lava.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Lava, P_Type.ObjectC, m, lava);
            map.Add(obj);
        }
        private static void AddHot(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\LogicBlock\Hot.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.HOT, P_Type.LogicBlock, m, Hot);
            map.Add(obj);
        }
        private static void AddIce(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\ObjectBlock\IC.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Ice, P_Type.ObjectBlock, m, Ice);
            map.Add(obj);
        }
        public static void Addice(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\object\Ice.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Ice, P_Type.ObjectC, m, ice);
            map.Add(obj);
        }
        private static void AddLove(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\ObjectBlock\LO.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Love, P_Type.ObjectBlock, m, Love);
            map.Add(obj);
        }
        public static void Addlove(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\object\Love.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Love, P_Type.ObjectC, m, love);
            map.Add(obj);
        }
        private static void AddBest(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\LogicBlock\Best.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.BEST, P_Type.LogicBlock, m, Best);
            map.Add(obj);
        }
        private static void AddMove(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\LogicBlock\Move.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.MOVE, P_Type.LogicBlock, m, Move);
            map.Add(obj);
        }
        private static void AddDefeat(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\LogicBlock\Defeat.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.KILL, P_Type.LogicBlock, m, Kill);
            map.Add(obj);
        }
        private static void AddMelt(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\LogicBlock\Melt.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.MELT, P_Type.LogicBlock, m, Melt);
            map.Add(obj);
        }
        private static void AddSink(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\LogicBlock\Sink.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.SINK, P_Type.LogicBlock, m, Sink);
            map.Add(obj);
        }
        private static void AddSkull(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\ObjectBlock\SK.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Bone, P_Type.ObjectBlock, m, Bone);
            map.Add(obj);
        }
        public static void Addskull(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\object\Skull.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Bone, P_Type.ObjectC, m, bone);
            map.Add(obj);
        }
        private static void AddGrass(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\ObjectBlock\GR.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Grass, P_Type.ObjectBlock, m, Grass);
            map.Add(obj);
        }
        public static void Addgrass(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\object\Grass.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Grass, P_Type.ObjectC, m, grass);
            map.Add(obj);
        }
        private static void AddKeke(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\ObjectBlock\KE.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Keke, P_Type.ObjectBlock, m, Keke);
            map.Add(obj);
        }
        public static void Addkeke(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\object\Keke.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Keke, P_Type.ObjectC, m, keke);
            map.Add(obj);
        }
        private static void AddGoop(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\ObjectBlock\GO.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Goop, P_Type.ObjectBlock, m, Goop);
            map.Add(obj);
        }
        public static void Addgoop(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\object\Goop.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Goop, P_Type.ObjectC, m, goop);
            map.Add(obj);
        }
        public static void Addempty(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\object\Void.PNG", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.NULL, P_Type.ObjectC, m, empty);
            map.Add(obj);
        }
        private static void AddEmpty(controller c, List<Instance> map)
        {
            Image m = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"..\..\Resources\Images\ObjectBlock\Empty.gif", UriKind.Relative);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            m.Source = bi;
            Instance obj = new Instance(c, InstanceType.Empty, P_Type.ObjectBlock, m, Empty);
            map.Add(obj);
        }
    }
}
