using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using Erp.CommonData.Entity;
using HD.MeteringPayment.Module.Forms.BaseInfoMng.ManagerMng;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HD.MeteringPayment.Module.BootLoader.Menu
{
    public class BaseDataMngMenu  : GpNavBarTreeMenu
    {
        public BaseDataMngMenu()
        {
            GpNavBarTreeItem subUser = new GpNavBarTreeItem("管理员设置");
            Items.Add(subUser);
            GpNavBarTreeItem subUser1 = new GpNavBarTreeItem("标段管理员设置", subUser1_LinkClicked);
            GpNavBarTreeItem subUser2 = new GpNavBarTreeItem("监理管理员设置", subUser2_LinkClicked);
            GpNavBarTreeItem subUser3 = new GpNavBarTreeItem("业主管理员设置", subUser3_LinkClicked);

            subUser.Items.Add(subUser1);
            subUser.Items.Add(subUser2);
            subUser.Items.Add(subUser3);


        }

        /// <summary>
        /// 标段管理员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <param name="arg1"></param>
        public void subUser1_LinkClicked(Object sender, Object arg, Object arg1)
        {
            GpNavBarTreeItem item = sender as GpNavBarTreeItem;
            AppForm.CurrentForm.ChangeForm(item.Caption, new frmXMBManager(), item.Caption);
        }

        /// <summary>
        /// 监理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <param name="arg1"></param>
        public void subUser2_LinkClicked(Object sender, Object arg, Object arg1)
        {
            GpNavBarTreeItem item = sender as GpNavBarTreeItem;

            AppForm.CurrentForm.ChangeForm(item.Caption, new frmFGSManager(), item.Caption);
        }

        /// <summary>
        /// 业主
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <param name="arg1"></param>
        public void subUser3_LinkClicked(Object sender, Object arg, Object arg1)
        {
            GpNavBarTreeItem item = sender as GpNavBarTreeItem;

            AppForm.CurrentForm.ChangeForm(item.Caption, new frmGSManager(), item.Caption);
        }

    }
}
