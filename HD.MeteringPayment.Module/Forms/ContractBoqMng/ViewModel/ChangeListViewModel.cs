using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Domain.Entity.ContractBoqEntity;
using Hondee.Common.Advance.Util;
using Hondee.Common.HDException;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Module.Forms.ContractBoqMng.ViewModel
{
    /// <summary>
    /// 变更ViewModel
    /// </summary>
   public class ChangeListViewModel
    {
        /// <summary>
        /// 变更服务
        /// </summary>
        private IContractBoqChange projectBoqChangeService;
        /// <summary>
        /// 清单服务
        /// </summary>
        private IContractBoq projectBoqService;
        /// <summary>
        /// 清单编号
        /// </summary>
        public String projectNo;
        public String projectName;
        /// <summary>
        /// 项目清单
        /// </summary>
        private ContractBoq boq;
        /// <summary>
        /// 变更列表
        /// </summary>
        public BindingList<ContractBoqChangeInfo> ChangedList { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ProjectNo"></param>
        public ChangeListViewModel(String ProjectNo, string ProjectName)
        {
            this.projectNo = ProjectNo;
            this.projectName = ProjectName;
            MeteringPaymentClient client = new MeteringPaymentClient();
            projectBoqChangeService = client.GetIContractBoqChangeService();
            projectBoqService = client.GetIContractBoqService();
            ChangedList = new BindingList<ContractBoqChangeInfo>();
        }
        /// <summary>
        /// 变更数据加载
        /// </summary>
        public void Load()
        {
            boq = projectBoqService.GetByProjectNo(projectNo);
            ChangedList.Clear();
            List<ContractBoqChangeInfo> lstChangeInfo = projectBoqChangeService.GetListByProjectNo(projectNo);
            lstChangeInfo.ForEach(m=> {
                ChangedList.Add(m);
            });
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Info"></param>
        public void Delete(ContractBoqChangeInfo Info)
        {
            projectBoqChangeService.Delete(Info.ChangeNo);
            ChangedList.Remove(Info);
        }
        public ChangeDetailViewModel Add()
        {
            if (boq == null)
            {
                throw new BusinessException("清单不存在！");
            }
            //锁定
            if (boq.ExecuteStat != 2)
            {
                throw new BusinessException("只能在清单锁定的情况下新增！");

            } 
            return new ChangeDetailViewModel(ChangedList, projectNo, projectName);
        }
        /// <summary>
        /// 获取扩展对象
        /// </summary>
        /// <param name="ChangeNo">变更单No</param>
        /// <returns></returns>
        public ProjectBoqChangeEx GetChangeEx(String ChangeNo)
        {
            //查找Info对象
            ContractBoqChangeInfo chInfo = ChangedList.ToList().Find(m => m.ChangeNo == ChangeNo);
            //获取变更明细
            ContractBoqChange boqChange=  projectBoqChangeService.Get(ChangeNo);
            //加工成扩展对象
            ProjectBoqChangeEx boqChangeEx = HDAutoMapper.DynamicMap<ProjectBoqChangeEx>(boqChange);
            boqChangeEx.ChangeInfo = chInfo;
            return boqChangeEx;

        }
    }  
}
