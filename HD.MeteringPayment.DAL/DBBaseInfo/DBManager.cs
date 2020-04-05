using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using Hondee.Common.DB;
using Hondee.Common.HDException;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using Erp.CommonData.Entity.Business;


namespace HD.MeteringPayment.DAL.DBBaseInfo
{
    public class DBManager : IManager
    {
        private HdDbCmdManager hdDbCmdManager = HdDbCmdManager.GetInstance();
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        public List<Manager> GetList(string whereQuery)
        {
            return hdDbCmdManager.QueryForList<Manager>(@"SELECT *
                                                            FROM ERP_Subpay.dbo.v_cmn_manager " + whereQuery, System.Data.CommandType.Text, null);
        }
        /// <summary>
        /// 加一个用户访问一次表，速度慢
        /// </summary>
        public Manager Add(Manager entity, string operationBy)
        {
            hdDbCmdManager.ExecuteTran((tran) =>
            {
                InnerAdd(tran, entity, operationBy);
            });
            return entity;
        }
        /// <summary>
        /// 多个用户一起加入，速度快
        /// </summary>
        public void AddList(List<Manager> list, string operationBy)
        {
            hdDbCmdManager.ExecuteTran((tran) =>
            {
                foreach (Manager entity in list)
                {
                    InnerAdd(tran, entity, operationBy);
                }
            });
        }
        public Manager Update(Manager entity, string operationBy)
        {
            hdDbCmdManager.ExecuteTran((tran) =>
            {
                InnerUpdate(tran, entity, operationBy);
            });
            return entity;
        }
        public void Delete(Manager entity, string operationBy)
        {
            hdDbCmdManager.ExecuteTran((tran) =>
            {
                InnerDelete(tran, entity, operationBy);
            });
        }

        /// <summary>
        /// 保存更改
        /// </summary>
        /// <param name="addMng"></param>
        /// <param name="updateMng"></param>
        /// <param name="delMng"></param>
        /// <param name="OperationBy"></param>
        public void Save(List<Manager> addMng, List<Manager> updateMng, List<Manager> delMng, string OperationBy)
        {
            hdDbCmdManager.ExecuteTran((tran) =>
            {
                //删除
                if (delMng != null && delMng.Count > 0)
                {
                    delMng.ForEach(m =>
                    {
                        InnerDelete(tran, m, OperationBy);
                    });
                }
                //修改
                if (updateMng != null && updateMng.Count > 0)
                {
                    updateMng.ForEach(m =>
                    {
                        InnerUpdate(tran, m, OperationBy);
                    });
                }
                //新增
                if (addMng != null && addMng.Count > 0)
                {
                    addMng.ForEach(m =>
                    {
                        InnerAdd(tran, m, OperationBy);
                    });
                }
            });
        }

        /// <summary>
        /// 通过登陆名找用户机构
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public List<CacheOrg> FindOrgByLoginName(string LoginName)
        {
            return InnerFindOrgByLoginName(null, LoginName);
        }

        #region 内部方法
        private List<CacheOrg> InnerFindOrgByLoginName(SqlTransaction tran, string LoginName)
        {
            List<CacheOrg> result = new List<CacheOrg>();
            string sqlStr = string.Format(@"SELECT o.*
                                              FROM ERP_Identity.Org.Org o
                                              LEFT OUTER JOIN ERP_Identity.Org.OrgMember om ON o.OrgNo = om.OrgNo
                                             WHERE o.StatId = 1 AND om.StatId = 1 AND om.LoginName = '{0}'", LoginName);
            if (tran == null)
            {
                result = hdDbCmdManager.QueryForList<CacheOrg>(sqlStr, CommandType.Text, null, tran);
            }
            else
            {
                result = hdDbCmdManager.QueryForList<CacheOrg>(sqlStr, CommandType.Text, null, null);
            }

            return result;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="projectinfo"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        private Manager InnerAdd(SqlTransaction tran, Manager manager, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();

            cmds.Add(new CmdParameter("@LoginName", manager.LoginName));
            cmds.Add(new CmdParameter("@ManagerNo", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@UserName", manager.UserName));
            cmds.Add(new CmdParameter("@UserType", manager.UserType));
            cmds.Add(new CmdParameter("@OrgNo", manager.OrgNo));
            cmds.Add(new CmdParameter("@OrgName", manager.OrgName));
            cmds.Add(new CmdParameter("@ProjectName", manager.ProjectName));
            cmds.Add(new CmdParameter("@ProjectNo", manager.ProjectNo));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@Id", 0, System.Data.ParameterDirection.Output));

            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Cmn_Manager_Add]", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            return manager;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="period"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        private Manager InnerUpdate(SqlTransaction tran, Manager manager, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ManagerNo", manager.ManagerNo));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@LoginName", manager.LoginName));
            cmds.Add(new CmdParameter("@UserName", manager.UserName));
            cmds.Add(new CmdParameter("@UserType", manager.UserType));
            cmds.Add(new CmdParameter("@OrgNo", manager.OrgNo));
            cmds.Add(new CmdParameter("@OrgName", manager.OrgName));
            cmds.Add(new CmdParameter("@ProjectName", manager.ProjectName));
            cmds.Add(new CmdParameter("@ProjectNo", manager.ProjectNo));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Cmn_Manager_Update]", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            return manager;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="period"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        private void InnerDelete(SqlTransaction tran, Manager manager, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ManagerNo", manager.ManagerNo));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_Subpay].[dbo].[Cmn_Manager_Delete]", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
        }
        #endregion
    }
}
