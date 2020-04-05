using Erp.CommonData.Entity.Business;
using Erp.SharedLib.Presentation.Lib;
using HD.MeteringPayment.Domain.Client;
using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using HD.MeteringPayment.Module.BootLoader.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HD.MeteringPayment.Module.BootLoader
{
    public class LoginUser
    {
        public Boolean HasGrpMngrRole { get; set; }
        public Boolean HasBaseMngrRole { get; set; }
        public Boolean HasXMBMngrRole { get; set; }
        public List<ProjectInfo> LstProject { get; set; }
        public List<ProjectBid> LstFGSOrgs { get; set; }
        public List<ProjectBid> LstGrpOrgs { get; set; }
        public AppConfig.ManagerRole LoginRole { get; set; }
        public int RoleCount { get; set; }
        public ProjectBid LoginOrg { get; set; }
        public LoginUser()
        {
        }
        public LoginUser(string sLoginName)
        {
            GetMngrRole(sLoginName);
        }

        private void GetMngrRole(string sLoginName)
        {
            LstGrpOrgs = new List<ProjectBid>();
            LstFGSOrgs = new List<ProjectBid>();
            LstProject = new List<ProjectInfo>();
            List<Manager> LstMngr = new List<Manager>();
            //LstMngr = CacheCollection.GetList<Manager>("ERP_Subpay.dbo.cmn_manager", " WHERE StatId=1 and LoginName='" + sLoginName + "'").ToList();
            LstMngr = new MeteringPaymentClient(AppConfig.SEndPoint, AppConfig.SLoginName, AppConfig.SPsw).GetIManagerService().GetList(String.Format(@" WHERE StatId=1 and LoginName='{0}' 
                                                                                                                                                         ORDER BY UserType ASC, ProjectName ASC",sLoginName));
            if(AppConfig.LstBid == null)
            {
                AppConfig.LstBid = new MeteringPaymentClient(AppConfig.SEndPoint, AppConfig.SLoginName, AppConfig.SPsw).GetIProjectBidService().GetList("WHERE StatId = 1");
            }
            List<Manager> LstGrpMngr = LstMngr.FindAll(m => m.UserType == 3).ToList();
            List<Manager> LstFGSMngr = LstMngr.FindAll(m => m.UserType == 2).ToList();
            List<Manager> LstXMBMngr = LstMngr.FindAll(m => m.UserType == 1).ToList();
            if (LstGrpMngr.Count > 0)
            {
                HasGrpMngrRole = true;
                LoginRole = AppConfig.ManagerRole.GrpMngr;
                RoleCount++;
            }
            if (LstFGSMngr.Count > 0)
            {
                HasBaseMngrRole = true;
                LoginRole = AppConfig.ManagerRole.FGSMngr;
                RoleCount++;
            }
            if (LstXMBMngr.Count > 0)
            {
                HasXMBMngrRole = true;
                LoginRole = AppConfig.ManagerRole.XMBMngr;
                RoleCount++;
            }
            if (HasGrpMngrRole)
            {
                AppConfig.LstBid.ForEach(m =>
                {
                    LstGrpMngr.ForEach(n =>
                    {
                        if (m.BidNo == n.OrgNo)
                            LstGrpOrgs.Add(m);
                    });
                });
            }
            if (HasBaseMngrRole)
            {
                AppConfig.LstBid.ForEach(m =>
                {
                    LstFGSMngr.ForEach(n =>
                    {
                        if (m.BidNo == n.OrgNo)
                            LstFGSOrgs.Add(m);
                    });
                });
            }
            if (HasXMBMngrRole)
            {
                LstProject.Clear();
                LstXMBMngr.ForEach(m =>
                {
                    ProjectInfo project = new ProjectInfo();
                    project.ProjectNo = m.ProjectNo;
                    project.ProjectName = m.ProjectName;
                    LstProject.Add(project);
                });
            }
        }
        public void SetRoles(String sRole)
        {
            switch (sRole)
            {
                case "业主":
                    LoginRole = AppConfig.ManagerRole.GrpMngr;
                    break;
                case "监理":
                    LoginRole = AppConfig.ManagerRole.FGSMngr;
                    break;
                case "标段管理员":
                    LoginRole = AppConfig.ManagerRole.XMBMngr;
                    break;
            }
        }
    }
}
