using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Domain.Entity.ContractBoqEntity;
using Hondee.Common.Advance.Util;
using Hondee.Common.Client;
using Hondee.Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HD.MeteringPayment.Module.Forms.ContractBoqMng.ViewModel
{
    /// <summary>
    /// 变更对象ViewModel
    /// </summary>
    public class ChangeDetailViewModel
    {
        /// <summary>
        /// 变更服务
        /// </summary>
        private IContractBoqChange projectBoqChangeService;
        /// <summary>
        /// 清单服务
        /// </summary>
        private IContractBoq projectBoqService; 
        public String projectNo = "";
        public String projectName = "";
        /// <summary>
        /// 清单变更业务对象
        /// </summary> 
        public ProjectBoqChangeEx BoqChangeEx { get;  set; }
        /// <summary>
        /// 变更列表
        /// </summary>
        public BindingList<ContractBoqChangeInfo> ChangedList { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ChangeNo"></param>
        public ChangeDetailViewModel(BindingList<ContractBoqChangeInfo> ChangedList,String ProjectNo, string ProjectName = "")
        { 
            this.projectNo = ProjectNo;
            this.projectName = ProjectName;
            MeteringPaymentClient client = new MeteringPaymentClient();
            projectBoqChangeService = client.GetIContractBoqChangeService();
            projectBoqService = client.GetIContractBoqService();
            this.ChangedList=ChangedList;
            BoqChangeEx = new ProjectBoqChangeEx();
            BoqChangeEx.ProjectNo = projectNo;
        }
        /// <summary>
        /// 变更数据加载
        /// </summary>
        public void Load(String ChangeNo)
        {
            ContractBoqChange changeData = projectBoqChangeService.Get(ChangeNo);
            BoqChangeEx = HDAutoMapper.DynamicMap<ProjectBoqChangeEx>(changeData);
            BoqChangeEx.ChangeInfo = ChangedList.ToList().FirstOrDefault(m => m.ChangeNo == ChangeNo);
            List<ContractBoqChangeDetailEx> lstEx = HDAutoMapper.DynamicMap<List<ContractBoqChangeDetailEx>>(changeData.Details);
            lstEx.ForEach(m =>
            {
                BoqChangeEx.DetailExList.Add(m);
            });

        } 
        /// <summary>
        /// 保存数据
        /// </summary>
        public void Save()
        {
            ContractBoqChange changeData = new ContractBoqChange();
            ObjectHelper.CopyOneToTwo(BoqChangeEx, changeData);
            changeData.Details = new List<ContractBoqChangeDetail>();
            changeData.Details.AddRange(BoqChangeEx.DetailExList.ToList().ConvertAll<ContractBoqChangeDetail>(m =>
            {
                ContractBoqChangeDetail detail = new ContractBoqChangeDetail();
                ObjectHelper.CopyOneToTwo(m, detail);
                return detail;
            }));
            ContractBoqChange result = projectBoqChangeService.Save(changeData);
            if (String.IsNullOrEmpty(changeData.ChangeNo))
            {
                ContractBoqChangeInfo info = HDAutoMapper.DynamicMap<ContractBoqChangeInfo>(result);
                BoqChangeEx.ChangeNo = result.ChangeNo;
                BoqChangeEx.ChangeInfo = info;
                ChangedList.Add(info);
            }
            ObjectHelper.CopyOneToTwo<ContractBoqChangeInfo>(HDAutoMapper.DynamicMap<ContractBoqChangeInfo>(result), BoqChangeEx.ChangeInfo);
        }
        /// <summary>
        /// 发布清单
        /// </summary>
        public void Fixed()
        {
            projectBoqChangeService.Fixed(BoqChangeEx.ChangeNo,true);
            BoqChangeEx.Fixed = true;
            BoqChangeEx.ChangeInfo.Fixed = true;
        }
        /// <summary>
        /// 撤销清单
        /// </summary>
        public void UnFixed()
        {
            projectBoqChangeService.Fixed(BoqChangeEx.ChangeNo, false);
            BoqChangeEx.Fixed = false;
            BoqChangeEx.ChangeInfo.Fixed = false;
        }
    }
    /// <summary>
    /// 变更清单扩展对象
    /// </summary>
    public class ProjectBoqChangeEx : ContractBoqChange
    {
        public ProjectBoqChangeEx()
        {
            PrepareBy = LoginInfo.UserName;

            DetailExList = new BindingList<ContractBoqChangeDetailEx>();
        }
        /// <summary>
        /// 变更info
        /// </summary>
        public ContractBoqChangeInfo ChangeInfo { get; set; }
        /// <summary>
        /// 明细的扩展对象列表
        /// </summary>
        public BindingList<ContractBoqChangeDetailEx> DetailExList { get; set; }

        /// <summary>
        /// 计算变更金额
        /// </summary>
        public void CalcChangeAmount()
        {
            List<ContractBoqChangeDetailEx> lstDetail= DetailExList.ToList();
            List<ContractBoqChangeDetailEx> leafNode = lstDetail.FindAll(m=>  
                !lstDetail.Exists(n=>n.ItemCode.StartsWith(m.ItemCode)&&n!=m));
            ChangeAmount = leafNode.Sum(m => ObjectHelper.GetDefaultDecimal(m.ChangeAmount)); 
        }
    }
    /// <summary>
    /// 变更明细扩展对象
    /// </summary>
    public class ContractBoqChangeDetailEx : ContractBoqChangeDetail
    {
        public ChangeType ChangeType
        {
            get
            {  
                switch (Type)
                {
                    case 1:
                        return ChangeType.Add;
                    case 2:
                        return ChangeType.Update;
                    case 3:
                        return ChangeType.Delete;
                    case 4:
                        return ChangeType.Disable;
                    case 5:
                        return ChangeType.Enable;
                    default: return ChangeType.NoChange;
                }
            }
            set
            {
                Type = (Int32)value;
            }
        }
        public int TypeShow
        {
            get
            {
                int result = 0;//1-新增，2-修改单价，3-修改数量，4-修改单价数量，5-修改其他信息,6-删除
                if (Type == 1||Type==5)
                    result = 1;
                if (Type == 2)
                {
                    if (IsUpPrice && IsUpQty)
                        result = 4;
                    else if (IsUpPrice)
                    {
                        result = 2;
                    }
                    else if (IsUpQty)
                    {
                        result = 3;
                    }
                    else
                        result = 5;
                }
                if (Type == 3 || Type == 4)
                {
                    result = 6;
                }
                return result;

            }
        }
    }
}
