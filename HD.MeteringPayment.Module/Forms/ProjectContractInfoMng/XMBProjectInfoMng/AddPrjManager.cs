using DevExpress.XtraEditors;
using Erp.CommonData;
using Erp.CommonData.Entity;
using Erp.CommonData.Entity.Business;
using Erp.CommonData.Workflow;
using Erp.GpServiceClient;
using Erp.SharedLib.Presentation.FormBases;
using Erp.SharedLib.Presentation.Interface;
using Erp.SharedLib.Presentation.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Erp.SharedLib.Presentation.WorkFlowButton;
using Hondee.Common.DataConvertor;

namespace HD.MeteringPayment.Module.Forms.ProjectContractInfoMng.XMBProjectInfoMng
{
    public partial class AddPrjManager : GpFormBase
    {
        private ServiceDataSet dsGpUser = null;
        private ServiceDataSet dsOrgMember = null;
        private DataTable dtSelectedGpUser = new DataTable();
        public List<CacheGpuser> LstSelectUser = null;
        private DataTable dtSelectOrg = new DataTable();
        private DataTable dtOrgs;

        public AddPrjManager()
        {
            InitializeComponent();

            dtSelectedGpUser.Columns.Add("isSelected", Type.GetType("System.Boolean"));
            dtSelectedGpUser.Columns.Add("LoginName", Type.GetType("System.String"));
            dtSelectedGpUser.Columns.Add("UserName", Type.GetType("System.String"));
            dtSelectedGpUser.Columns.Add("OrgNo", Type.GetType("System.String"));
            dtSelectedGpUser.Columns.Add("OrgName", Type.GetType("System.String"));

            dtSelectOrg.Columns.Add("isSelected", Type.GetType("System.Boolean"));
            dtSelectOrg.Columns.Add("OrgNo", Type.GetType("System.String"));
            dtSelectOrg.Columns.Add("OrgName", Type.GetType("System.String"));

        }
        public AddPrjManager(wfmenuitem menuitem, List<BusinessDataInfo> lstRefDataInfo, int intGrade, List<CacheOrg> lstOrgs, string strSysNo)
        {
            InitializeComponent();
            this.Text = menuitem.ItemName;

            //strApplicantOrgNo = applicantOrgNo;

            dtSelectedGpUser.Columns.Add("isSelected", Type.GetType("System.Boolean"));
            dtSelectedGpUser.Columns.Add("LoginName", Type.GetType("System.String"));
            dtSelectedGpUser.Columns.Add("UserName", Type.GetType("System.String"));
            dtSelectedGpUser.Columns.Add("OrgNo", Type.GetType("System.String"));
            dtSelectedGpUser.Columns.Add("OrgName", Type.GetType("System.String"));

            dtSelectOrg.Columns.Add("isSelected", Type.GetType("System.Boolean"));
            dtSelectOrg.Columns.Add("OrgNo", Type.GetType("System.String"));
            dtSelectOrg.Columns.Add("OrgName", Type.GetType("System.String"));

        }

        //public void LoadOrgList()
        //{
        //    dtOrgs = Erp.GpServiceClient.GpClient.CreateInstance().Execute("SELECT OrgNo,OrgName,ParentOrgNo,Position FROM [ERP_Identity].[Org].[v_Org_OrgMbrPosition]", CommandType.Text).Table;
        //    tlOrg.DataSource = dtOrgs;
        //    tlOrg.ExpandAll();
        //}
        private void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }

