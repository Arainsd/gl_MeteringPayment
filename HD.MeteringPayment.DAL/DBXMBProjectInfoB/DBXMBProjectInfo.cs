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
    public class DBXMBProjectInfo : IXMBProjectInfo
    {
        private HdDbCmdManager hdDbCmdManager = HdDbCmdManager.GetInstance();
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        public List<XMBProjectInfo> GetList(string whereQuery)
        {
            return hdDbCmdManager.QueryForList<XMBProjectInfo>(@"SELECT * FROM [ERP_Project].[Project].[Project] " + whereQuery, System.Data.CommandType.Text, null);
        }
        //public XMBProjectInfo Add(XMBProjectInfo entity, string operationBy)
        //{
        //    hdDbCmdManager.ExecuteTran((tran) =>
        //    {
        //        InnerAdd(tran, entity, operationBy);
        //    });
        //    return entity;
        //}
        public XMBProjectInfo Update(XMBProjectInfo entity, string operationBy)
        {
            hdDbCmdManager.ExecuteTran((tran) =>
            {
                InnerUpdate(tran, entity, operationBy);
            });
            return entity;
        }
        //public XMBProjectInfo Delete(XMBProjectInfo entity, string operationBy)
        //{
        //    hdDbCmdManager.ExecuteTran((tran) =>
        //    {
        //        InnerDelete(tran, entity, operationBy);
        //    });
        //    return entity;
        //}
        public int GetMaxNumber()
        {
            DataSet ds = hdDbCmdManager.QueryForDataSet(@"SELECT MAX(PeriodsNumber) as PeriodsNumber FROM [ERP_Project].[Project].[Project] ", System.Data.CommandType.Text, null);
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
        //private XMBProjectInfo InnerAdd(SqlTransaction tran, XMBProjectInfo period, string operationBy)
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
        //    hdDbCmdManager.Execute("[ERP_Project].[dbo].[Project_Project_Add]", CommandType.StoredProcedure, pResult.Parameters, tran);
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
        private XMBProjectInfo InnerUpdate(SqlTransaction tran, XMBProjectInfo period, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ProjectNo", period.ProjectNo));
            cmds.Add(new CmdParameter("@ProjectNoNew", period.ProjectNoNew));
            cmds.Add(new CmdParameter("@AdjustNewNo", 0));
            cmds.Add(new CmdParameter("@OrgNo", period.OrgNo));
            cmds.Add(new CmdParameter("@OrgName", period.OrgName));
            cmds.Add(new CmdParameter("@ProjectCode", period.ProjectCode));
            cmds.Add(new CmdParameter("@ProjectName", period.ProjectName));
            cmds.Add(new CmdParameter("@Catalog", period.Catalog));
            cmds.Add(new CmdParameter("@ProjectClass", period.ProjectClass));
            cmds.Add(new CmdParameter("@Duration", period.Duration));
            cmds.Add(new CmdParameter("@ProjectDuration", period.ProjectDuration));
            cmds.Add(new CmdParameter("@Currency", period.Currency));
            cmds.Add(new CmdParameter("@CurrencyCode", period.CurrencyCode));
            cmds.Add(new CmdParameter("@CtrctCurrencyCode", period.CtrctCurrencyCode));
            cmds.Add(new CmdParameter("@CtrctCurrency", period.CtrctCurrency));
            cmds.Add(new CmdParameter("@CtrctExChangeRate", period.CtrctExChangeRate));
            cmds.Add(new CmdParameter("@DomOvsea", period.DomOvsea));
            //cmds.Add(new CmdParameter("@AddressId", period.AddressId));
            cmds.Add(new CmdParameter("@AddressName", period.AddressName));
            cmds.Add(new CmdParameter("@Country", period.Country));
            cmds.Add(new CmdParameter("@District", period.District));
            cmds.Add(new CmdParameter("@Province", period.Province));
            cmds.Add(new CmdParameter("@City", period.City));
            //cmds.Add(new CmdParameter("@Address", period.Address));
            //cmds.Add(new CmdParameter("@ParentprojectNo", period.ParentprojectNo));
            //cmds.Add(new CmdParameter("@ParentprojectName", period.ParentprojectName));
            cmds.Add(new CmdParameter("@Description", period.Description));
            cmds.Add(new CmdParameter("@ContractAmount", period.ContractAmount));
            cmds.Add(new CmdParameter("@ProjectManager", period.ProjectManager));
            cmds.Add(new CmdParameter("@BidDate", period.BidDate));
            cmds.Add(new CmdParameter("@SignDate", period.SignDate));
            cmds.Add(new CmdParameter("@BeginningDate", period.BeginningDate));
            cmds.Add(new CmdParameter("@Endingdate", period.Endingdate));
            cmds.Add(new CmdParameter("@ActualBeginningDate", period.ActualBeginningDate));
            cmds.Add(new CmdParameter("@ActualEndingDate", period.ActualEndingDate));
            cmds.Add(new CmdParameter("@PlanDate", period.PlanDate));
            cmds.Add(new CmdParameter("@StageNo", period.StageNo));
            cmds.Add(new CmdParameter("@Stage", period.Stage));
            //cmds.Add(new CmdParameter("@ProjectphaseNo", period.ProjectphaseNo));
            //cmds.Add(new CmdParameter("@ProjectphaseName", period.ProjectphaseName));
            cmds.Add(new CmdParameter("@ProjectBalanceNo", period.ProjectBalanceNo));
            cmds.Add(new CmdParameter("@ProjectBalance", period.ProjectBalance));
            cmds.Add(new CmdParameter("@Remark", period.Remark));
            cmds.Add(new CmdParameter("@ndReport", period.ndReport));
            //cmds.Add(new CmdParameter("@needReport", period.needReport));
            //cmds.Add(new CmdParameter("@CustomerId", period.CustomerId));
            cmds.Add(new CmdParameter("@CustomerNo", period.CustomerNo));
            cmds.Add(new CmdParameter("@CustomerName", period.CustomerName));
            cmds.Add(new CmdParameter("@SignDeptNo", period.SignDeptNo));
            cmds.Add(new CmdParameter("@SignDept", period.SignDept));
            cmds.Add(new CmdParameter("@EngnrDeptNo", period.EngnrDeptNo));
            cmds.Add(new CmdParameter("@EngnrDept", period.EngnrDept));
            cmds.Add(new CmdParameter("@EngnrDeptRecord", period.EngnrDeptRecord));
            cmds.Add(new CmdParameter("@DesignDept", period.DesignDept));
            cmds.Add(new CmdParameter("@ResponseDept", period.ResponseDept));
            cmds.Add(new CmdParameter("@PrepDept", period.PrepDept));
            cmds.Add(new CmdParameter("@Scale", period.Scale));
            cmds.Add(new CmdParameter("@PlanScale", period.PlanScale));
            cmds.Add(new CmdParameter("@Probability", period.Probability));
            cmds.Add(new CmdParameter("@SourceProject", period.SourceProject));
            cmds.Add(new CmdParameter("@ProjectStat", period.ProjectStat));
            cmds.Add(new CmdParameter("@ProjectLevel", period.ProjectLevel));
            cmds.Add(new CmdParameter("@Important", period.Important));
            cmds.Add(new CmdParameter("@PrjType", period.PrjType));
            cmds.Add(new CmdParameter("@Region", period.Region));
            cmds.Add(new CmdParameter("@OvTypeNo", period.OvTypeNo));
            cmds.Add(new CmdParameter("@OvType", period.OvType));
            cmds.Add(new CmdParameter("@ProjectTypeNo", period.ProjectTypeNo));
            cmds.Add(new CmdParameter("@ProjectType", period.ProjectType));
            cmds.Add(new CmdParameter("@ProjectType1No", period.ProjectType1No));
            cmds.Add(new CmdParameter("@ProjectType1", period.ProjectType1));
            cmds.Add(new CmdParameter("@isClosed", period.isClosed));
            //cmds.Add(new CmdParameter("@isClosedC", period.isClosedC));
            cmds.Add(new CmdParameter("@CloseReason", period.CloseReason));
            cmds.Add(new CmdParameter("@EditFlag", period.EditFlag));
            cmds.Add(new CmdParameter("@DeptName", period.DeptName));
            cmds.Add(new CmdParameter("@DeptNo", period.DeptNo));
            //cmds.Add(new CmdParameter("@DeptId", period.DeptId));
            cmds.Add(new CmdParameter("@ConnectProject", period.ConnectProject));
            cmds.Add(new CmdParameter("@ApprovalStat", period.ApprovalStat));
            cmds.Add(new CmdParameter("@TenderOpenDate", period.TenderOpenDate));
            //cmds.Add(new CmdParameter("@BidDeptId", period.BidDeptId));
            cmds.Add(new CmdParameter("@TenderOpenYear", period.TenderOpenYear));
            cmds.Add(new CmdParameter("@TenderOpenQuarter", period.TenderOpenQuarter));
            cmds.Add(new CmdParameter("@TenderOpenMonth", period.TenderOpenMonth));
            cmds.Add(new CmdParameter("@CloseType", period.CloseType));
            cmds.Add(new CmdParameter("@FromProjects", period.FromProjects));
            cmds.Add(new CmdParameter("@isShow", period.isShow));
            cmds.Add(new CmdParameter("@ProjectSchedule", period.ProjectSchedule));
            //cmds.Add(new CmdParameter("@CreatedByUserId", period.CreatedByUserId));
            cmds.Add(new CmdParameter("@isOutputValue", period.isOutputValue));
            cmds.Add(new CmdParameter("@isGroupAdmin", period.isGroupAdmin));
            cmds.Add(new CmdParameter("@IsBadDebts", period.IsBadDebts));
            cmds.Add(new CmdParameter("@IsThree", period.IsThree));
            cmds.Add(new CmdParameter("@RowPointer", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", 0, System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@OwnerAttribute", period.OwnerAttribute));
            cmds.Add(new CmdParameter("@PaymentRatio", period.PaymentRatio));
		    cmds.Add(new CmdParameter("@WarrantyRatio", period.WarrantyRatio));
		    cmds.Add(new CmdParameter("@VATRate", period.VATRate));
		    cmds.Add(new CmdParameter("@WarrantyStartingDate", period.WarrantyStartingDate));
		    cmds.Add(new CmdParameter("@WarrantyPeriod", period.WarrantyPeriod));
		    cmds.Add(new CmdParameter("@AcceptanceDate", period.AcceptanceDate));
		    cmds.Add(new CmdParameter("@SettlementDate", period.SettlementDate));
		    cmds.Add(new CmdParameter("@CompletedAccDate", period.CompletedAccDate));
		    cmds.Add(new CmdParameter("@PrjManager", period.PrjManager));
            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_Project].[dbo].[Project_Project_Update]", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            return period;
        }
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
        public XMBProjectInfo Get(string key)
        {

            List<XMBProjectInfo> list = hdDbCmdManager.QueryForList<XMBProjectInfo>(@"SELECT * FROM [ERP_Project].[Project].[Project]  WHERE ProjectNo='" + key + "'", System.Data.CommandType.Text, null);
            if (list != null && list.Count > 0)
            {
                XMBProjectInfo entity = list[0];
                return entity;
            }
            else
                return null;
        }

        /// <summary>
        /// 根据新项目编号来获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public XMBProjectInfo GetByProjectNoNew(string key)
        {

            XMBProjectInfo result = hdDbCmdManager.QueryForFirstRow<XMBProjectInfo>(@"SELECT * FROM [ERP_Project].[Project].[Project]  WHERE ProjectNoNew='" + key + "'", System.Data.CommandType.Text, null);
            if (result == null)
            {
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = "找不到该项目"
                });
            }
            return result;
        }
        #endregion
    }
}
