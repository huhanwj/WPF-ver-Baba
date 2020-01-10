using baba_is_you.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace baba_is_you.Model
{
    public class Instance
    {
        private controller ctrl;
        public Image image { private set; get; }
        public String text { private set; get; }
        public InstanceType ObjectType { private set; get; }
        public P_Type pType { private set; get; }
        private List<InstanceType> LogicType;
        public List<InstanceType> Logic { get { return LogicType; } }
        //the following is used by Is logic update
        public InstanceType Add { set { if (LogicType.Count <= 4) LogicType.Add(value); else throw new Exception("Unexpected Assignment"); } }
        public InstanceType Remove { set { if (LogicType.Contains(value)) LogicType.Remove(value); else throw new Exception("Unexpected Removal"); } }
        public void AddType(InstanceType logic, int index)                                   
            // Only used by IsBlock.
        {
            if (LogicType[index] == InstanceType.NULL)
            {
                LogicType[index] = logic;
            }
            else
            {
                throw new Exception("Unexpected Assignment");
            }
        }
        public void DeleteType(int index)
        {
            if (LogicType[index] != InstanceType.NULL)
            {
                LogicType[index] = InstanceType.NULL;
            }
            else
            {
                throw new Exception("Unexpected Removal");
            }
        }
        public List<InstanceType> StoredObjectType { get; private set; }                                   
        // Only used by IsBlock, will only have two elements.
        public void AddObject(InstanceType obj, int index)
        {
            if (StoredObjectType[index] == InstanceType.NULL)
            {
                StoredObjectType[index] = obj;
            }
            else
            {
                throw new Exception("Unexpected Assignment");
            }
        }
        public void DeleteObject(int index)
        {
            if (StoredObjectType[index] != InstanceType.NULL)
            {
                StoredObjectType[index] = InstanceType.NULL;
            }
            else
            {
                throw new Exception("Unexpected Removal");
            }
        }
        public void ChangeAttri(InstanceType ObjT, Image m, List<InstanceType> LogicT, String t)
        {
            ObjectType = ObjT;
            image = m;
            LogicType = LogicT;
            text = t;
        }
        public Instance(controller c, InstanceType oType, P_Type PType, Image i, String t)
        {
            ctrl = c;
            ObjectType = oType;
            pType = PType;
            image = i;
            text = t;
            LogicType = new List<InstanceType>(3);
            StoredObjectType = new List<InstanceType>(2);
            if (PType == P_Type.ObjectBlock || PType==P_Type.LogicBlock || PType==P_Type.IsBlock)
            {
                LogicType.Add(InstanceType.PUSH);
                if (PType == P_Type.IsBlock)
                {
                    LogicType.Add(InstanceType.NULL);
                    LogicType.Add(InstanceType.NULL);
                    StoredObjectType.Add(InstanceType.NULL);
                    StoredObjectType.Add(InstanceType.NULL);
                }
            }
            else
            {
                LogicType.Add(InstanceType.NULL);
            }
        }
        public bool getpushed(int x, int y, Instance i, Instance pushed)
        {
            //The return value indicate whether the pushed array can move on.
            //following the priority of stop > push > win > sink > defeat = hot&melt > move(other logic) == best(other logic) == NULL.
            if (pushed.LogicType.Contains(InstanceType.STOP))
            {
                return false;
            }
            else if (pushed.LogicType.Contains(InstanceType.PUSH))
            {
                Instance block = ctrl.CurrentData.getBlock(pushed, x, y);
                if (block != null)
                {
                    int index = ctrl.CurrentData.getindex(pushed);
                    if ((x == 1 && (ctrl.CurrentData.getindex(block) % 20 == 0)) || (x == -1 && (((ctrl.CurrentData.getindex(block) + 1) % 20) == 0)))
                    {
                        return false;
                    }
                    else
                    {
                        if (getpushed(x, y, pushed, block))
                        {
                            ctrl.MapMove(i, index);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }

            }
            else if (pushed.LogicType.Contains(InstanceType.WIN))
            {
                if (i.LogicType.Contains(InstanceType.YOU))
                {
                    ctrl.Victory();
                    return true;
                }
                else
                {
                    ctrl.MapMove(i, ctrl.CurrentData.getindex(pushed));
                    return true;
                }
            }
            else if (pushed.LogicType.Contains(InstanceType.SINK))
            {
                if (i.pType == P_Type.ObjectC)
                {
                    ctrl.kill(i);
                    ctrl.kill(pushed);
                    return true;
                }
                else
                {
                    ctrl.MapMove(i, ctrl.CurrentData.getindex(pushed));
                    return true;
                }
            }
            else if (i.LogicType.Contains(InstanceType.SINK))
            {
                if (pushed.pType == P_Type.ObjectC)
                {
                    ctrl.kill(i);
                    ctrl.kill(pushed);
                    return true;
                }
                else
                {
                    ctrl.MapMove(i, ctrl.CurrentData.getindex(pushed));
                    return true;
                }
            }
            else if (pushed.LogicType.Contains(InstanceType.KILL))
            {
                if (i.Logic.Contains(InstanceType.YOU))
                {
                    ctrl.kill(i);
                    return true;
                }
                else
                {
                    ctrl.MapMove(i, ctrl.CurrentData.getindex(pushed));
                    return true;
                }
            }
            else if (pushed.LogicType.Contains(InstanceType.HOT))
            {
                if (i.LogicType.Contains(InstanceType.MELT))
                {
                    ctrl.kill(i);
                    return true;
                }
                else
                {
                    ctrl.MapMove(i, ctrl.CurrentData.getindex(pushed));
                    return true;
                }
            }
            else if (i.LogicType.Contains(InstanceType.HOT))
            {
                if (pushed.LogicType.Contains(InstanceType.MELT))
                {
                    ctrl.kill(pushed);
                    return true;
                }
                else
                {
                    ctrl.MapMove(i, ctrl.CurrentData.getindex(pushed));
                    return true;
                }
            }
            else if(pushed.LogicType.Contains(InstanceType.MOVE)|| pushed.LogicType.Contains(InstanceType.BEST)|| pushed.LogicType.Contains(InstanceType.NULL))
            {
                ctrl.MapMove(i, ctrl.CurrentData.getindex(pushed));
                return true;
            }
            else
            {
                return false;
                throw new Exception("Something wrong with the moving!");
            }
        }

    }
}
