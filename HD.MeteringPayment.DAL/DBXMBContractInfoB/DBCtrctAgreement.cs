using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using Hondee.Common.DB;
using Hondee.Common.HDException;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;


namespace HD.MeteringPayment.DAL.DBBaseInfo
{
    public class DBCtrctAgreement : ICtrctAgreement
    {
        private HdDbCmdManager hdDbCmdManager = HdDbCmdManager.GetInstance();
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        public List<CtrctAgreement> GetList(string whereQuery)
        {
            return hdDbCmdManager.QueryForList<CtrctAgreement>(@"SELECT * FROM [ERP_SalesMarketing].[dbo].[ctrct_prjagreement] " + whereQuery, System.Data.CommandType.Text, null);
        }
        //public ProjectDepInfo Add(ProjectDepInfo entity, string operationBy)
        //{
        //    hdDbCmdManager.ExecuteTran((tran) =>
        //    {
        //        InnerAdd(tran, entity, operationBy);
        //    });
        //    return entity;
        //}
        //public ProjectDepInfo Update(ProjectDepInfo entity, string operationBy)
        //{
        //    hdDbCmdManager.ExecuteTran((tran) =>
        //    {
        //        InnerUpdate(tran, entity, operationBy);
        //    });
        //    return entity;
        //}
        //public ProjectDepInfo Delete(ProjectDepInfo entity, string operationBy)
        //{
        //    hdDbCmdManager.ExecuteTran((tran) =>
        //    {
        //        InnerDelete(tran, entity, operationBy);
        //    });
        //    return entity;
        //}
        public int GetMaxNumber()
        {
            DataSet ds = hdDbCmdManager.QueryForDataSet(@"SELECT MAX(PeriodsNumber) as PeriodsNumber FROM [ERP_SettlementCollection].[dbo].[cmn_periods] ", System.Data.CommandType.Text, null);
            if (ds.Tables[0].Rows.Count > 0)
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["PeriodsNumber"].ToString()))
                    return int.Parse(ds.Tables[0].Rows[0]["PeriodsNumber"].ToString()) + 1;
                else
                    return 1;
            else
                return 1;
        }
        #region 内部方法
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="projectinfo"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        //private CtrctAgreement InnerAdd(SqlTransaction tran, CtrctAgreement period, string operationBy)
        //{
        //    List<CmdParameter> cmds = new List<CmdParameter>();
        //    cmds.Add(new CmdParameter("@PeriodsNum", period.PeriodsNum));
        //    cmds.Add(new CmdParameter("@PeriodsNumber", period.PeriodsNumber));
        //    cmds.Add(new CmdParameter("@StartingDate", period.StartingDate));
        //    cmds.Add(new CmdParameter("@EndingDate", period.EndingDate));
        //    cmds.Add(new CmdParameter("@Remark", period.Remark));
        //    cmds.Add(new CmdParameter("@OperationBy", operationBy));
        //    cmds.Add(new CmdParameter("@Id", 0, System.Data.ParameterDirection.Output));
        //    cmds.Add(new CmdParameter("@PeriodsNo", "", System.Data.ParameterDirection.Output));
        //    cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
        //    cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
        //    ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
        //    hdDbCmdManager.Execute("[ERP_SettlementCollection].[dbo].[Cmn_Periods_Add]", CommandType.StoredProcedure, pResult.Parameters, tran);
        //    if (!Convert.ToBoolean(pResult["@Ok"]))
        //        throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
        //        {
        //            ErrorMessage = pResult["@Infor"].ToString()
        //        });
        //    return period;
        //}
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="period"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        //private ProjectDepInfo InnerUpdate(SqlTransaction tran, ProjectDepInfo period, string operationBy)
        //{
        //    List<CmdParameter> cmds = new List<CmdParameter>();
        //    cmds.Add(new CmdParameter("@PeriodsNo", period.PeriodsNo));
        //    cmds.Add(new CmdParameter("@PeriodsNum", period.PeriodsNum));
        //    cmds.Add(new CmdParameter("@StartingDate", period.StartingDate));
        //    cmds.Add(new CmdParameter("@EndingDate", period.EndingDate));
        //    cmds.Add(new CmdParameter("@Remark", period.Remark));
        //    cmds.Add(new CmdParameter("@OperationBy", operationBy));
        //    cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
        //    cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
        //    ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
        //    hdDbCmdManager.Execute("[ERP_SettlementCollection].[dbo].[Cmn_Periods_Update]", CommandType.StoredProcedure, pResult.Parameters, tran);
        //    if (!Convert.ToBoolean(pResult["@Ok"]))
        //        throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
        //        {
        //            ErrorMessage = pResult["@Infor"].ToString()
        //        });
        //    return period;
        //}
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="period"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        //private void InnerDelete(SqlTransaction tran, ProjectDepInfo period, string operationBy)
        //{
        //    List<CmdParameter> cmds = new List<CmdParameter>();
        //    cmds.Add(new CmdParameter("@PeriodsNo", period.PeriodsNo));
        //    cmds.Add(new CmdParameter("@OperationBy", operationBy));
        //    cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
        //    cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
        //    ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
        //    hdDbCmdManager.Execute("[ERP_SettlementCollection].[dbo].[Cmn_Periods_Delete]", CommandType.StoredProcedure, pResult.Parameters, tran);
        //    if (!Convert.ToBoolean(pResult["@Ok"]))
        //        throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
        //        {
        //            ErrorMessage = pResult["@Infor"].ToString()
        //        });
        //}
        #endregion
    }
}
