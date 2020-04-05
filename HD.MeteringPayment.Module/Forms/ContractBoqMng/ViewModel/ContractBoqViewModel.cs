using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Domain.Entity.ContractBoqEntity;
using Hondee.Common.Advance.Util;
using Hondee.Common.HDException;
using Hondee.Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Module.Forms.ContractBoqMng.ViewModel
{
    public partial class ContractBoqViewModel
    {
        #region 成员
        private IContractBoq contractBoqService;
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
        public ContractBoq boq;
        /// <summary>
        /// 合同清单数据
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
        public BindingList<ContractBoiNode> NodeList { get; private set; }
        private List<ContractBoiNode> RootList { get; set; }
        private List<ContractBoiNode> updateList { get; set; }
        /// <summary>
        /// 列表改变事件
        /// </summary>
        public Action ListChanged;
        #endregion
        #region 构造方法
        public ContractBoqViewModel(String ProjectNo, String ProjectName)
        {
            NodeList = new BindingList<ContractBoiNode>();
            this.ProjectNo = ProjectNo;
            this.ProjectName = ProjectName;
            contractBoqService = new MeteringPaymentClient().GetIContractBoqService();
            RootList = new List<ContractBoiNode>();
            updateList = new List<ContractBoiNode>();
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
                ContractBoiNode boiNode = NodeList[e.NewIndex];
                CalcNode(boiNode);
                if (!String.IsNullOrEmpty(boiNode.ItemNo) && !updateList.Contains(boiNode))
                    updateList.Add(boiNode);
            }
        }

        #endregion
        #region 公有方法
        /// <summary>
        /// 装载清单数据
        /// </summary>
        public void Load()
        {
            NodeList.ListChanged -= OnNodeList_ListChanged;
            RootList.Clear();
            NodeList.Clear();
            updateList.Clear();
            List<ContractBoiNode> lstNode = new List<ContractBoiNode>();
            Boq = contractBoqService.GetByProjectNo(ProjectNo);
            if (Boq != null)
            {
                lstNode = Convert(Boq.BoiList);
                lstNode.ForEach(m => NodeList.Add(m));
            }
            else
            {
                Boq = new ContractBoq();
                Boq.ProjectNo = ProjectNo;
                Boq.BoQName = ProjectName;
            }
            NodeList.ListChanged += OnNodeList_ListChanged;

            if (ListChanged != null)
            {
                ListChanged();
            }
        }
        /// <summary>
        /// 插入节点
        /// </summary>
        /// <param name="ParentNode"></param>
        public ContractBoiNode InsertNode(ContractBoiNode ParentNode = null)
        {
            String strParentCode;
            String strMaxCode;
            String strNewCode;
            int iNewCode;
            ContractBoiNode nodeNew = new ContractBoiNode();
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
            }
            nodeNew.ItemCode = strNewCode;
            nodeNew.ProjectNo = ProjectNo;
            nodeNew.BoQNo = Boq.BoQNo;
            nodeNew.ParentBoiNode = ParentNode;
            CalcNode(nodeNew);
            NodeList.Add(nodeNew);

            if (ListChanged != null)
                ListChanged();
            return nodeNew;
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="Node"></param>
        public void DeleteNode(ContractBoiNode Node)
        {
            //已删除过，则返回
            if (!NodeList.Contains(Node)) return;
            // RecursionDeleteNode(Node);
            List<ContractBoiNode> lstChildren = Node.Children.ToList();
            for (int i = lstChildren.Count - 1; i >= 0; i--)
            {
                DeleteNode(lstChildren[i]);
            }
            if (NodeList.Contains(Node))
                NodeList.Remove(Node);
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
                        //对后面的节点的序号 统一减1
                        strCode = Node.ParentBoiNode.Children[i].ItemCode;
                        iCode = System.Convert.ToInt32(strCode.Substring(strCode.Length - 3, 3));
                        iCode = iCode - 1;
                        strCode = Node.ParentBoiNode.ItemCode + iCode.ToString().PadLeft(3, '0');
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
                        strCode = RootList[i].ItemCode;
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
            if (ListChanged != null)
                ListChanged();
        }
        /// <summary>
        /// 禁用和启用项
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="Stat">true-启用项，false-禁用项</param>
        public void ChangeNodeStat(ContractBoiNode Node, Boolean Stat)
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

            if (ListChanged != null)
                ListChanged();
        }
        /// <summary>
        /// 保存更改
        /// </summary>
        public void Save()
        {
            if (NodeList.Count == 0)
            {
                throw new BusinessException("至少有一条明细！");
            }
            if (NodeList.ToList().Exists(m => String.IsNullOrEmpty(m.ItemName) || String.IsNullOrEmpty(m.ItemName.Trim())))
            {
                throw new BusinessException(" 清单项名称不能为空！");
            }
            List<ContractBoi> lstAddBoi = null;
            List<ContractBoi> lstDelBoi = null;
            List<ContractBoi> lstUpdateBoi = null;
            List<String> lstDelNo = null;
            List<ContractBoiNode> lstAllNode = NodeList.ToList();
            //添加的节点集合
            List<ContractBoiNode> lstAddNode = lstAllNode.FindAll(m => m.OriginalBoi == null);
            //删除的节点集合
            lstDelBoi = boq.BoiList.FindAll(m => !lstAllNode.Exists(n => n.OriginalBoi == m));
            if (lstAddNode.Count > 0)
                lstAddBoi = lstAddNode.ConvertAll<ContractBoi>(m => ObjectHelper.Clone<ContractBoi>(m));
            if (lstDelBoi.Count > 0)
                lstDelNo = lstDelBoi.ConvertAll<String>(m => m.ItemNo);
            if (updateList.Count > 0)
            {
                lstUpdateBoi = updateList.ConvertAll<ContractBoi>(m => ObjectHelper.Clone<ContractBoi>(m));
            }
            //执行保存操作
            List<ContractBoiNoInfo> lstAddedInfo = contractBoqService.Save(ProjectNo, null, Boq.BoQName, ObjectHelper.GetDefaultDecimal(Boq.TotalAmount), lstAddBoi, lstUpdateBoi, lstDelNo);
            lstAddNode.ForEach(m =>
            {
                //为新增的项填写ItemNo
                m.ItemNo = lstAddedInfo.Find(n => n.ItemCode == m.ItemCode).ItemNo;
                //拷贝Boi对象
                ContractBoi boi = ObjectHelper.Clone<ContractBoi>(m);
                //将Boi对象与节点关联起来
                m.OriginalBoi = boi;
                Boq.BoQNo = m.BoQNo;
                //加入到原始Boq集合中
                Boq.BoiList.Add(boi);
            });
            //从原始集合中移除已删除的对象
            lstDelBoi.ForEach(m =>
            {
                Boq.BoiList.Remove(m);
            });
            //清除变化记录集合
            updateList.Clear();
            //将编辑状态设置为FALSE
            this.Editing = false;
            //重新读取数据
            Load();
        }
        public void Lock()
        {
            //锁定
            contractBoqService.ChangeStat(Boq.BoQNo, 2);
            Boq.ExecuteStat = 2;
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
        /// 导入DataTable到清单中
        /// </summary>
        /// <param name="ImportTable"></param>
        public void ImportBoq(DataTable ImportTable)
        {
            bool validityFormat = true;
            String strParentCode = null;
            if (!ImportTable.Columns.Contains("系统编号"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("清单名称"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("清单编号"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("数量"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("单价"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("单位"))
            {
                validityFormat = false;
            }
            if (!validityFormat) throw new BusinessException("文件格式不正确,格式：系统编号|清单名称|清单编号|数量|单价|单位");
            List<ContractBoiNode> lstNewNode = new List<ContractBoiNode>();
            List<ContractBoiNode> lstTemp = new List<ContractBoiNode>();
            List<ContractBoiNode> lstExist = NodeList.ToList();
            ContractBoiNode nodeParent = null;
            for (int i = 0; i < ImportTable.Rows.Count; i++)
            {
                DataRow dr = ImportTable.Rows[i];
                lstNewNode.Add(RowToNode(dr));
            }
            //检查编号是否连续
            for (int i = 0; i < lstNewNode.Count; i++)
            {
                if (lstNewNode[i].ItemCode.Length % 3 != 2)
                {
                    throw new BusinessException(String.Format("编号长度不正确,根项长度为2，子项编号=父项编号+3位数字，错误编号：{0}", lstNewNode[i].ItemCode));
                }
                else if (lstNewNode[i].ItemCode.Length == 2)
                {
                    strParentCode = null;
                    lstNewNode[i].ParentCode = strParentCode;
                }
                else if (lstNewNode[i].ItemCode.Length > 2)
                {
                    strParentCode = lstNewNode[i].ItemCode.Substring(0, lstNewNode[i].ItemCode.Length - 3);
                    lstNewNode[i].ParentCode = strParentCode;
                }
                //查找父节点
                nodeParent = lstExist.FirstOrDefault(m => m.ItemCode == lstNewNode[i].ParentCode);
                if (nodeParent == null)
                    nodeParent = lstTemp.FirstOrDefault(m => m.ItemCode == lstNewNode[i].ParentCode);

                //有父节点的情况
                if (strParentCode != null && ((lstExist.Exists(m => m.ItemCode == lstNewNode[i].ParentCode)
                    && !lstExist.Exists(m => m.ItemCode == lstNewNode[i].ItemCode)
                    && !lstTemp.Exists(m => m.ItemCode == lstNewNode[i].ItemCode))
                    || (lstTemp.Exists(m => m.ItemCode == lstNewNode[i].ParentCode)
                    && !lstTemp.Exists(m => m.ItemCode == lstNewNode[i].ItemCode))))
                {
                    lstNewNode[i].ParentBoiNode = nodeParent;
                    nodeParent.Children.Add(lstNewNode[i]);
                }
                else if (strParentCode == null && !lstExist.Exists(m => m.ItemCode == lstNewNode[i].ItemCode)
                   && !lstTemp.Exists(m => m.ItemCode == lstNewNode[i].ItemCode))
                {
                    //无父节点的情况
                    lstNewNode[i].ParentBoiNode = null;
                    RootList.Add(lstNewNode[i]);
                }
                else
                {
                    //不符合条件
                    throw new BusinessException(String.Format("编号不正确，错误编号：{0}，可能的原因是 1、无法找到父项，2、编号重复", lstNewNode[i].ItemCode));

                }
                lstTemp.Add(lstNewNode[i]);
            }
            lstNewNode.ForEach(m =>
            {
                CalcNode(m);
                NodeList.Add(m);
            });

            if (ListChanged != null)
                ListChanged();
        }


        /// <summary>
        /// 导入DataTable到清单中
        /// </summary>
        /// <param name="ImportTable"></param>
        public void ImportBoqByCode(DataTable ImportTable)
        {
            bool validityFormat = true;
            String strParentCode = null;
            //if (!ImportTable.Columns.Contains("系统编号"))
            //{
            //    validityFormat = false;
            //}
            if (!ImportTable.Columns.Contains("清单名称"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("清单编号"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("数量"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("单价"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("单位"))
            {
                validityFormat = false;
            }
            if (!validityFormat) throw new BusinessException("文件格式不正确,格式：清单名称|清单编号|数量|单价|单位");
            List<ContractBoiNode> lstNewNode = new List<ContractBoiNode>();
            List<ContractBoiNode> lstTemp = new List<ContractBoiNode>();
            List<ContractBoiNode> lstExist = NodeList.ToList();
            ContractBoiNode nodeParent = null;
            for (int i = 0; i < ImportTable.Rows.Count; i++)
            {
                DataRow dr = ImportTable.Rows[i];
                lstNewNode.Add(RowToNode(dr));
            }
            #region 生成系统编号
            string lastCode = lstExist.Count > 0 ? lstExist[lstExist.Count - 1].ItemCode : "";
            NodeInfo nodeInfo = new NodeInfo(lastCode);
            foreach (ContractBoiNode node in lstNewNode)
            {
                int lv0 = -1;
                string[] pathNodes = node.IItemCoe.Split('-');
                if (pathNodes.Length == 1 && int.TryParse(pathNodes[0], out lv0) && lv0 % 100 == 0) //根节点
                {
                    if (nodeInfo.levelRecord == null)
                    {
                        nodeInfo.levelRecord = new List<string>();
                        nodeInfo.levelRecord.Add("01");
                        nodeInfo.lastCode = "01";
                    }
                    else
                    {
                        nodeInfo.ClearLevelBehind(0);  //清除第一级以后的记录，并将当前根节点向后移动一位
                        int root = 1;
                        if (int.TryParse(nodeInfo.levelRecord[0], out root))
                        {
                            root += 1;
                            nodeInfo.levelRecord[0] = root.ToString("d2");
                            nodeInfo.lastCode = nodeInfo.levelRecord[0];
                        }
                    }
                }
                else
                {
                    int nodeLv = pathNodes.Length;
                    if(nodeInfo.level < nodeLv/* || nodeInfo.level == nodeLv && lv0 % 100 != 0*/) //这个节点是上一个节点的子节点
                    {
                        nodeInfo.SetLevelValue(nodeLv, "001");
                    }
                    else if(nodeInfo.level == nodeLv)  //这个节点和上一个节点是兄弟节点
                    {
                        int lastValue = int.Parse(nodeInfo.levelRecord[nodeInfo.level]) + 1;  //获取上一个节点的最后一个等级的记录并加一
                        nodeInfo.SetLevelValue(nodeLv, lastValue.ToString("d3"));
                    }
                    else  //如果层次降低，则清空当前节点等级以后的记录，并将当前等级记录加一
                    {
                        nodeInfo.ClearLevelBehind(nodeLv);
                        int lastValue = int.Parse(nodeInfo.levelRecord[nodeLv]) + 1;
                        nodeInfo.SetLevelValue(nodeLv, lastValue.ToString("d3"));
                    }
                }
                nodeInfo.lastCode = nodeInfo.GenerateCode();
                node.ItemCode = nodeInfo.lastCode;
            }
            #endregion
            #region 检查编号是否连续
            //检查编号是否连续
            lstTemp.Clear();
            for (int i = 0; i < lstNewNode.Count; i++)
            {
                if (lstNewNode[i].ItemCode.Length % 3 != 2)
                {
                    throw new BusinessException(String.Format("编号长度不正确,根项长度为2，子项编号=父项编号+3位数字，错误编号：{0}", lstNewNode[i].ItemCode));
                }
                else if (lstNewNode[i].ItemCode.Length == 2)
                {
                    strParentCode = null;
                    lstNewNode[i].ParentCode = strParentCode;
                }
                else if (lstNewNode[i].ItemCode.Length > 2)
                {
                    strParentCode = lstNewNode[i].ItemCode.Substring(0, lstNewNode[i].ItemCode.Length - 3);
                    lstNewNode[i].ParentCode = strParentCode;
                }
                //查找父节点
                nodeParent = lstExist.FirstOrDefault(m => m.ItemCode == lstNewNode[i].ParentCode);
                if (nodeParent == null)
                    nodeParent = lstTemp.FirstOrDefault(m => m.ItemCode == lstNewNode[i].ParentCode);

                //有父节点的情况
                if (strParentCode != null && ((lstExist.Exists(m => m.ItemCode == lstNewNode[i].ParentCode)
                    && !lstExist.Exists(m => m.ItemCode == lstNewNode[i].ItemCode)
                    && !lstTemp.Exists(m => m.ItemCode == lstNewNode[i].ItemCode))
                    || (lstTemp.Exists(m => m.ItemCode == lstNewNode[i].ParentCode)
                    && !lstTemp.Exists(m => m.ItemCode == lstNewNode[i].ItemCode))))
                {
                    lstNewNode[i].ParentBoiNode = nodeParent;
                    nodeParent.Children.Add(lstNewNode[i]);
                }
                else if (strParentCode == null && !lstExist.Exists(m => m.ItemCode == lstNewNode[i].ItemCode)
                   && !lstTemp.Exists(m => m.ItemCode == lstNewNode[i].ItemCode))
                {
                    //无父节点的情况
                    lstNewNode[i].ParentBoiNode = null;
                    RootList.Add(lstNewNode[i]);
                }
                else
                {
                    //不符合条件
                    throw new BusinessException(String.Format("编号不正确，错误编号：{0}，可能的原因是 1、无法找到父项，2、编号重复", lstNewNode[i].ItemCode));

                }
                lstTemp.Add(lstNewNode[i]);
            }
            #endregion
            lstNewNode.ForEach(m =>
            {
                CalcNode(m);
                NodeList.Add(m);
            });

            if (ListChanged != null)
                ListChanged();
        }
        #endregion
        #region 内部方法
        private void ChangeCode(ContractBoiNode node, String newCode)
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
        private List<ContractBoiNode> Convert(List<ContractBoi> BoiList)
        {
            List<ContractBoiNode> lstBoiNode;
            List<ContractBoiNode> lstRoot;
            lstBoiNode = BoiList.ConvertAll<ContractBoiNode>(m =>
            {
                ContractBoiNode node = HDAutoMapper.DynamicMap<ContractBoiNode>(m);
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
        private void CalcNode(ContractBoiNode Node)
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
        private void CalcParentNode(ContractBoiNode ParentNode)
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
        private void RecursionNode(List<ContractBoiNode> NodeList, ContractBoiNode Parent)
        {
            List<ContractBoiNode> lstChildren = NodeList.FindAll(m => m.ParentCode == Parent.ItemCode);
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
        private void RecursionDeleteNode(ContractBoiNode Node)
        {
            List<ContractBoiNode> lstChildren = Node.Children.ToList();
            if (lstChildren.Count > 0)
            {
                lstChildren.ForEach(m =>
                {
                    RecursionDeleteNode(m);
                    if (NodeList.Contains(m))
                        NodeList.Remove(m);
                    if (updateList.Contains(m))
                        updateList.Remove(m);
                });
            }
            if (NodeList.Contains(Node))
                NodeList.Remove(Node);
            if (updateList.Contains(Node))
                updateList.Remove(Node);
            if (Node.ParentBoiNode != null && Node.ParentBoiNode.Children.Contains(Node))
            {
                Node.ParentBoiNode.Children.Remove(Node);
            }
        }

        private ContractBoiNode RowToNode(DataRow ImportRow)
        {
            List<ContractBoiNode> lstNewNode = new List<ContractBoiNode>();
            decimal iItemQty = 0;
            decimal iItemPrice = 0;
            String strIItemCode;
            String strItemCode = "";
            String strItemName;
            String strItemUom;
            if (ImportRow.Table.Columns.Contains("系统编号"))
            {
                strItemCode = String.Format("{0}", ImportRow["系统编号"]);
            }
            strIItemCode = String.Format("{0}", ImportRow["清单编号"]);
            strItemName = String.Format("{0}", ImportRow["清单名称"]);
            strItemUom = String.Format("{0}", ImportRow["单位"]);
            ContractBoiNode nodeNew = new ContractBoiNode();
            nodeNew.ItemCode = strItemCode;
            nodeNew.ProjectNo = ProjectNo;
            nodeNew.BoQNo = Boq.BoQNo;
            nodeNew.ParentBoiNode = null;
            nodeNew.ItemName = strItemName;
            nodeNew.Uom = strItemUom;
            nodeNew.IItemCoe = strIItemCode;
            if (ImportRow["数量"] != null && Decimal.TryParse(String.Format("{0}", ImportRow["数量"]), out iItemQty))
            {
                nodeNew.CtrctQty = iItemQty;
            }
            if (ImportRow["单价"] != null && Decimal.TryParse(String.Format("{0}", ImportRow["单价"]), out iItemPrice))
            {
                nodeNew.CtrctPrjPrice = iItemPrice;
            }
            return nodeNew;
        }

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
    public class ContractBoiNode : ContractBoi
    {
        /// <summary>
        /// 父级
        /// </summary>
        public ContractBoiNode ParentBoiNode { get; set; }
        /// <summary>
        /// 子集集合
        /// </summary>
        public List<ContractBoiNode> Children { get; set; }
        public ContractBoi OriginalBoi { get; set; }
        public ContractBoiNode()
        {
            Children = new List<ContractBoiNode>();
        }
    }

    public class NodeInfo
    {
        public NodeInfo(string lastCode)
        {
            this.lastCode = lastCode;
            if (lastCode.Length == 2)
            {
                levelRecord[0] = lastCode;
            } 
            else if(lastCode.Length > 2)
            {
                levelRecord[0] = lastCode.Substring(0, 2);  //第1级取两个字符，后面每个等级取三个字符
                int level = 1;
                for(int i = 2; i < lastCode.Length; i = i + 3)
                {
                    levelRecord[level] = lastCode.Substring(i, 3);
                    level++;
                }
            } 
               
        }

        public List<string> levelRecord = null;
        public string lastCode = null;

        /// <summary>
        /// level从0开始
        /// </summary>
        public int level
        {
            get
            {
                if(levelRecord == null) return -1;
                return levelRecord.Count - 1;
            }
        }
        
        /// <summary>
        /// 清除该等级以后的记录，例如level = 1，则将level > 1的记录清除
        /// </summary>
        /// <param name="level"></param>
        public void ClearLevelBehind(int level)
        {
            if (level < 0) return;
            if(levelRecord.Count - 1 > level)
            {

                for(int index = levelRecord.Count - 1; index > level; index--)
                {
                    levelRecord.RemoveAt(index);
                }
            }
        }

        public void SetLevelValue(int level, string value)
        {
            if(levelRecord.Count >= level + 1)
            {
                levelRecord[level] = value;
            }
            else 
            {
                for(int i = 0; i < (level + 1) - levelRecord.Count; i++)
                {
                    levelRecord.Add("001");
                }
            }
        }

        /// <summary>
        /// 根据当前层次记录生成code
        /// </summary>
        /// <returns></returns>
        public string GenerateCode()
        {
            string lastCode = "";
            foreach (string str in levelRecord)
            {
                lastCode += str;
            }
            return lastCode;
        }
    }
}
