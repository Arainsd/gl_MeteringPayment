using Erp.CommonData.Entity;
using HD.MeteringPayment.Module.BootLoader.Config;
using HD.MeteringPayment.Module.Forms.ContractBoqMng;
using HD.MeteringPayment.Module.Forms.WBSBoqMng;
using System;

namespace HD.MeteringPayment.Module.BootLoader.Menu
{
    public class XMBMeteringPaymentMenu : GpNavBarTreeMenu
    {

        public XMBMeteringPaymentMenu()
        {
            GpNavBarTreeItem subBasicInfo = new GpNavBarTreeItem("基础信息");
            Items.Add(subBasicInfo);
            GpNavBarTreeItem subBasicInfo1 = new GpNavBarTreeItem("合同清单", OnContractBoqMng_Click);
            GpNavBarTreeItem subBasicInfo2 = new GpNavBarTreeItem("WBS清单", OnWBSMng_Click);
            GpNavBarTreeItem subBasicInfo3 = new GpNavBarTreeItem("进度计量", OnProgressMetering_Click);
            GpNavBarTreeItem subBasicInfo4 = new GpNavBarTreeItem("计量报表", OnProgressMeteringRpt_Click);

            subBasicInfo.Items.Add(subBasicInfo1);
            subBasicInfo.Items.Add(subBasicInfo2);
            subBasicInfo.Items.Add(subBasicInfo3);
            subBasicInfo.Items.Add(subBasicInfo4);


        }
      
    /// <summary>
    /// 合同清单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="arg"></param>
    /// <param name="arg1"></param>
    public void OnContractBoqMng_Click(Object sender, Object arg, Object arg1)
        {
            GpNavBarTreeItem item = sender as GpNavBarTreeItem;
            AppForm.CurrentForm.ChangeForm(item.Caption, 
                new HD.MeteringPayment.Module.Forms.ContractBoqMng.frmPrjBoq(AppConfig.SelectProject.ProjectNo, AppConfig.SelectProject.ProjectName), item.Caption);
        }

        /// <summary>
        /// WBS清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <param name="arg1"></param>
        public void OnWBSMng_Click(Object sender, Object arg, Object arg1)
        {
            GpNavBarTreeItem item = sender as GpNavBarTreeItem;
            AppForm.CurrentForm.ChangeForm(item.Caption, 
                new HD.MeteringPayment.Module.Forms.WBSBoqMng.frmPrjBoq(AppConfig.SelectProject.ProjectNo, AppConfig.SelectProject.ProjectName), item.Caption);
        }

        /// <summary>
        /// 进度计量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <param name="arg1"></param>
        public void OnProgressMetering_Click(Object sender, Object arg, Object arg1)
        {
            GpNavBarTreeItem item = sender as GpNavBarTreeItem;
            AppForm.CurrentForm.ChangeForm(item.Caption, new HD.MeteringPayment.Module.Forms.ProgressMeteringMng.PMListControl(AppConfig.SelectProject.ProjectNo, AppConfig.SelectProject.ProjectName, AppConfig.GetRoleLevel()), item.Caption);
        }

        /// <summary>
        /// 计量报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <param name="arg1"></param>
        public void OnProgressMeteringRpt_Click(Object sender, Object arg, Object arg1)
        {
            GpNavBarTreeItem item = sender as GpNavBarTreeItem;
            AppForm.CurrentForm.ChangeForm(item.Caption, 
                new HD.MeteringPayment.Module.Forms.MeteringRptMng.MeteringRptLstControl(AppConfig.SelectProject.ProjectNo, AppConfig.SelectProject.ProjectName), item.Caption);
        }
    }
}