        private void teFilterCondition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                search();
            }
        }
        private void search()
        {
            DoWork("数据查询中...", "请稍后", () =>
            {

                dsGpUser = Erp.GpServiceClient.GpClient.CreateInstance().Execute("SELECT *  FROM [ERP_Identity].[Auth].[Gpuser]   where LoginName like '%" + teFilterCondition.Text + "%' or UserName like'%" + teFilterCondition.Text + "%' order by id asc", CommandType.Text);
            }, (ex) =>
            {
                if (ex == null)
                {
                    gcGpUser.DataSource = dsGpUser.Data.Tables[0];
                }
            });
        }
        //双击用户列表,判断用户所属部门,如果部门数=0,提示没有可选部门,如果部门数=1,将该行的用户添入右边已选择列表.如果部门数>1,弹出部门选择窗体,用户选择部门后,加入选择列表
        private void gvGpUser_DoubleClick(object sender, EventArgs e)
        {
            if (gvGpUser.GetFocusedDataRow() != null)
            {
                string strLoginName = gvGpUser.GetFocusedDataRow()["LoginName"].ToString();
                string strUserName = gvGpUser.GetFocusedDataRow()["UserName"].ToString();
                AddRowToList(strLoginName, strUserName);
                //DataTable dtOrgMember = searchOrgMember(gvGpUser.GetFocusedDataRow()["LoginName"].ToString());
                //if (dtOrgMember.Rows.Count == 0)
                //{
                //    XtraMessageBox.Show("没有找到该用户所属部门");
                //    return;
                //}
                //if (dtOrgMember.Rows.Count == 1)
                //{
                //    string strLoginName = dtOrgMember.Rows[0]["LoginName"].ToString();
                //    string strUserName = dtOrgMember.Rows[0]["UserName"].ToString();
                //    AddRowToList(strLoginName, strUserName);
                //    return;
                //}
                //if (dtOrgMember.Rows.Count > 1)
                //{
                //    frmSelectOrg selectorg = new frmSelectOrg(dtOrgMember);
                //    selectorg.reforginfoevent += selectorg_reforginfoevent;
                //    selectorg.ShowDialog();

                //}
            }
        }

        void selectorg_reforginfoevent(string loginname, string username, OrgInfo org)
        {
            AddRowToList(loginname, username);
        }


        private DataTable searchOrgMember(string sLoginName)
        {
            DataTable dtOrgMember = null;
            dsOrgMember = Erp.GpServiceClient.GpClient.CreateInstance().Execute("SELECT *  FROM [ERP_Identity].[Org].[VOrgGpuser]   where LoginName ='" + sLoginName + "'", CommandType.Text);
            dtOrgMember = dsOrgMember.Data.Tables[0];
            //DoWork("数据查询中...", "请稍后", () =>
            //{
                
            //}, (ex) =>
            //{
            //    if (ex == null)
            //    {
            //        dtOrgMember = dsOrgMember.Data.Tables[0];
            //    }
            //});
            return dtOrgMember;
        }
        /// <summary>
        /// 向已选用户列表中添加用户
        /// </summary>
        /// <param name="dr"></param>
        private void AddRowToList(string loginName, string userName)
        {
            Boolean isExit = false;
            for (int i = 0; i < dtSelectedGpUser.Rows.Count; i++)
            {
                if (dtSelectedGpUser.Rows[i]["LoginName"].ToString() == loginName)
                {
                    isExit = true;
                    break;
                }
            }
            if (!isExit)
            {

                DataRow newRow;
                newRow = dtSelectedGpUser.NewRow();
                newRow["isSelected"] = false;
                newRow["LoginName"] = loginName;
                newRow["UserName"] = userName;
                DataTable dtOrg = searchOrgMember(loginName);
                string orgName = "";
                foreach (DataRow row in dtOrg.Rows)
                {
                   orgName += (row["OrgName"].ToString() + ",");
                }
                newRow["OrgName"] = orgName.TrimEnd(',');
                dtSelectedGpUser.Rows.Add(newRow);
                gcSelectedGpUser.DataSource = dtSelectedGpUser;
            }
        }

        /// <summary>
        /// 删除已选用户列表中的用户
        /// </summary>
        /// <param name="dr"></param>
        private void DeleteRowFromList(DataTable dt)
        {

            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                if (Boolean.Parse(dt.Rows[i]["isSelected"].ToString()))
                {
                    dt.Rows.Remove(dt.Rows[i]);
                }
            }
            gcSelectedGpUser.DataSource = dtSelectedGpUser;
        }

        private void btnDelSelected_Click(object sender, EventArgs e)
        {
            DeleteRowFromList(dtSelectedGpUser);
        }

        private void repositoryItemCheckEdit1_Click(object sender, EventArgs e)
        {
            gvSelectedGpUser.PostEditor();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }







   



        public List<CacheOrg> LstOrg
        {
            get
            {
                List<CacheOrg> LstDepts = new List<CacheOrg>();
                for (int i = 0; i < dtSelectOrg.Rows.Count; i++)
                {
                    LstDepts.Add(CacheCollection.OrgList.Find(m => m.OrgNo == dtSelectOrg.Rows[i]["OrgNo"].ToString()));
                }
                return LstDepts;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string ApplicantOrgNo
        {
            get;
            set;
        }

        private void xtraTabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                dtOrgs = Erp.GpServiceClient.GpClient.CreateInstance().Execute("SELECT OrgNo,OrgName,ParentOrgNo,Position FROM [ERP_Identity].[Org].[v_Org_OrgMbrPosition]", CommandType.Text).Table;
                tlOrgs.DataSource = dtOrgs;
                tlOrgs.ExpandAll();
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                dtOrgs = Erp.GpServiceClient.GpClient.CreateInstance().Execute("SELECT OrgNo,OrgName,ParentOrgNo FROM [ERP_Identity].[Org].[Org]", CommandType.Text).Table;
                tlOrgs.DataSource = dtOrgs;
                tlOrgs.ExpandAll();
            }
            //if (xtraTabControl1.SelectedTabPageIndex == 2)
            //{
            //    List<CacheGpuser> LstRegularContacters = GpWorkflowClient.CreateInstance().GetRegularContacters(LoginInfor.LoginName, 20);
            //    gcRegularContacters.DataSource = LstRegularContacters;
            //}
        }

        private void tlOrgs_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (tlOrgs.FocusedNode["OrgNo"] != null)
            {
                List<CacheGpuser> LstUser = Erp.GpServiceClient.GpIdentityClient.CreateInstance().GetOrgMembers(tlOrgs.FocusedNode["OrgNo"].ToString());

                gcSelectUsers.DataSource = LstUser;
            }
        }


        public string refNo
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {

            }
        }


        private void gvSelectUsers_DoubleClick(object sender, EventArgs e)
        {
            if (gvSelectUsers.GetFocusedRow() != null)
            {
                string strLoginName = (gvSelectUsers.GetFocusedRow() as CacheGpuser).LoginName;
                string strUserName = (gvSelectUsers.GetFocusedRow() as CacheGpuser).UserName;
                AddRowToList(strLoginName, strUserName);
                //DataTable dtOrgMember = searchOrgMember(((CacheGpuser)gvSelectUsers.GetFocusedRow()).LoginName);
                //if (dtOrgMember.Rows.Count == 0)
                //{
                //    XtraMessageBox.Show("没有找到该用户所属部门");
                //    return;
                //}
                //if (dtOrgMember.Rows.Count == 1)
                //{
                //    string strLoginName = dtOrgMember.Rows[0]["LoginName"].ToString();
                //    string strUserName = dtOrgMember.Rows[0]["UserName"].ToString();
                //    string strOrgNo = dtOrgMember.Rows[0]["OrgNo"].ToString();
                //    string strOrgName = dtOrgMember.Rows[0]["OrgName"].ToString();
                //    AddRowToList(strLoginName, strUserName, strOrgNo, strOrgName);
                //    return;
                //}
                //if (dtOrgMember.Rows.Count > 1)
                //{
                //    frmSelectOrg selectorg = new frmSelectOrg(dtOrgMember);
                //    selectorg.reforginfoevent += selectorg_reforginfoevent;
                //    selectorg.ShowDialog();

                //}
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            LstSelectUser = CollectionHelper.ConvertTo<CacheGpuser>(dtSelectedGpUser).ToList();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        //private void gvRegularContacters_DoubleClick(object sender, EventArgs e)
        //{
        //    if (gvRegularContacters.GetFocusedRow() != null)
        //    {
        //        DataTable dtOrgMember = searchOrgMember(((CacheGpuser)gvRegularContacters.GetFocusedRow()).LoginName);
        //        if (dtOrgMember.Rows.Count == 0)
        //        {
        //            XtraMessageBox.Show("没有找到该用户所属部门");
        //            return;
        //        }
        //        if (dtOrgMember.Rows.Count == 1)
        //        { 
        //            string strLoginName = dtOrgMember.Rows[0]["LoginName"].ToString();
        //            string strUserName = dtOrgMember.Rows[0]["UserName"].ToString();
        //            string strOrgNo = dtOrgMember.Rows[0]["OrgNo"].ToString();
        //            string strOrgName = dtOrgMember.Rows[0]["OrgName"].ToString();
        //            AddRowToList(strLoginName, strUserName, strOrgNo, strOrgName);
        //            return;
        //        }
        //        if (dtOrgMember.Rows.Count > 1)
        //        {
        //            frmSelectOrg selectorg = new frmSelectOrg(dtOrgMember);
        //            selectorg.reforginfoevent += selectorg_reforginfoevent;
        //            selectorg.ShowDialog();

        //        }
        //    }
        //}
    }
}
