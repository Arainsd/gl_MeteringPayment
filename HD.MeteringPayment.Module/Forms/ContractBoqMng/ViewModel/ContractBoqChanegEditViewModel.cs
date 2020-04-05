using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Domain.Entity.ContractBoqEntity;
using Hondee.Common.Advance.Util;
using Hondee.Common.HDException;
using Hondee.Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Module.Forms.ContractBoqMng.ViewModel
{
    public partial class ContractBoqChanegEditViewModel
    {
        #region 成员
        private IContractBoq contractBoqService;
        /// <summary>
        /// 清单状态
        /// </summary>
        public BoqStat BoqStat { get; private set; }
        public String ProjectNo { get; private set; }
        public String ProjectName { get; private set; }
        /// <summary>
        /// 根节点变更事件
        /// </summary>
        private event EventHandler RootChangedEvent;
        private Boolean editing;
        /// <summary>
        /// 获取或设置清单的编辑状态，true为编辑中，否则为未编辑
        /// </summary>
        public Boolean Editing
        {
            get
            {
                return editing;
            }
            set
            {
                editing = value;
            }
        }
        /// <summary>
        /// 获取清单的可编辑性
        /// </summary>
        public Boolean BoqEdit
        {
            get
            {
                return BoqStat == BoqStat.Absent || BoqStat == BoqStat.UnLock;
            }
        }
        /// <summary>
        /// 获取清单的可变更状态
        /// </summary>
        public Boolean BoqChange
        {
            get
            {
                return BoqStat == BoqStat.Locked;
            }
        }
        public ContractBoq boq;
        /// <summary>
        /// 项目清单数据
        /// </summary>
        public ContractBoq Boq
        {
            get
            {
                return boq;
            }
            private set
            {
                if (value == null)
                    BoqStat = BoqStat.Absent;
                boq = value;
                if (value != null)
                {
                    switch (value.ExecuteStat)
                    {
                        case 1:
                            BoqStat = BoqStat.UnLock;
                            break;
                        case 2:
                            BoqStat = BoqStat.Locked;
                            break;
                        case 3:
                            BoqStat = BoqStat.Changing;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 节点列表(用于绑定)
        /// </summary>
        public BindingList<ContractBoiChangeNode> NodeList { get; private set; }
        /// <summary>
        /// 记录头列表
        /// </summary>
        private List<ContractBoiChangeNode> RootList { get; set; }
        /// <summary>
        /// 记录更新列表
        /// </summary>
        private List<ContractBoiChangeNode> updateList { get; set; }
        /// <summary>
        /// 记录删除列表
        /// </summary>
        private List<ContractBoiChangeNode> deleteList { get; set; }
        private BindingList<ContractBoqChangeDetailEx> ChangeDetailExList { get; set; }
        #endregion
        #region 构造方法
        public ContractBoqChanegEditViewModel(BindingList<ContractBoqChangeDetailEx> ChangeDetailExList, String ProjectNo, String ProjectName)
        {
            NodeList = new BindingList<ContractBoiChangeNode>();
            this.ProjectNo = ProjectNo;
            this.ProjectName = ProjectName;
            this.ChangeDetailExList = ChangeDetailExList;
            contractBoqService = new MeteringPaymentClient().GetIContractBoqService();
            RootList = new List<ContractBoiChangeNode>();
            updateList = new List<ContractBoiChangeNode>();
            deleteList = new List<ContractBoiChangeNode>();
        }
        #endregion
        #region 事件处理
        /// <summary>
        /// 列表项更改处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNodeList_ListChanged(object sender, ListChangedEventArgs e)
        {
            //修改项
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                ContractBoiChangeNode boiNode = NodeList[e.NewIndex];
                CalcNode(boiNode);
                if (!updateList.Contains(boiNode))
                    updateList.Add(boiNode);
            }
        }
        private void InitChanged()
        {
            List<ContractBoqChangeDetailEx> lstDetail = ChangeDetailExList.ToList();
            List<ContractBoqChangeDetailEx> lstDelete = lstDetail.FindAll(m => m.ChangeType == ChangeType.Delete).OrderByDescending(m => m.ItemCode).ToList();
            List<ContractBoqChangeDetailEx> lstUpdate = lstDetail.FindAll(m => m.ChangeType == ChangeType.Update).OrderByDescending(m => m.ItemCode).ToList();
            List<ContractBoqChangeDetailEx> lstNew = lstDetail.FindAll(m => m.ChangeType == ChangeType.Add).OrderBy(m => m.ItemCode).ToList();
            List<ContractBoqChangeDetailEx> lstDiable = lstDetail.FindAll(m => m.ChangeType == ChangeType.Disable).OrderByDescending(m => m.ItemCode).ToList();
            List<ContractBoqChangeDetailEx> lstEnable = lstDetail.FindAll(m => m.ChangeType == ChangeType.Enable).OrderByDescending(m => m.ItemCode).ToList();
            List<ContractBoiChangeNode> lstNode = NodeList.ToList();
            lstDelete.ForEach(m =>
            {
                ContractBoiChangeNode node = lstNode.FirstOrDefault(n => n.ItemNo == m.ItemNo);
                node.ChangeDetailEx = m;
                ChangeNodeStat(node, false);
            });
            lstNew.ForEach(m =>
            {
                ContractBoiChangeNode nodeAdd = HDAutoMapper.DynamicMap<ContractBoiChangeNode>(m);
                nodeAdd.CtrctPrjPrice = m.AfPrice;
                nodeAdd.CtrctQty = m.AfQty;
                nodeAdd.CtrctAmount = m.AfAmount;
                nodeAdd.ChangeDetailEx = m;
                ContractBoiChangeNode parentNode = lstNode.FirstOrDefault(n => n.ItemCode == nodeAdd.ParentCode);
                if (parentNode != null)
                {
                    parentNode.Children.Add(nodeAdd);
                }
                else
                {
                    RootList.Add(nodeAdd);
                }
                NodeList.Add(nodeAdd);
            });
            lstUpdate.ForEach(m =>
            {
                ContractBoiChangeNode node = lstNode.Find(n => n.ItemNo == m.ItemNo);
                node.CtrctAmount = m.AfAmount;
                node.CtrctPrjPrice = m.AfPrice;
                node.CtrctQty = m.AfQty;
                node.IItemCoe = m.IItemCoe;
                node.Uom = m.Uom;
                node.ItemName = m.ItemName;
                node.ChangeDetailEx = m;
            });
            lstDiable.ForEach(m =>
            {
                ContractBoiChangeNode node = lstNode.Find(n => n.ItemNo == m.ItemNo);
                node.ChangeDetailEx = m;
                ChangeNodeStat(node, false);
            });
            lstEnable.ForEach(m =>
            {
                ContractBoiChangeNode node = lstNode.Find(n => n.ItemNo == m.ItemNo);
                node.ChangeDetailEx = m;
                ChangeNodeStat(node, true);
            });
        }

        /// <summary>
        /// 检查节点是否变化
        /// </summary>
        /// <param name="Node"></param>
        /// <returns></returns>
        bool HasNodeChanged(ContractBoiChangeNode Node)
        {
            ContractBoi boi = Node;
            return boi.IItemCoe != Node.IItemCoe
                || boi.ItemCode == Node.ItemCode
                || boi.ItemName != Node.ItemName
                || boi.Uom != Node.Uom
                || boi.CtrctAmount != Node.CtrctAmount
                || boi.CtrctPrjPrice != Node.CtrctPrjPrice
                || boi.CtrctQty != Node.CtrctQty
                || boi.StatId != Node.StatId;
        }
        /// <summary>
        /// 检查节点基本信息是否变化
        /// </summary>
        /// <param name="Node"></param>
        /// <returns></returns>
        bool HasInfoChanged(ContractBoiChangeNode Node)
        {
            ContractBoi boi = Node;
            return boi.IItemCoe != Node.IItemCoe
                || boi.ItemCode == Node.ItemCode
                || boi.ItemName != Node.ItemName
                || boi.Uom != Node.Uom;
        }
        /// <summary>
        /// 检查节点数量是否变化
        /// </summary>
        /// <param name="Node"></param>
        /// <returns></returns>
        bool HasQtyChanged(ContractBoiChangeNode Node)
        {
            ContractBoi boi = Node.OriginalBoi;
            return boi.CtrctQty != Node.CtrctQty;
        }
        /// <summary>
        /// 检查节点单价是否变化
        /// </summary>
        /// <param name="Node"></param>
        /// <returns></returns>
        bool HasPriceChanged(ContractBoiChangeNode Node)
        {
            ContractBoi boi = Node.OriginalBoi;
            return boi.CtrctPrjPrice != Node.CtrctPrjPrice;
        }
        /// <summary>
        /// 检查节点是否禁用、启用变化
        /// </summary>
        /// <param name="Node"></param>
        /// <returns></returns>
        bool HasDisableChanged(ContractBoiChangeNode Node)
        {
            ContractBoi boi = Node;
            return boi.StatId != Node.StatId;
        }
        /// <summary>
        /// 处理变化的节点
        /// </summary>
        /// <param name="Node"></param>
        void HandleChange(ContractBoiChangeNode Node)
        {
            bool hasChanged = HasNodeChanged(Node);
            if (hasChanged)
            {
                UpdateChangeEx(Node);
            }
            else if (Node.ChangeDetailEx != null)
            {
                ChangeDetailExList.Remove(Node.ChangeDetailEx);
                Node.ChangeDetailEx = null;
            }
        }
        /// <summary>
        /// 更新扩展对象新增和修改的值
        /// </summary>
        /// <param name="Node"></param>
        void UpdateChangeEx(ContractBoiChangeNode Node)
        {
            if (Node.ChangeDetailEx == null)
            {
                Node.ChangeDetailEx = HDAutoMapper.DynamicMap<ContractBoqChangeDetailEx>(Node);
                ChangeDetailExList.Add(Node.ChangeDetailEx);
            }
            Node.ChangeDetailEx.AfAmount = Node.CtrctAmount;
            Node.ChangeDetailEx.AfPrice = Node.CtrctPrjPrice;
            Node.ChangeDetailEx.AfQty = Node.CtrctQty;
            Node.ChangeDetailEx.ItemCode = Node.ItemCode;
            Node.ChangeDetailEx.IItemCoe = Node.IItemCoe;
            Node.ChangeDetailEx.ItemName = Node.ItemName;
            Node.ChangeDetailEx.ParentCode = Node.ParentCode;
            Node.ChangeDetailEx.ItemNo = Node.ItemNo;
            Node.ChangeDetailEx.Uom = Node.Uom;
            if (Node.OriginalBoi != null)
            {
                Node.ChangeDetailEx.BefAmount = Node.OriginalBoi.CtrctAmount;
                Node.ChangeDetailEx.BefPrjPrice = Node.OriginalBoi.CtrctPrjPrice;
                Node.ChangeDetailEx.BefQty = Node.OriginalBoi.CtrctQty;
                Node.ChangeDetailEx.ChangeQty = Node.CtrctQty - Node.OriginalBoi.CtrctQty;
                Node.ChangeDetailEx.ChangePrice = Node.CtrctPrjPrice - Node.OriginalBoi.CtrctPrjPrice;
                Node.ChangeDetailEx.ChangeAmount = Node.CtrctAmount - Node.OriginalBoi.CtrctAmount;
                if (Node.StatId != Node.OriginalBoi.StatId)
                {
                    Node.ChangeDetailEx.ChangeType = Node.StatId == 1 ? ChangeType.Enable : ChangeType.Disable;
                    if (Node.StatId == 0)
                    {
                        Node.ChangeDetailEx.BefAmount = Node.OriginalBoi.CtrctAmount;
                        Node.ChangeDetailEx.BefPrjPrice = Node.OriginalBoi.CtrctPrjPrice;
                        Node.ChangeDetailEx.BefQty = Node.OriginalBoi.CtrctQty;
                        Node.ChangeDetailEx.AfAmount = 0;
                        Node.ChangeDetailEx.AfPrice = 0;
                        Node.ChangeDetailEx.AfQty = 0;
                        Node.ChangeDetailEx.ChangeQty = 0 - Node.OriginalBoi.CtrctQty;
                        Node.ChangeDetailEx.ChangePrice = 0 - Node.OriginalBoi.CtrctPrjPrice;
                        Node.ChangeDetailEx.ChangeAmount = 0 - Node.OriginalBoi.CtrctAmount;
                    }
                    else
                    {
                        Node.ChangeDetailEx.BefAmount = 0;
                        Node.ChangeDetailEx.BefPrjPrice = 0;
                        Node.ChangeDetailEx.BefQty = 0;
                        Node.ChangeDetailEx.AfAmount = Node.OriginalBoi.CtrctAmount;
                        Node.ChangeDetailEx.AfPrice = Node.OriginalBoi.CtrctPrjPrice;
                        Node.ChangeDetailEx.AfQty = Node.OriginalBoi.CtrctQty;
                        Node.ChangeDetailEx.ChangeQty = Node.OriginalBoi.CtrctQty;
                        Node.ChangeDetailEx.ChangePrice = Node.OriginalBoi.CtrctPrjPrice;
                        Node.ChangeDetailEx.ChangeAmount = Node.OriginalBoi.CtrctAmount;

                    }
                }
                else
                {
                    Node.ChangeDetailEx.ChangeType = ChangeType.Update;
                    Node.ChangeDetailEx.IsUpQty = HasQtyChanged(Node);
                    Node.ChangeDetailEx.IsUpPrice = HasPriceChanged(Node);
                    Node.ChangeDetailEx.IsUpInfo = HasInfoChanged(Node);
                }
            }
            else
            {
                Node.ChangeDetailEx.BefAmount = 0;
                Node.ChangeDetailEx.BefPrjPrice = 0;
                Node.ChangeDetailEx.BefQty = 0;
                Node.ChangeDetailEx.ChangeQty = Node.CtrctQty;
                Node.ChangeDetailEx.ChangePrice = Node.CtrctPrjPrice;
                Node.ChangeDetailEx.ChangeAmount = Node.CtrctAmount;
                Node.ChangeDetailEx.ChangeType = ChangeType.Add;
            }
        }
        void DeleteChangeEx(ContractBoiChangeNode Node)
        {
            if (Node.ChangeDetailEx == null)
            {
                Node.ChangeDetailEx = HDAutoMapper.DynamicMap<ContractBoqChangeDetailEx>(Node);
                ChangeDetailExList.Add(Node.ChangeDetailEx);
            }
            Node.ChangeDetailEx.AfAmount = 0;
            Node.ChangeDetailEx.AfPrice = 0;
            Node.ChangeDetailEx.AfQty = 0;
            Node.ChangeDetailEx.BefAmount = Node.OriginalBoi.CtrctAmount;
            Node.ChangeDetailEx.BefPrjPrice = Node.OriginalBoi.CtrctPrjPrice;
            Node.ChangeDetailEx.BefQty = Node.OriginalBoi.CtrctQty;
            Node.ChangeDetailEx.ChangeQty = 0 - Node.OriginalBoi.CtrctQty;
            Node.ChangeDetailEx.ChangePrice = 0 - Node.OriginalBoi.CtrctPrjPrice;
            Node.ChangeDetailEx.ChangeAmount = 0 - Node.OriginalBoi.CtrctAmount;
            //if (Node.StatId == 0)
            //{
            //    Node.ChangeDetailEx.ChangeType = ChangeType.Disable;
            //}
            //else
            //{
                Node.ChangeDetailEx.ChangeType = ChangeType.Delete;
            //}
        }
        #endregion
        #region 公有方法
        /// <summary>
        /// 装载清单数据
        /// </summary>
        public void Load()
        {
            RootList.Clear();
            NodeList.Clear();
            updateList.Clear();
            List<ContractBoiChangeNode> lstNode = new List<ContractBoiChangeNode>();
            Boq = contractBoqService.GetByProjectNo(ProjectNo);
            lstNode = Convert(Boq.BoiList);
            ////初始化批复值
            //RootList.ForEach(m =>
            //{
            //    InitReply(lstNode, m);
            //});
            lstNode.ForEach(m => NodeList.Add(m));
            NodeList.ListChanged += OnNodeList_ListChanged;
            RootList.ForEach(m => m.PropertyChanged += M_PropertyChanged);
            InitChanged();
        }

        private void M_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //NewTotalReplyAmount = CalcNewTotalReplyAmount();
        }

        /// <summary>
        /// 插入节点
        /// </summary>
        /// <param name="ParentNode"></param>
        public ContractBoiChangeNode InsertNode(ContractBoiChangeNode ParentNode = null)
        {
            String strParentCode;
            String strMaxCode;
            String strNewCode;
            int iNewCode;
            ContractBoiChangeNode nodeNew = new ContractBoiChangeNode();
            if (ParentNode != null)
            {
                strParentCode = ParentNode.ItemCode;
                strMaxCode = ParentNode.Children.Max(m => m.ItemCode);
                if (String.IsNullOrEmpty(strMaxCode))
                {
                    strNewCode = strParentCode + "001";
                }
                else
                {
                    iNewCode = System.Convert.ToInt32(strMaxCode.Substring(strMaxCode.Length - 3, 3)) + 1;
                    if (iNewCode > 999)
                    {
                        throw new BusinessException("清单项超出限制（子项不多余999项）");
                    }
                    strNewCode = strParentCode + iNewCode.ToString().PadLeft(3, '0');
                }
                ParentNode.Children.Add(nodeNew);
                nodeNew.ParentCode = ParentNode.ItemCode;

            }
            else
            {
                strMaxCode = RootList.Max(m => m.ItemCode);
                if (String.IsNullOrEmpty(strMaxCode))
                {
                    strNewCode = "01";
                }
                else
                {
                    iNewCode = System.Convert.ToInt32(strMaxCode) + 1;
                    if (iNewCode > 99)
                    {
                        throw new BusinessException("清单项超出限制（顶层项不多余99项）");
                    }
                    strNewCode = iNewCode.ToString().PadLeft(2, '0');
                }
                RootList.Add(nodeNew);
                nodeNew.PropertyChanged += M_PropertyChanged;
            }
            nodeNew.ItemCode = strNewCode;
            nodeNew.ProjectNo = ProjectNo;
            nodeNew.BoQNo = Boq.BoQNo;
            nodeNew.ParentBoiNode = ParentNode;
            CalcNode(nodeNew);
            NodeList.Add(nodeNew);
            return nodeNew;
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="Node"></param>
        public void DeleteNode(ContractBoiChangeNode Node)
        {
            //已删除过，则返回
            if (!NodeList.Contains(Node)) return;
            List<ContractBoiChangeNode> lstChildren = Node.Children.ToList();
            for (int i = lstChildren.Count - 1; i >= 0; i--)
            {
                DeleteNode(lstChildren[i]);
            }
            if (NodeList.Contains(Node))
                NodeList.Remove(Node);
            if (updateList.Contains(Node))
                updateList.Remove(Node);
            if (!deleteList.Contains(Node))
                deleteList.Add(Node);
            String strCode = null;
            int iCode = 0;
            if (Node.ParentBoiNode != null)
            {
                //删除节点的序号
                int iIndex = Node.ParentBoiNode.Children.IndexOf(Node);
                //删除节点不是最后一个
                if (iIndex < Node.ParentBoiNode.Children.Count - 1)
                {
                    //移除节点
                    Node.ParentBoiNode.Children.Remove(Node);
                    for (int i = iIndex; i < Node.ParentBoiNode.Children.Count; i++)
                    {
                        //对后面的节点的序号 统一减1
                        strCode = Node.ParentBoiNode.Children[i].ItemCode;
                        iCode = System.Convert.ToInt32(strCode.Substring(strCode.Length - 3, 3));
                        iCode = iCode - 1;
                        strCode = Node.ParentBoiNode.ItemCode + iCode.ToString().PadLeft(3, '0');
                        ChangeCode(Node.ParentBoiNode.Children[i], strCode);
                    }
                }
                CalcParentNode(Node.ParentBoiNode);
            }
            else
            {
                int iIndex = RootList.IndexOf(Node);
                //删除节点不是最后一个
                if (iIndex < RootList.Count - 1)
                {
                    //移除节点
                    RootList.Remove(Node);
                    for (int i = iIndex; i < RootList.Count; i++)
                    {
                        //对后面的节点的序号 统一减1
                        strCode = RootList[i].ItemCode;
                        iCode = System.Convert.ToInt32(strCode);
                        iCode = iCode - 1;
                        strCode = iCode.ToString().PadLeft(2, '0');
                        RootList[i].ItemCode = strCode;
                        ChangeCode(RootList[i], strCode);
                    }
                }
                else
                {
                    RootList.Remove(Node);
                }
            }

        }
        /// <summary>
        /// 禁用和启用项
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="Stat">true-启用项，false-禁用项</param>
        public void ChangeNodeStat(ContractBoiChangeNode Node, Boolean Stat)
        {

            for (int i = Node.Children.Count - 1; i >= 0; i--)
            {
                ChangeNodeStat(Node.Children[i], Stat);
                //if (!Stat && !Node.Children[i].IsUse)
                //{
                //    Node.Children.RemoveAt(i);
                //    RecursionDeleteNode(Node.Children[i]);
                //}
            }
            //updated by stang 20170603
            //规则更改：关联原始清单项，则只能禁用
            if (Node.OriginalBoi != null)
            //  if (Node.IsUse)
            {
                Node.StatId = Stat ? 1 : 0;
                if (!Stat)
                {
                    if (!deleteList.Contains(Node))
                        deleteList.Add(Node);
                }
                else
                {
                    if (deleteList.Contains(Node))
                        deleteList.Remove(Node);
                }
            }
            else
            {
                if (Node.ParentBoiNode != null)
                {
                    Node.ParentBoiNode.Children.Remove(Node);
                }
                DeleteNode(Node);
            }
        }
        public String Verification()
        {
            String error = null;
            List<ContractBoiChangeNode> lstAllNode = NodeList.ToList();
            if (lstAllNode.Exists(m => String.IsNullOrEmpty(m.ItemName)))
            {
                error = "请填写清单项名称";
            }
            return error;
        }
        /// <summary>
        /// 保存更改
        /// </summary>

        public void SaveChanged()
        {

            List<ContractBoiChangeNode> lstAllNode = NodeList.ToList();
            //添加的节点集合
            List<ContractBoiChangeNode> lstAddNode = lstAllNode.FindAll(m => m.OriginalBoi == null && m.ChangeDetailEx == null);
            //删除的节点集合
            List<ContractBoiChangeNode> lstDelNode = deleteList;
            //移除ItemNo为空的删除项
            List<ContractBoiChangeNode> lstNull = deleteList.FindAll(m => String.IsNullOrEmpty(m.ItemNo));
            lstNull.ForEach(m => deleteList.Remove(m));
            //修改的
            updateList.ForEach(m =>
            {
                HandleChange(m);
            });
            //新增
            lstAddNode.ForEach(m =>
            {
                UpdateChangeEx(m);
            });
            //删除的
            deleteList.ForEach(m =>
            {
                DeleteChangeEx(m);
            });

            //将编辑状态设置为FALSE
            this.Editing = false;
        }
        public void Lock()
        {
            //锁定
            contractBoqService.ChangeStat(Boq.BoQNo, 2);
            Boq.ExecuteStat = 2;
        }
        #endregion
        #region 内部方法
        private void ChangeCode(ContractBoiChangeNode node, String newCode)
        {
            String strOldCode = node.ItemCode;
            node.ItemCode = newCode;
            if (node.Children.Count > 0)
            {
                node.Children.ForEach(m =>
                {
                    m.ParentCode = newCode;
                    ChangeCode(m, newCode + m.ItemCode.Substring(m.ItemCode.Length - 3, 3));
                });
            }
        }
        /// <summary>
        /// 将Boi列表转换为Node列表
        /// </summary>
        /// <param name="BoiList"></param>
        /// <returns></returns>
        private List<ContractBoiChangeNode> Convert(List<ContractBoi> BoiList)
        {
            List<ContractBoiChangeNode> lstBoiNode;
            List<ContractBoiChangeNode> lstRoot;
            lstBoiNode = BoiList.ConvertAll<ContractBoiChangeNode>(m =>
            {
                ContractBoiChangeNode node = HDAutoMapper.DynamicMap<ContractBoiChangeNode>(m);
                //生成变更的编辑界面使用合同字段作为最新的值，因此 将变更后的值赋值给合同字段
                node.CtrctAmount = node.LatestAmount;
                node.CtrctPrjPrice = node.LatestPrice;
                node.CtrctQty = node.LatestQty;
                node.OriginalBoi = m;
                return node;
            });
            lstRoot = lstBoiNode.FindAll(m => String.IsNullOrEmpty(m.ParentCode));
            RootList.AddRange(lstRoot);
            lstRoot.ForEach(m =>
            {
                RecursionNode(lstBoiNode, m);
            });
            return lstBoiNode;
        }

        /// <summary>
        /// 清单项值计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalcNode(ContractBoiChangeNode Node)
        {
            if (Node.Children.Count == 0)
            {
                Node.CtrctAmount = Math.Round(ObjectHelper.GetDefaultDecimal(Node.CtrctQty) * ObjectHelper.GetDefaultDecimal(Node.CtrctPrjPrice), 2, MidpointRounding.AwayFromZero);
            }
            if (Node.ParentBoiNode != null)
            {
                Node.ParentBoiNode.CtrctQty = null;
                Node.ParentBoiNode.CtrctPrjPrice = null;
                Node.ParentBoiNode.CtrctAmount = Node.ParentBoiNode.Children.Sum(m => m.StatId == 1 ? m.CtrctAmount : 0);
            }
        }
        /// <summary>
        /// 清单父项值计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalcParentNode(ContractBoiChangeNode ParentNode)
        {
            if (ParentNode.Children.Count == 0)
            {
                ParentNode.CtrctAmount = Math.Round(ObjectHelper.GetDefaultDecimal(ParentNode.CtrctQty) * ObjectHelper.GetDefaultDecimal(ParentNode.CtrctPrjPrice), 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                ParentNode.CtrctQty = null;
                ParentNode.CtrctPrjPrice = null;
                ParentNode.CtrctAmount = ParentNode.Children.Sum(m => m.StatId == 1 ? m.CtrctAmount : 0);
            }
        }
        /// <summary>
        /// 递归创建父子关系
        /// </summary>
        /// <param name="NodeList"></param>
        /// <param name="Parent"></param>
        private void RecursionNode(List<ContractBoiChangeNode> NodeList, ContractBoiChangeNode Parent)
        {
            List<ContractBoiChangeNode> lstChildren = NodeList.FindAll(m => m.ParentCode == Parent.ItemCode);
            if (lstChildren.Count > 0)
            {
                Parent.Children.AddRange(lstChildren);
                lstChildren.ForEach(m =>
                {
                    m.ParentBoiNode = Parent;
                    RecursionNode(NodeList, m);
                });
            }
        }
        
        #endregion
    }

    /// <summary>
    /// 清单扩展节点，用于记录父子节点
    /// </summary>
    public class ContractBoiChangeNode : ContractBoi
    {

        /// <summary>
        /// 父级
        /// </summary>
        public ContractBoiChangeNode ParentBoiNode { get; set; }
        /// <summary>
        /// 子集集合
        /// </summary>
        public List<ContractBoiChangeNode> Children { get; set; }
        public ContractBoi OriginalBoi { get; set; }
        /// <summary>
        /// 关联的变更明细对象
        /// </summary>
        public ContractBoqChangeDetailEx ChangeDetailEx { get; set; }
        public ContractBoiChangeNode()
        {
            Children = new List<ContractBoiChangeNode>();
        }
    }
    /// <summary>
    /// 清单变更类型
    /// </summary> 
    public enum ChangeType
    {
        /// <summary>
        /// 
        /// </summary>
        NoChange = 0,

        /// <summary>
        /// 添加
        /// </summary>
        Add = 1,
        /// <summary>
        /// 修改
        /// </summary>
        Update = 2,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 3,
        /// <summary>
        /// 禁用
        /// </summary>
        Disable = 4,
        /// <summary>
        /// 禁用
        /// </summary>
        Enable = 5

    }
}
