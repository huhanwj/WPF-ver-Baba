using baba_is_you.Controller;
using baba_is_you.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baba_is_you.Model
{
    public class Element
    {
        public List<List<Instance>> elements { private set; get; }
        public List<Instance> InActiveIs { private set; get; }
        public List<Instance> RowActiveIs { private set; get; }
        public List<Instance> ColumnActiveIs { private set; get; }
        public List<Instance> DoubleActiveIs { private set; get; }
        public Element()
        {
            elements = new List<List<Instance>>();
        }
        public Element(List<Instance> map)
        {
            elements = new List<List<Instance>>();
            foreach (Instance i in map)
            {
                List<Instance> refer = new List<Instance>();
                refer.Add(i);
                elements.Add(refer);
            }
        }
        public Element(controller c, List<Instance> map)
        {
            elements = new List<List<Instance>>();
            InActiveIs = new List<Instance>();
            RowActiveIs = new List<Instance>();
            ColumnActiveIs = new List<Instance>();
            DoubleActiveIs = new List<Instance>();
            //Add dummy instance for reference
            List<Instance> baba = new List<Instance>();
            MapData.Addbaba(c, baba);
            elements.Add(baba);
            List<Instance> wall = new List<Instance>();
            MapData.Addwall(c, wall);
            elements.Add(wall);
            List<Instance> rock = new List<Instance>();
            MapData.Addrock(c, rock);
            elements.Add(rock);
            List<Instance> flag = new List<Instance>();
            MapData.Addflag(c, flag);
            elements.Add(flag);
            List<Instance> empty = new List<Instance>();
            MapData.Addempty(c, empty);
            elements.Add(empty);
            List<Instance> skull = new List<Instance>();
            MapData.Addskull(c, skull);
            elements.Add(skull);
            List<Instance> grass = new List<Instance>();
            MapData.Addgrass(c, grass);
            elements.Add(grass);
            List<Instance> keke = new List<Instance>();
            MapData.Addkeke(c, keke);
            elements.Add(keke);
            //......
            //store the objects into the list of objects
            foreach (Instance i in map)
            {
                if (i.pType == P_Type.ObjectC)
                {
                    switch (i.ObjectType)
                    {
                        case InstanceType.Baba:
                            baba.Add(i);
                            break;
                        case InstanceType.Wall:
                            wall.Add(i);
                            break;
                        case InstanceType.Flag:
                            flag.Add(i);
                            break;
                        case InstanceType.Empty:
                            empty.Add(i);
                            break;
                        case InstanceType.Rock:
                            rock.Add(i);
                            break;
                        case InstanceType.Bone:
                            skull.Add(i);
                            break;
                        case InstanceType.Grass:
                            grass.Add(i);
                            break;
                        case InstanceType.Keke:
                            keke.Add(i);
                            break;
                    }
                }
                else if (i.pType == P_Type.IsBlock)
                {
                    InActiveIs.Add(i);
                }
            }
        }
        public Instance getBlock(Instance i, int x, int y)
        {
            int index = -1;
            foreach (List<Instance> list in elements)
            {
                foreach (Instance j in list)
                {
                    if (j.Equals(i))
                    {
                        index = elements.IndexOf(list) + x + y * 20;
                        break;
                    }
                }
            }
            if (index < 300 && index > -1)
            {
                return elements[index][elements[index].Count - 1];
            }
            else
            {
                return null;
            }
        }
        public int getindex(Instance i)
        {
            int index = -1;
            foreach (List<Instance> list in elements)
            {
                foreach (Instance j in list)
                {
                    if (j.Equals(i))
                    {
                        index = elements.IndexOf(list);
                        break;
                    }
                }
            }
            if (index < 300 && index > -1)
            {
                return index;
            }
            else
            {
                return -1;
            }
        }
        public void move(Instance scr, int dest)
        {
            int index = getindex(scr);
            if (index < 300 && index > -1)
            {
                if (elements[dest].Count == 1)
                {
                    if (elements[dest][0].ObjectType == InstanceType.NULL)
                    {
                        Instance tem;
                        tem = elements[dest][0];
                        elements[dest].Remove(elements[dest][0]);
                        elements[dest].Add(scr);
                        elements[index].Remove(scr);
                        elements[index].Add(tem);
                    }
                    else
                    {
                        elements[index].Remove(scr);
                        elements[dest].Add(scr);
                    }
                }
                else
                {
                    elements[index].Remove(scr);
                    elements[dest].Add(scr);
                }
            }
        }
        public void CheckEmpty(controller c)
        {
            for (int i = 0; i < 300; i++)
            {
                if (elements[i].Count == 0)
                {
                    List<Instance> empty = new List<Instance>();
                    MapData.Addempty(c, empty);
                    elements[i] = empty;
                }
            }
        }
        public void AddE(Element e)
        {
            foreach (List<Instance> list in e.elements.ToList())
            {
                List<Instance> m = new List<Instance>();
                foreach(Instance i in list.ToList())
                {
                    m.Add(i);
                }
                elements.Add(m);
            }
        }
        public void ClearIs()
        {
            foreach (Instance i in RowActiveIs)
            {
                if (i.Logic[1] != InstanceType.NULL)
                {
                    i.DeleteType(1);
                }
                if (i.StoredObjectType[0] != InstanceType.NULL)
                {
                    i.DeleteObject(0);
                }
            }
            foreach (Instance i in ColumnActiveIs)
            {
                if (i.Logic[2] != InstanceType.NULL)
                {
                    i.DeleteType(2);
                }
                if (i.StoredObjectType[1] != InstanceType.NULL)
                {
                    i.DeleteObject(1);
                }
            }
            foreach (Instance i in DoubleActiveIs)
            {
                if (i.Logic[1] != InstanceType.NULL)
                {
                    i.DeleteType(1);
                }
                if (i.Logic[2] != InstanceType.NULL)
                {
                    i.DeleteType(2);
                }
                if (i.StoredObjectType[0] != InstanceType.NULL)
                {
                    i.DeleteObject(0);
                }
                if (i.StoredObjectType[1] != InstanceType.NULL)
                {
                    i.DeleteObject(1);
                }
            }
            foreach (List<Instance> list in elements)
            {
                foreach(Instance i in list)
                {
                    for(int j = 1; j < i.Logic.Count; j++)
                    {
                        i.Remove = i.Logic[j];
                    }
                }
            }
        }
    }
}
