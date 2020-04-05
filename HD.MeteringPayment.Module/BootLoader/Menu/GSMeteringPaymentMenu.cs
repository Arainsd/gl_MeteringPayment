using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using Erp.CommonData.Entity;
using HD.MeteringPayment.Module.Forms.BaseInfoMng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HD.MeteringPayment.Module.Forms.BaseInfoMng.ManagerMng;
using HD.MeteringPayment.Module.BootLoader.Config;

namespace HD.MeteringPayment.Module.BootLoader.Menu
{
    public class GSMeteringPaymentMenu : GpNavBarTreeMenu
    {
        public GSMeteringPaymentMenu()
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
            AppForm.CurrentForm.ChangeForm(item.Caption, new Forms.ContractBoqMng.BidTreeSelectControl(1), item.Caption);
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
            //AppForm.CurrentForm.ChangeForm(item.Caption,
            //    new HD.MeteringPayment.Module.Forms.WBSBoqMng.frmPrjBoq(AppConfig.SelectProject.ProjectNo, AppConfig.SelectProject.ProjectName), item.Caption);

            AppForm.CurrentForm.ChangeForm(item.Caption, new Forms.ContractBoqMng.BidTreeSelectControl(2), item.Caption);
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
            //AppForm.CurrentForm.ChangeForm(item.Caption, new HD.MeteringPayment.Module.Forms.ProgressMeteringMng.PMListControl(), item.Caption);
            AppForm.CurrentForm.ChangeForm(item.Caption, new Forms.ContractBoqMng.BidTreeSelectControl(3), item.Caption);
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
            AppForm.CurrentForm.ChangeForm(item.Caption, new Forms.ContractBoqMng.BidTreeSelectControl(4), item.Caption);
        }
    }
}
