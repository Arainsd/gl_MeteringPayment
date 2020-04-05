using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Native;
using Erp.CommonData.Tool;
using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Domain.Entity.ContractBoqEntity;
using HD.MeteringPayment.Domain.Entity.WBSBoqEntity;
using HD.MeteringPayment.Module.BootLoader.Config;
using Hondee.Common.Advance.Util;
using Hondee.Common.HDException;
using Hondee.Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace HD.MeteringPayment.Module.Forms.WBSBoqMng.ViewModel
{
    public partial class WBSBoqViewModel
    {
        #region 成员
        private IContractBoq contractBoqService;
        private IWBSBoq WBSBoqService;
        /// <summary>
        /// 清单状态
        /// </summary>
        public BoqStat BoqStat { get; private set; }
        public String ProjectNo { get; private set; }
        public String ProjectName { get; private set; }
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
                return BoqStat == BoqStat.Locked || BoqStat == BoqStat.Changing;
            }
        }
        public Boolean BoqFixedStat
        {
            get
            {
                if(Boq == null)
                {
                    return true;
                }
                return Boq.Fixed;
            }
        }
        public WBSBoq boq;
        /// <summary>
        /// WBS清单数据
        /// </summary>
        public WBSBoq Boq
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
        /// WBS节点列表(用于绑定)
        /// </summary>
        public BindingList<WBSLineNode> NodeBindingSource { get; private set; }
        /// <summary>
        /// 关联关系列表(用于绑定)
        /// </summary>
        public BindingList<WBSline_boi> RelationBindingSource { get; private set; }
        /// <summary>
        /// 根节点列表
        /// </summary>
        private List<WBSLineNode> RootList { get; set; }
        /// <summary>
        /// 更新节点列表
        /// </summary>
        private List<WBSLineNode> updateList { get; set; }

        /// <summary>
        /// 关联关系更新列表
        /// </summary>
        public List<WBSline_boi> updateRelationList { get; set; }

        /// <summary>
        /// 合同清单剩余项
        /// </summary>
        public List<ContractBoi> remainContractBoiList { get; set; }

        /// <summary>
        /// 合同清单原始项
        /// </summary>
        public List<ContractBoi> originContractBoiList { get; set; }
        private WBSLineNode _currentNode;
        /// <summary>
        /// 当前选中的Node
        /// </summary>
        public WBSLineNode CurrentSelectedNode
        {
            get
            {
                return _currentNode;
            }
            set
            {
                _currentNode = value;
                RelationBindingSource.ListChanged -= OnRelationNodeList_ListChanged;
                RelationBindingSource.Clear();
                List<WBSline_boi> wbsLineBois = new List<WBSline_boi>();
                List<WBSline_boi> tempList = new List<WBSline_boi>();
                Dictionary<String, WBSline_boi> dicWbsLineBoi = new Dictionary<string, WBSline_boi>();
                WBSline_boi tempObj = null;
                // if (value != null && value.RelationList != null && value.RelationList.Count > 0)
                if (value != null)
                {                         
                     wbsLineBois = RecursionFindRelationShip(value).Where(m=>m.StatId==1).ToList();
                    if (value.Children.Count > 0) {
                        wbsLineBois.ForEach(m => {

                            if(!dicWbsLineBoi.TryGetValue(m.ItemNo,out tempObj))
                            {
                                dicWbsLineBoi.Add(m.ItemNo, ObjectHelper.Clone(m));
                                tempList.Add(dicWbsLineBoi[m.ItemNo]);
                                tempObj = dicWbsLineBoi[m.ItemNo];
                            }   
                            else
                            {
                                tempObj.Amount += m.Amount;
                                tempObj.Qty += m.Qty;  
                            }
                        });
                    }
                    else
                    {
                        tempList = wbsLineBois;
                    }
                    
                }
                //tempList.ForEach(m =>
                //{
                //    RelationBindingSource.Add(m);
                //});
                if(tempList != null)
                {
                    RelationBindingSource = new BindingList<WBSline_boi>(tempList);
                }
                else
                {
                    RelationBindingSource = new BindingList<WBSline_boi>();
                }
                RelationBindingSource.ListChanged += OnRelationNodeList_ListChanged;
                RelationBindingSource.ResetBindings();
                if(RelationListChanged != null)
                {
                    RelationListChanged();
                }
            }
        } 

        /// <summary>
        /// 列表改变事件
        /// </summary>
        public Action WbsListChanged;
        /// <summary>
        /// 关联关系列表改变事件
        /// </summary>
        public Action RelationListChanged;
        #endregion
        #region 构造方法
        public WBSBoqViewModel(String ProjectNo, String ProjectName)
        {
            NodeBindingSource = new BindingList<WBSLineNode>();
            RelationBindingSource = new BindingList<WBSline_boi>();
            this.ProjectNo = ProjectNo;
            this.ProjectName = ProjectName;
            contractBoqService = new MeteringPaymentClient().GetIContractBoqService();
            WBSBoqService = new MeteringPaymentClient().GetIWBSBoqService();
            RootList = new List<WBSLineNode>();
            updateList = new List<WBSLineNode>();
            updateRelationList = new List<WBSline_boi>();
            remainContractBoiList = new List<ContractBoi>();
        }
        #endregion
        #region 事件处理
        /// <summary>
        /// WBS清单列表项更改处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWbsNodeList_ListChanged(object sender, ListChangedEventArgs e)
        {
            //修改项
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                WBSLineNode boiNode = NodeBindingSource[e.NewIndex];
                CalcNode(boiNode);
                if (!String.IsNullOrEmpty(boiNode.WbsLineNo) && !updateList.Contains(boiNode))
                    updateList.Add(boiNode);
            }
        }

        /// <summary>
        /// 关联关系项更改处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRelationNodeList_ListChanged(object sender, ListChangedEventArgs e)
        {
            //修改项
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                if (e.PropertyDescriptor.Name == "Amount")
                {
                    return;
                }
                WBSline_boi updatedRelation = RelationBindingSource[e.NewIndex];
                updatedRelation.Amount = (updatedRelation.Qty ?? 0) * (updatedRelation.CtrctPrjPrice ?? 0);

                //计算未关联项数量和金额
                ContractBoi remainBoi = remainContractBoiList.Find(m => m.ItemNo == updatedRelation.ItemNo);
                ContractBoi originBoi = originContractBoiList.Find(m => m.ItemNo == updatedRelation.ItemNo);
                if (remainBoi != null && originBoi != null)
                {
                    decimal qty = 0, amount = 0;
                    foreach (WBSline_boi relation in boq.AllRelationList)
                    {
                        if (relation.StatId == 1 && relation.ItemNo == remainBoi.ItemNo)
                        {
                            qty += ObjectHelper.GetDefaultDecimal(relation.Qty);
                            amount += ObjectHelper.GetDefaultDecimal(relation.Amount);
                        }
                    }
                    remainBoi.CtrctQty = (originBoi.CtrctQty ?? 0) - qty;
                    remainBoi.CtrctAmount = originBoi.CtrctAmount - amount;
                }

                //计算WBS节点金额
                List<WBSLineNode> allNode = NodeBindingSource.ToList();
                WBSLineNode boiNode = allNode != null ? allNode.Find(m => m.WbsSysCode == updatedRelation.WbsSysCode) : null;
                if (boiNode != null)
                {
                    CalcNode(boiNode);
                }

                if (!updateRelationList.Contains(updatedRelation))
                {
                    updateRelationList.Add(updatedRelation);
                }
            }

        }
        #endregion
        #region 公有方法
        /// <summary>
        /// 装载清单数据
        /// </summary>
        public void Load()
        {
            NodeBindingSource.ListChanged -= OnWbsNodeList_ListChanged;
            RelationBindingSource.ListChanged -= OnRelationNodeList_ListChanged;

            RootList.Clear();
            NodeBindingSource.Clear();
            updateList.Clear();
            List<WBSLineNode> lstNode = new List<WBSLineNode>();
            //Boq = WBSBoqService.GetByProjectNo(ProjectNo);//读取Boq
            Boq = DecompressConvertToApplyBoq(WBSBoqService.GetBytesByProjectNo(ProjectNo));
            if (Boq != null)
            {
                lstNode = Convert(Boq.WBSlineList, Boq.AllRelationList);
                //lstNode.ForEach(m => NodeBindingSource.Add(m));
                NodeBindingSource = new BindingList<WBSLineNode>(lstNode);
            }
            else
            {
                Boq = new WBSBoq();
                Boq.ProjectNo = ProjectNo;
                Boq.WbsName = ProjectName;
            }
            NodeBindingSource.ListChanged += OnWbsNodeList_ListChanged;
            RelationBindingSource.ListChanged += OnRelationNodeList_ListChanged;

            ContractBoq contractBoq = contractBoqService.GetByProjectNo(ProjectNo); //读取合同清单
            if (contractBoq != null && contractBoq.BoiList != null && contractBoq.BoiList.Count > 0)
            {
                originContractBoiList = new List<ContractBoi>();
                remainContractBoiList = new List<ContractBoi>();
                contractBoq.BoiList.ForEach(m =>
                    {
                        ContractBoi boiOrigin = ObjectHelper.Clone<ContractBoi>(m);  //获得合同清单原始值
                        originContractBoiList.Add(boiOrigin);

                        ContractBoi boiRemain = ObjectHelper.Clone<ContractBoi>(m);  //获得合同清单剩余值
                        List<WBSline_boi> Relations = boq.AllRelationList.FindAll(n => n.ItemNo == m.ItemNo);
                        if (Relations != null && Relations.Count > 0)
                        {
                            decimal Qty = 0;
                            foreach (WBSline_boi relation in Relations)
                            {
                                Qty += ObjectHelper.GetDefaultDecimal(relation.Qty);
                            }

                            boiRemain.CtrctQty = ObjectHelper.GetDefaultDecimal(boiOrigin.CtrctQty) - Qty;
                            boiRemain.CtrctAmount = ObjectHelper.GetDefaultDecimal(boiRemain.CtrctQty) * ObjectHelper.GetDefaultDecimal(boiRemain.CtrctPrjPrice);
                        }
                        remainContractBoiList.Add(boiRemain);
                    });
            }

            if (WbsListChanged != null)
            {
                WbsListChanged();
            }
        }
        /// <summary>
        /// 从字节流解压到DateSet在转换为WBSBoq
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private WBSBoq DecompressConvertToApplyBoq(Byte[] data)
        {
            WBSBoq result = null;

            DataSet ds = Erp.CommonData.DataZip.DecompressConvertToDataSet(data);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                    result = CollectionHelper.CreateItem<WBSBoq>(ds.Tables[0].Rows[0]);
                if (ds.Tables["change_applywbs"] != null && ds.Tables["change_applywbs"].Rows.Count > 0)
                    result.WBSlineList = CollectionHelper.ConvertTo<WBSline>(ds.Tables["change_applywbs"]).ToList();
                if (ds.Tables["change_applydetail"] != null && ds.Tables["change_applydetail"].Rows.Count > 0)
                    result.AllRelationList = CollectionHelper.ConvertTo<WBSline_boi>(ds.Tables["change_applydetail"]).ToList();
            }
            return result;
        }
        /// <summary>
        /// 插入节点
        /// </summary>
        /// <param name="ParentNode"></param>
        public WBSLineNode InsertNode(WBSLineNode ParentNode = null)
        {
            String strParentCode;
            String strMaxCode;
            String strNewCode;
            int iNewCode;
            WBSLineNode nodeNew = new WBSLineNode();
            if (ParentNode != null)  //插入子节点
            {
                strParentCode = ParentNode.WbsSysCode;
                strMaxCode = ParentNode.Children.Max(m => m.WbsSysCode);
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
                nodeNew.ParentCode = ParentNode.WbsSysCode;

            }
            else  //插入根节点
            {
                strMaxCode = RootList.Max(m => m.WbsSysCode);
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
            }
            nodeNew.WbsSysCode = strNewCode;
            nodeNew.ProjectNo = ProjectNo;
            nodeNew.WbsNo = Boq.WbsNo;
            nodeNew.ParentBoiNode = ParentNode;
            CalcNode(nodeNew);
            NodeBindingSource.Add(nodeNew);

            return nodeNew;
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="Node"></param>
        public void DeleteNode(WBSLineNode Node)
        {
            //已删除过，则返回
            if (!NodeBindingSource.Contains(Node)) return;
            //该节点存在关联关系，则返回
            List<WBSline_boi> deleteRelation = RecursionFindRelationShip(Node);
            if (deleteRelation != null && deleteRelation.Count > 0)
            {
                return;
            }
            List<WBSLineNode> lstChildren = Node.Children.ToList();
            for (int i = lstChildren.Count - 1; i >= 0; i--)
            {
                DeleteNode(lstChildren[i]);
            }
            if (NodeBindingSource.Contains(Node))
                NodeBindingSource.Remove(Node);
            if (updateList.Contains(Node))
                updateList.Remove(Node);
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
                        //对后面的节点的序号以及节点的关联关系的序号 统一减1
                        strCode = Node.ParentBoiNode.Children[i].WbsSysCode;
                        iCode = System.Convert.ToInt32(strCode.Substring(strCode.Length - 3, 3));
                        iCode = iCode - 1;
                        strCode = Node.ParentBoiNode.WbsSysCode + iCode.ToString().PadLeft(3, '0');
                        ChangeCode(Node.ParentBoiNode.Children[i], strCode);
                    }
                }
                else
                {
                    Node.ParentBoiNode.Children.Remove(Node);
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
                        strCode = RootList[i].WbsSysCode;
                        iCode = System.Convert.ToInt32(strCode);
                        iCode = iCode - 1;
                        strCode = iCode.ToString().PadLeft(2, '0');
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
        public void ChangeNodeStat(WBSLineNode Node, Boolean Stat)
        {
            if (Node.Children.Count > 0)
            {
                for (int i = Node.Children.Count - 1; i >= 0; i--)
                {
                    ChangeNodeStat(Node.Children[i], Stat);
                    if (!Stat && !Node.Children[i].IsUse)
                    {
                        Node.Children.RemoveAt(i);
                        RecursionDeleteNode(Node.Children[i]);
                    }
                }
            }
            if (Node.IsUse)
            {
                Node.StatId = Stat ? 1 : 0;
            }
            else
            {
                if (Node.ParentBoiNode != null)
                {
                    Node.ParentBoiNode.Children.Remove(Node);
                }
                RecursionDeleteNode(Node);
            }
        }
        /// <summary>
        /// 保存更改
        /// </summary>
        public void Save()
        {
            if (NodeBindingSource.Count == 0)
            {
                throw new BusinessException("至少有一条WBS清单明细！");
            }
            if (NodeBindingSource.ToList().Exists(m => String.IsNullOrEmpty(m.WbsLineName) || String.IsNullOrEmpty(m.WbsLineName.Trim())))
            {
                throw new BusinessException(" WBS清单项名称不能为空！");
            }
            List<WBSline> lstAddBoi = null;
            List<WBSline> lstDelBoi = null;
            List<WBSline> lstUpdateBoi = null;
            List<String> lstDelNo = null;
            List<WBSLineNode> lstAllNode = NodeBindingSource.ToList();

            List<WBSline_boi> lstAddRelation = null;
            List<WBSline_boi> lstUpdateRelation = null;
            List<WBSline_boi> lstDeleteReletion = null;

            //添加的节点集合
            List<WBSLineNode> lstAddNode = lstAllNode.FindAll(m => m.OriginalBoi == null);
            //删除的节点集合
            lstDelBoi = boq.WBSlineList.FindAll(m => !lstAllNode.Exists(n => n.OriginalBoi == m));
            //lstUpdateBoi = 
            if (lstAddNode.Count > 0)
            {
                lstAddBoi = lstAddNode.ConvertAll<WBSline>(m => ObjectHelper.Clone<WBSline>(m));
            }
            if (lstDelBoi.Count > 0)
            {
                lstDelNo = lstDelBoi.ConvertAll<String>(m => m.WbsLineNo);
            }
            if (updateList.Count > 0)
            {
                lstUpdateBoi = updateList.ConvertAll<WBSline>(m => ObjectHelper.Clone<WBSline>(m));
            }

            //添加的关系集合
            if (updateRelationList != null && updateRelationList.Count > 0)
            {
                lstAddRelation = updateRelationList.FindAll(m => m.Id <= 0);
            }
            //删除的关系集合
            if (boq.AllRelationList != null && boq.AllRelationList.Count > 0)
            {
                lstDeleteReletion = boq.AllRelationList.FindAll(m => m.Id > 0 && m.StatId == 0);
            }
            if (updateRelationList != null && updateRelationList.Count > 0)
            {
                lstUpdateRelation = updateRelationList.FindAll(m => m.Id > 0);
            }


            //执行保存操作
            WBSBoq result = WBSBoqService.Save(ProjectNo, null, Boq.WbsName, ObjectHelper.GetDefaultDecimal(Boq.TotalAmount)
                                                                 , lstAddBoi, lstUpdateBoi, lstDelNo
                                                                 , lstAddRelation, lstUpdateRelation, lstDeleteReletion);
            if (lstAddNode != null && lstAddNode.Count > 0)
            {
                lstAddNode.ForEach(m =>
                {
                    //为新增的项填写ItemNo
                    m.WbsLineNo = result.WBSlineList.Find(n => n.WbsSysCode == m.WbsSysCode).WbsLineNo;
                    //拷贝Boi对象
                    WBSline boi = ObjectHelper.Clone<WBSline>(m);
                    //将Boi对象与节点关联起来
                    m.OriginalBoi = boi;
                    Boq.WbsNo = m.WbsNo;
                    //加入到原始Boq集合中
                    Boq.WBSlineList.Add(boi);
                });
            }
            //从原始集合中移除已删除的对象
            lstDelBoi.ForEach(m =>
            {
                Boq.WBSlineList.Remove(m);
            });
            //清除变化记录集合
            updateList.Clear();

            if (lstAddRelation != null && lstAddRelation.Count > 0)
            {
                lstAddRelation.ForEach(m =>
                {
                    if (String.IsNullOrEmpty(m.WBSLineNo))
                    {
                        //为新增的项填写ItemNo
                        if (result.WBSlineList.Find(n => n.WbsSysCode == m.WbsSysCode) != null)
                        {
                            m.WBSLineNo = result.WBSlineList.Find(n => n.WbsSysCode == m.WbsSysCode).WbsLineNo;
                        }
                    }

                    //拷贝Boi对象
                    WBSline_boi boi = ObjectHelper.Clone<WBSline_boi>(m);
                    //将Boi对象与节点关联起来
                    WBSLineNode node = lstAllNode.Find(n => n.WbsSysCode == m.WbsSysCode);
                    node.OriginalRelationList.Add(boi);

                });
            }
            if (lstDeleteReletion != null && lstDeleteReletion.Count > 0)
            {
                //从集合中移除已删除的对象
                lstDeleteReletion.ForEach(m =>
                {
                    WBSLineNode node = lstAllNode.Find(n => n.WbsSysCode == m.WbsSysCode);
                    if(node != null)
                        node.OriginalRelationList.Remove(m);//从原始列表中移除
                    Boq.AllRelationList.Remove(m); //从全部关系列表中移除
                });
            }
            //清除变化记录集合
            updateRelationList.Clear();

            //将编辑状态设置为FALSE
            this.Editing = false;
            //重新读取数据
            Load();
        }
        public Boolean HasChanged()
        {
            Boolean hasChanged = false;      
            List<WBSline> lstDelBoi = null;       
            List<WBSLineNode> lstAllNode = NodeBindingSource.ToList();    

            //添加的节点集合
            List<WBSLineNode> lstAddNode = lstAllNode.FindAll(m => m.OriginalBoi == null);
            //删除的节点集合
            lstDelBoi = boq.WBSlineList.FindAll(m => !lstAllNode.Exists(n => n.OriginalBoi == m));       
            if (updateList.Count > 0)
            {
                hasChanged = true;
            }

            //添加的关系集合
            if (!hasChanged && updateRelationList != null && updateRelationList.Count > 0)
            {
                hasChanged = true;
            }
            //删除的关系集合
            if (!hasChanged&&boq.AllRelationList != null && boq.AllRelationList.Count > 0)
            {          if(boq.AllRelationList.FindAll(m => m.Id > 0 && m.StatId == 0).Count>0)
                             hasChanged = true;
            }
            if (!hasChanged && updateRelationList != null && updateRelationList.Count > 0)
            {
                if(updateRelationList.FindAll(m => m.Id > 0).Count > 0)
                {
                    hasChanged = true;
                }
            }
            return hasChanged;
        }
        /// <summary>
        /// 锁定
        /// </summary>
        public void Lock()
        {
            //锁定
            Boq.ExecuteStat = 2;
            WBSBoqService.ChangeStat(Boq);
            switch (Boq.ExecuteStat)
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
        /// <summary>
        /// 发布/撤销发布
        /// </summary>
        public void FixOrUnfix(bool Fixed)
        {
            switch (Boq.Fixed)
            {
                case false:
                    WBSBoqService.Release(Boq);
                    break;
                case true:
                    WBSBoqService.Unfix(Boq);
                    break;
            }
            Load();
        }
        /// <summary>
        /// 导入DataTable到清单中
        /// </summary>
        /// <param name="ImportTable"></param>
        public void ImportBoq(DataTable ImportTable)
        {
            bool validityFormat = true;
            string ErrorText = "";
            List<WBSline_boi> lstNew = new List<WBSline_boi>();
            List<string> NotContainBoiName = new List<string>();
            List<string> NotContainWbsLineName = new List<string>();
            List<QtyErrorRelation> LstQtyErrRelation = new List<QtyErrorRelation>();

            if (!ImportTable.Columns.Contains("系统编号1"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("系统编号2"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("数量"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("清单项目"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("部位"))
            {
                validityFormat = false;
            }

            if (!validityFormat)
            {
                throw new BusinessException("文件格式不正确,格式：部位|清单项目|系统编号1|系统编号2|数量");
            }

            if (ImportTable.Rows.Count == 0)
            {
                throw new BusinessException("无导入数据。");
            }

            for (int i = 0; i < ImportTable.Rows.Count; i++)
            {
                DataRow dr = ImportTable.Rows[i];

                decimal iQty = 0;
                WBSline_boi relationNew = new WBSline_boi();

                string strPart = String.Format("{0}", dr["部位"]).Trim();
                string strItemName = String.Format("{0}", dr["清单项目"]).Trim();
                string wbsLineNo = String.Format("{0}", dr["系统编号1"]).Trim();
                string itemNo = String.Format("{0}", dr["系统编号2"]).Trim();
                ContractBoi remainBoi = remainContractBoiList.Find(m => m.ItemNo == itemNo);

                if (remainBoi == null) //验证是否存在同名合同清单项
                {
                    if (!NotContainBoiName.Contains(strItemName))
                    {
                        NotContainBoiName.Add(strItemName);
                    }
                    continue;
                }
                else
                {
                    relationNew.ItemNo = remainBoi.ItemNo;
                }
                                                                                 
                List<WBSLineNode> lstAllNode = NodeBindingSource.ToList();
                WBSLineNode wbsNode = lstAllNode.Find(m => m.WbsLineNo == wbsLineNo);
                if (wbsNode == null) //验证是否存在同名WBS清单项
                {
                    if(!NotContainWbsLineName.Contains(strPart))
                    {
                        NotContainWbsLineName.Add(strPart);
                    }
                    continue;
                }
                else
                {
                    relationNew.WbsSysCode = wbsNode.WbsSysCode;
                }

                decimal qtySum = 0;
                DataRow[] sameItemNameRows = ImportTable.Select(String.Format("系统编号2 = '{0}'", itemNo));
                if (sameItemNameRows.Length > 0)
                {
                    sameItemNameRows.ForEach(m =>
                    {
                        qtySum += ObjectHelper.GetDefaultDecimal(m["数量"]);
                    });
                    
                }
                if (qtySum < 0)
                {
                    QtyErrorRelation errRelation = new QtyErrorRelation();
                    errRelation.ItemName = strItemName;
                    errRelation.WbsLineName = strPart;
                    errRelation.ErrStat = QtyErrStat.Negative;
                    LstQtyErrRelation.Add(errRelation);
                    continue;
                }
                else if (qtySum > (remainBoi.CtrctQty ?? 0))
                {
                    QtyErrorRelation errRelation = new QtyErrorRelation();
                    errRelation.ItemName = strItemName;
                    errRelation.WbsLineName = strPart;
                    errRelation.ImportQty = ObjectHelper.GetDefaultDecimal(dr["数量"]);
                    errRelation.OriginQty = (remainBoi.CtrctQty ?? 0);
                    errRelation.ErrStat = QtyErrStat.OutOfRemain;
                    LstQtyErrRelation.Add(errRelation);
                    continue;
                }

                if (dr["数量"] != null && Decimal.TryParse(String.Format("{0}", dr["数量"]), out iQty))
                {
                    relationNew.Qty = iQty;
                }

                lstNew.Add(relationNew);
            }

            //如果导入出错，抛出错误提示
            #region 抛出错误
           /* if (NotContainBoiName.Count > 0 || NotContainWbsLineName.Count > 0 || LstQtyErrRelation.Count > 0)
            {
                ErrorText = "数据出错\n";
                int ErrorCount = 1;
                if (NotContainWbsLineName.Count > 0)
                {
                    ErrorText += String.Format("{0}、以下WBS项不存在：\n", ErrorCount);
                    ErrorCount++;
                    NotContainWbsLineName.ForEach(m =>
                    {
                        ErrorText += String.Format("\t{0}\n", m);
                    });
                }

                if (NotContainBoiName.Count > 0)
                {
                    ErrorText += String.Format("{0}、以下合同清单项不存在：\n", ErrorCount);
                    ErrorCount++;
                    NotContainBoiName.ForEach(m =>
                    {
                        ErrorText += String.Format("\t{0}\n", m);
                    });
                }

                if(LstQtyErrRelation.Count > 0)
                {
                    if(LstQtyErrRelation.Find(m => m.ErrStat == QtyErrStat.Negative) != null)  //存在输入为负数的项
                    {
                        List<QtyErrorRelation> lstErrOutOfRemain = LstQtyErrRelation.FindAll(m => m.ErrStat == QtyErrStat.OutOfRemain);
                        ErrorText += String.Format("{0}、以下合同清单关联关系导入数量为负数：\n", ErrorCount);
                        ErrorCount++;
                        lstErrOutOfRemain.ForEach(m =>
                        {
                            ErrorText += String.Format("\t部位：{0}，合同清单名称：{1}，导入数量：{2}，剩余未关联数量{3}\n", m.WbsLineName, m.ItemName, m.ImportQty, m.OriginQty);
                        });
                    }

                    if (LstQtyErrRelation.Find(m => m.ErrStat == QtyErrStat.OutOfRemain) != null)  //存在输入超出范围的项
                    {
                        List<QtyErrorRelation> lstErrOutOfRemain = LstQtyErrRelation.FindAll(m => m.ErrStat == QtyErrStat.OutOfRemain);
                        ErrorText += String.Format("{0}、以下合同清单关联关系导入数量总和超出剩余未关联数量：\n", ErrorCount);
                        ErrorCount++;
                        lstErrOutOfRemain.ForEach(m =>
                        {
                            ErrorText += String.Format("\t部位：{0}，合同清单名称：{1}，导入数量：{2}，剩余未关联数量{3}\n", m.WbsLineName, m.ItemName, m.ImportQty, m.OriginQty);
                        });
                    }
                }

                throw new BusinessException(ErrorText);
            } */
            #endregion

            lstNew.ForEach(m =>
            {
                InsertNodeRelation(m.WbsSysCode, m.ItemNo, ObjectHelper.GetDefaultDecimal(m.Qty));
            });

        }
        public void ImportUpdateBoq(DataTable ImportTable)
        {
            bool validityFormat = true;
            string ErrorText = "";
            List<WBSline_boi> lstNew = new List<WBSline_boi>();
            List<string> NotContainBoiName = new List<string>();
            List<string> NotContainWbsLineName = new List<string>();
            List<QtyErrorRelation> LstQtyErrRelation = new List<QtyErrorRelation>();

            if (!ImportTable.Columns.Contains("系统编号1"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("系统编号2"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("数量"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("清单项目"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("部位"))
            {
                validityFormat = false;
            }

            if (!validityFormat)
            {
                throw new BusinessException("文件格式不正确,格式：部位|清单项目|系统编号1|系统编号2|数量");
            }

            if (ImportTable.Rows.Count == 0)
            {
                throw new BusinessException("无导入数据。");
            }
            List<WBSLineNode> nodeList = new List<WBSLineNode>(NodeBindingSource);
            for (int i = 0; i < ImportTable.Rows.Count; i++)
            {
                DataRow dr = ImportTable.Rows[i];

                decimal iQty = 0;
                WBSline_boi relationNew = new WBSline_boi();

                string strPart = String.Format("{0}", dr["部位"]).Trim();
                string strItemName = String.Format("{0}", dr["清单项目"]).Trim();
                string wbsLineNo = String.Format("{0}", dr["系统编号1"]).Trim();
                string itemNo = String.Format("{0}", dr["系统编号2"]).Trim();
                if (dr["数量"] == null || dr["数量"] == DBNull.Value || !Decimal.TryParse(dr["数量"].ToString(), out iQty)) continue;
                WBSLineNode node = nodeList.FirstOrDefault(m => m.WbsLineNo == wbsLineNo);
                if (node != null)
                {
                    WBSline_boi boi = node.RelationList.FirstOrDefault(m => m.ItemNo == itemNo);
                    if (boi != null)
                    {
                        if(boi.Qty.HasValue && boi.Qty.Value == iQty)
                        {
                            continue;
                        }
                        if(iQty < 0)  //导入的数据为负值
                        {
                            continue;
                        }
                        //判断导入的值是否超过剩余值
                        ContractBoi remainBoi = remainContractBoiList.Find(m => m.ItemNo == boi.ItemNo);
                        decimal remainQty = (remainBoi.CtrctQty ?? 0) + (boi.Qty ?? 0);
                        if(iQty > remainQty)
                        {
                            continue;
                        }


                        boi.Qty = iQty;
                        #region 计算金额-节点金额-以及加入已更改列表
                        //计算金额
                        boi.Amount = (boi.Qty ?? 0) * (boi.CtrctPrjPrice ?? 0);

                        //计算未关联项数量和金额
                        //ContractBoi remainBoi = remainContractBoiList.Find(m => m.ItemNo == boi.ItemNo);
                        ContractBoi originBoi = originContractBoiList.Find(m => m.ItemNo == boi.ItemNo);
                        if (remainBoi != null && originBoi != null)
                        {
                            decimal qty = 0, amount = 0;
                            foreach (WBSline_boi relation in boq.AllRelationList)
                            {
                                if (relation.StatId == 1 && relation.ItemNo == remainBoi.ItemNo)
                                {
                                    qty += ObjectHelper.GetDefaultDecimal(relation.Qty);
                                    amount += ObjectHelper.GetDefaultDecimal(relation.Amount);
                                }
                            }
                            remainBoi.CtrctQty = (originBoi.CtrctQty ?? 0) - qty;
                            remainBoi.CtrctAmount = originBoi.CtrctAmount - amount;
                        }

                        //计算WBS节点金额
                        List<WBSLineNode> allNode = NodeBindingSource.ToList();
                        WBSLineNode boiNode = allNode != null ? allNode.Find(m => m.WbsSysCode == boi.WbsSysCode) : null;
                        if (boiNode != null)
                        {
                            CalcNode(boiNode);
                        }
                        //加入修改列表
                        if (!updateRelationList.Contains(boi))
                        {
                            updateRelationList.Add(boi);
                        }
                        #endregion
                        //OnRelationNodeList_ListChanged(null, new ListChangedEventArgs(ListChangedType.ItemChanged, gcRelationDetail.))
                    }
                }
            }
        }

        /// <summary>
        /// 添加当前选中的节点关系
        /// </summary>
        /// <param name="ItemNo"></param>
        /// <param name="Qty"></param>
        public void InsertCurrentSelectNodeRelation (string ItemNo, decimal Qty)
        {
            if (CurrentSelectedNode == null)  //没有选中WBS清单
            {
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = "请选中一个WBS清单项"
                });
            }
            ContractBoi originBoi = originContractBoiList.Find(m => m.ItemNo == ItemNo);  //找原始合同清单项
            ContractBoi remainBoi = remainContractBoiList.Find(m => m.ItemNo == ItemNo);  //找合同请单剩余项
            if (remainBoi == null) //找不到相应合同清单
            {
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = "找不到合同清单项"
                });
            }
            else
            {
                if (Qty < 0) //数量不可为负数
                {
                    throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                    {
                        ErrorMessage = "数量不可为负数"
                    });
                }
                if (remainBoi.CtrctQty < Qty) //剩余数量不足
                {
                    throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                    {
                        ErrorMessage = "剩余清单数量不足"
                    });
                }

                if (CurrentSelectedNode.RelationList.Find(m => m.ItemNo == ItemNo) == null)  //如果当前节点不包含该项清单的关联关系，则新建关系
                {
                    WBSline_boi newRelation = new WBSline_boi();
                    newRelation.Id = -1; //新增的关系Id设置为-1
                    newRelation.ProjectNo = ProjectNo;
                    newRelation.BidNo = CurrentSelectedNode.BidNo;
                    newRelation.BoQNo = originBoi.BoQNo;
                    newRelation.IItemCoe = originBoi.IItemCoe;
                    newRelation.ItemNo = originBoi.ItemNo;
                    newRelation.ItemName = originBoi.ItemName;
                    newRelation.WBSLineNo = CurrentSelectedNode.WbsLineNo;
                    newRelation.WbsSysCode = CurrentSelectedNode.WbsSysCode;
                    newRelation.Uom = originBoi.Uom;
                    newRelation.CtrctQty = originBoi.CtrctQty;
                    newRelation.CtrctPrjPrice = originBoi.CtrctPrjPrice;
                    newRelation.CtrctAmount = originBoi.CtrctAmount;
                    newRelation.Description = originBoi.Description;
                    newRelation.CategoryNo = originBoi.CategoryNo;
                    newRelation.isImportant = originBoi.isImportant;
                    newRelation.IsTax = originBoi.isTax;

                    CurrentSelectedNode.RelationList.Add(newRelation);
                    boq.AllRelationList.Add(newRelation);

                    //updateRelationList.Add(newRelation);
                    RelationBindingSource.Add(newRelation);

                    newRelation.Qty = Qty;
                    //newRelation.Amount = Qty * ObjectHelper.GetDefaultDecimal(newRelation.CtrctPrjPrice);

                    ////剩下的减去本次关联的量
                    //remainBoi.CtrctQty = ObjectHelper.GetDefaultDecimal(remainBoi.CtrctQty) - Qty;
                    //remainBoi.CtrctAmount = ObjectHelper.GetDefaultDecimal(remainBoi.CtrctAmount) - newRelation.Amount;


                }
                else  //如果已经包含该清单关联关系，则更新
                {
                    WBSline_boi relation = CurrentSelectedNode.RelationList.Find(m => m.ItemNo == ItemNo);
                    relation.Qty = ObjectHelper.GetDefaultDecimal(relation.Qty) + Qty;
                    //relation.Amount = ObjectHelper.GetDefaultDecimal(relation.Qty) * ObjectHelper.GetDefaultDecimal(relation.CtrctPrjPrice);

                    ////剩下的减去本次关联的量
                    //remainBoi.CtrctQty = ObjectHelper.GetDefaultDecimal(remainBoi.CtrctQty) - Qty;
                    //remainBoi.CtrctAmount = ObjectHelper.GetDefaultDecimal(remainBoi.CtrctAmount) - Qty * (ObjectHelper.GetDefaultDecimal(remainBoi.CtrctPrjPrice));
                    //if (!updateRelationList.Contains(relation))
                    //{
                    //    updateRelationList.Add(relation);
                    //}
                }
            }
        }

        /// <summary>
        /// 添加关系
        /// </summary>
        /// <param name="ItemNo"></param>
        /// <param name="Qty"></param>
        public void InsertNodeRelation(string WbsSysCode, string ItemNo, decimal Qty)
        {
            List<WBSLineNode> allNode = NodeBindingSource.ToList();
            WBSLineNode node = allNode.Find(m => m.WbsSysCode == WbsSysCode);
            if (node == null)  //没有选中WBS清单
            {
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = "未找到WBS清单项"
                });
            }
            ContractBoi originBoi = originContractBoiList.Find(m => m.ItemNo == ItemNo);  //找原始合同清单项
            ContractBoi remainBoi = remainContractBoiList.Find(m => m.ItemNo == ItemNo);  //找合同请单剩余项
            if (remainBoi == null) //找不到相应合同清单
            {
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = "找不到合同清单项"
                });
            }
            else
            {
                //if (Qty < 0) //数量不可为负数
                //{
                //    throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                //    {
                //        ErrorMessage = "数量不可为负数"
                //    });
                //}
                //if (remainBoi.CtrctQty < Qty) //剩余数量不足
                //{
                //    throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                //    {
                //        ErrorMessage = "剩余清单数量不足"
                //    });
                //}

                if (node.RelationList.Find(m => m.ItemNo == ItemNo) == null)  //如果当前节点不包含该项清单的关联关系，则新建关系
                {
                    WBSline_boi newRelation = new WBSline_boi();
                    newRelation.Id = -1; //新增的关系Id设置为-1
                    newRelation.ProjectNo = ProjectNo;
                    newRelation.BidNo = node.BidNo;
                    newRelation.BoQNo = originBoi.BoQNo;
                    newRelation.ItemNo = originBoi.ItemNo;
                    newRelation.ItemName = originBoi.ItemName;
                    newRelation.WBSLineNo = node.WbsLineNo;
                    newRelation.WbsSysCode = node.WbsSysCode;
                    newRelation.Uom = originBoi.Uom;
                    newRelation.CtrctQty = originBoi.CtrctQty;
                    newRelation.CtrctPrjPrice = originBoi.CtrctPrjPrice;
                    newRelation.CtrctAmount = originBoi.CtrctAmount;
                    newRelation.Description = originBoi.Description;
                    newRelation.CategoryNo = originBoi.CategoryNo;
                    newRelation.isImportant = originBoi.isImportant;
                    newRelation.IsTax = originBoi.isTax;

                    node.RelationList.Add(newRelation);
                    boq.AllRelationList.Add(newRelation);

                    //updateRelationList.Add(newRelation);
                    RelationBindingSource.Add(newRelation);

                    newRelation.Qty = Qty;
                    //newRelation.Amount = Qty * ObjectHelper.GetDefaultDecimal(newRelation.CtrctPrjPrice);

                    ////剩下的减去本次关联的量
                    //remainBoi.CtrctQty = ObjectHelper.GetDefaultDecimal(remainBoi.CtrctQty) - Qty;
                    //remainBoi.CtrctAmount = ObjectHelper.GetDefaultDecimal(remainBoi.CtrctAmount) - newRelation.Amount;


                }
                else  //如果已经包含该清单关联关系，则更新
                {
                    WBSline_boi relation = node.RelationList.Find(m => m.ItemNo == ItemNo);
                    relation.Qty = ObjectHelper.GetDefaultDecimal(relation.Qty) + Qty;
                    //relation.Amount = ObjectHelper.GetDefaultDecimal(relation.Qty) * ObjectHelper.GetDefaultDecimal(relation.CtrctPrjPrice);

                    ////剩下的减去本次关联的量
                    //remainBoi.CtrctQty = ObjectHelper.GetDefaultDecimal(remainBoi.CtrctQty) - Qty;
                    //remainBoi.CtrctAmount = ObjectHelper.GetDefaultDecimal(remainBoi.CtrctAmount) - Qty * (ObjectHelper.GetDefaultDecimal(remainBoi.CtrctPrjPrice));
                    //if (!updateRelationList.Contains(relation))
                    //{
                    //    updateRelationList.Add(relation);
                    //}
                }
            }
        }

        public void DeleteRelation(string ItemNo)
        {
            if (CurrentSelectedNode == null)  //没有选中WBS清单
            {
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = "请选中一个WBS清单项"
                });
            }
            WBSline_boi deleteRelation = boq.AllRelationList.Find(m => m.ItemNo == ItemNo && m.WbsSysCode == CurrentSelectedNode.WbsSysCode && m.StatId == 1);
            ContractBoi remainBoi = remainContractBoiList.Find(m => m.ItemNo == ItemNo);  //找合同请单剩余项
            if (remainBoi == null) //找不到相应合同清单
            {
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = "找不到合同清单项,可能是该项合同清单已被删除。"
                });
            }
            else
            {
                if (deleteRelation != null)
                {
                    deleteRelation.StatId = 0;

                    remainBoi.CtrctQty = ObjectHelper.GetDefaultDecimal(remainBoi.CtrctQty) + ObjectHelper.GetDefaultDecimal(deleteRelation.Qty);
                    remainBoi.CtrctAmount = ObjectHelper.GetDefaultDecimal(remainBoi.CtrctAmount) + ObjectHelper.GetDefaultDecimal(deleteRelation.Amount);

                    CurrentSelectedNode.RelationList.Remove(deleteRelation);
                    RelationBindingSource.Remove(CurrentSelectedNode.RelationList.Find(m => m.ItemNo == ItemNo));
                }
                else
                {
                    throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                    {
                        ErrorMessage = "找不到该项的关联关系。"
                    });
                }
            }

        }
        #endregion
        #region 内部方法
        private void ChangeCode(WBSLineNode node, String newCode)
        {
            String strOldCode = node.WbsSysCode;
            node.WbsSysCode = newCode;
            if (node.Children.Count > 0)
            {
                node.Children.ForEach(m =>
                {
                    m.ParentCode = newCode;
                    ChangeCode(m, newCode + m.WbsSysCode.Substring(m.WbsSysCode.Length - 3, 3));
                });
            }
        }
        /// <summary>
        /// 将Boi列表转换为Node列表
        /// </summary>
        /// <param name="BoiList"></param>
        /// <returns></returns>
        private List<WBSLineNode> Convert(List<WBSline> BoiList, List<WBSline_boi> AllRelationList)
        {
            List<WBSLineNode> lstBoiNode;
            List<WBSLineNode> lstRoot;
            //转换节点
            lstBoiNode = BoiList.ConvertAll<WBSLineNode>(m =>
            {
                WBSLineNode node = HDAutoMapper.DynamicMap<WBSLineNode>(m);
                node.OriginalBoi = m;
                return node;
            });

            lstRoot = lstBoiNode.FindAll(m => String.IsNullOrEmpty(m.ParentCode));
            RootList.AddRange(lstRoot);
            //创建父子关系
            lstRoot.ForEach(m =>
            {
                RecursionNode(lstBoiNode, m);
            });

            //节点挂载关系列表
            lstBoiNode.ForEach(m =>
            {
                m.RelationList = AllRelationList.FindAll(n => n.WbsNo == m.WbsNo && n.WBSLineNo == m.WbsLineNo);

                if (m.RelationList != null && m.RelationList.Count > 0)
                {
                    m.OriginalRelationList = new List<WBSline_boi>();
                    m.RelationList.ForEach(n =>
                        {
                            WBSline_boi node = ObjectHelper.Clone<WBSline_boi>(n);
                            m.OriginalRelationList.Add(node);
                        });
                }

            });

            return lstBoiNode;
        }

        /// <summary>
        /// 清单项值计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalcNode(WBSLineNode Node)
        {
            if (Node.Children.Count == 0)
            {
                Node.Amount = Node.RelationList.Sum(m => m.StatId == 1 ? m.Amount : 0);
            }
            else
            {
                Node.Amount = Node.RelationList.Sum(m => m.StatId == 1 ? m.Amount : 0)
                              + Node.Children.Sum(m => m.StatId == 1 ? m.Amount : 0);
            }

            if (Node.ParentBoiNode != null)
            {
                Node.ParentBoiNode.Amount = Node.ParentBoiNode.RelationList.Sum(m => m.StatId == 1 ? m.Amount : 0)
                                            + Node.ParentBoiNode.Children.Sum(m => m.StatId == 1 ? m.Amount : 0);
            }
        }
        /// <summary>
        /// 清单父项值计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalcParentNode(WBSLineNode ParentNode)
        {
            if (ParentNode.Children.Count == 0)
            {
                ParentNode.Amount = ParentNode.RelationList.Sum(m => m.StatId == 1 ? m.Amount : 0);
            }
            else
            {
                ParentNode.Amount = ParentNode.RelationList.Sum(m => m.StatId == 1 ? m.Amount : 0)
                                    + ParentNode.Children.Sum(m => m.StatId == 1 ? m.Amount : 0);
            }
        }
        /// <summary>
        /// 递归创建父子关系
        /// </summary>
        /// <param name="NodeList"></param>
        /// <param name="Parent"></param>
        private void RecursionNode(List<WBSLineNode> NodeList, WBSLineNode Parent)
        {
            List<WBSLineNode> lstChildren = NodeList.FindAll(m => m.ParentCode == Parent.WbsSysCode);
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
        /// <summary>
        /// 递归删除子节点
        /// </summary>
        /// <param name="NodeList"></param>
        /// <param name="Node"></param>
        private void RecursionDeleteNode(WBSLineNode Node)
        {
            List<WBSLineNode> lstChildren = Node.Children.ToList();
            if (lstChildren.Count > 0)
            {
                lstChildren.ForEach(m =>
                {
                    RecursionDeleteNode(m);
                    if (NodeBindingSource.Contains(m))
                        NodeBindingSource.Remove(m);
                    if (updateList.Contains(m))
                        updateList.Remove(m);
                });
            }
            if (NodeBindingSource.Contains(Node))
                NodeBindingSource.Remove(Node);
            if (updateList.Contains(Node))
                updateList.Remove(Node);
            if (Node.ParentBoiNode != null && Node.ParentBoiNode.Children.Contains(Node))
            {
                Node.ParentBoiNode.Children.Remove(Node);
            }
        }

        /// <summary>
        /// 递归判断是否存在关联关系
        /// </summary>
        /// <param name="Node"></param>
        /// <returns></returns>
        public List<WBSline_boi> RecursionFindRelationShip(WBSLineNode Node)
        {
            List<WBSline_boi> result = new List<WBSline_boi>();
            if (Node.RelationList != null && Node.RelationList.Count > 0)
            {
                result.AddRange(Node.RelationList);
            }

            List<WBSLineNode> lstChildren = Node.Children.ToList();
            if (lstChildren.Count > 0)
            {
                lstChildren.ForEach(m =>
                    {
                        result.AddRange(RecursionFindRelationShip(m));
                    });
            }

            return result;
        }


        ///// <summary>
        ///// 导入表行转换为关系对象
        ///// </summary>
        ///// <param name="ImportRow"></param>
        ///// <returns></returns>
        //private WBSline_boi RowToNode(DataRow ImportRow)
        //{
        //    decimal iQty = 0;
        //    String strIItemCode;
        //    String strItemCode;
        //    String strItemName;
        //    String strItemUom;
        //    strItemCode = String.Format("{0}", ImportRow["系统编号"]);
        //    strIItemCode = String.Format("{0}", ImportRow["清单编号"]);
        //    strItemName = String.Format("{0}", ImportRow["清单名称"]);
        //    strItemUom = String.Format("{0}", ImportRow["单位"]);
        //    WBSline_boi relationNew = new WBSline_boi();
        //    relationNew.WbsSysCode = strItemCode;
        //    relationNew.ProjectNo = ProjectNo;
        //    relationNew.WbsNo = Boq.WbsNo;
        //    //relationNew.ParentBoiNode = null;
        //    //relationNew.WbsLineName = strItemName;
        //    //relationNew.WbsLineCode = strIItemCode;
        //    if (ImportRow["数量"] != null && Decimal.TryParse(String.Format("{0}", ImportRow["数量"]), out iQty))
        //    {
        //        relationNew.Qty = iQty;
        //    }

        //    return relationNew;
        //}
        #endregion
    }

    /// <summary>
    /// 清单状态枚举
    /// </summary>
    public enum BoqStat
    {
        /// <summary>
        /// 0缺少
        /// </summary>
        Absent = 0,
        /// <summary>
        /// 1未锁定
        /// </summary>
        UnLock = 1,
        /// <summary>
        /// 2锁定
        /// </summary>
        Locked = 2,
        /// <summary>
        /// 3变更中
        /// </summary>
        Changing = 3

    }
    /// <summary>
    /// 清单扩展节点，用于记录父子节点
    /// </summary>
    public class WBSLineNode : WBSline
    {
        /// <summary>
        /// 父级
        /// </summary>
        public WBSLineNode ParentBoiNode { get; set; }
        /// <summary>
        /// 子集集合
        /// </summary>
        public List<WBSLineNode> Children { get; set; }

        public WBSline OriginalBoi { get; set; }

        public List<WBSline_boi> OriginalRelationList { get; set; }

        private List<WBSline_boi> _relateionList;
        /// <summary>
        /// 该节点所关联的合同清单关系列表
        /// </summary>
        public List<WBSline_boi> RelationList
        {
            get
            {
                return _relateionList;
            }
            set
            {
                _relateionList = value;
                if (value != null && value.Count > 0)
                {
                    value.ForEach(m =>
                        {
                            m.WbsSysCode = this.WbsSysCode;
                        });
                }
            }
        }
        public WBSLineNode()
        {
            Children = new List<WBSLineNode>();
            OriginalRelationList = new List<WBSline_boi>();
            RelationList = new List<WBSline_boi>();
        }
    }

    /// <summary>
    /// 用于记录导入时数量错误的关联关系
    /// </summary>
    public class QtyErrorRelation
    {
        /// <summary>
        /// 出错状态
        /// </summary>
        public QtyErrStat ErrStat { get; set; }
        /// <summary>
        /// 合同项名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// WBS项名称
        /// </summary>
        public string WbsLineName { get; set; }
        /// <summary>
        /// 导入数量
        /// </summary>
        public Decimal ImportQty { get; set; }
        /// <summary>
        /// 合同原始数量
        /// </summary>
        public Decimal OriginQty { get; set; }
    }

    /// <summary>
    /// 数量出错状态
    /// </summary>
    public enum QtyErrStat
    {

        /// <summary>
        /// 负数
        /// </summary>
        Negative = 0,
        /// <summary>
        /// 超出剩余可关联值
        /// </summary>
        OutOfRemain = 1
    }

}
