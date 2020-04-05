using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using Hondee.Common.DB;
using Hondee.Common.HDException;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using HD.MeteringPayment.Domain.Entity.ContractEntity;


namespace HD.MeteringPayment.DAL.DBBaseInfo
{
    public class DBXMBContractInfo : IXMBContractInfo
    {
        private HdDbCmdManager hdDbCmdManager = HdDbCmdManager.GetInstance();
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        public List<XMBContractInfo> GetList(string whereQuery)
        {
            return hdDbCmdManager.QueryForList<XMBContractInfo>(@"SELECT * FROM [ERP_SalesMarketing].[dbo].[ctrct_prjcontract] " + whereQuery, System.Data.CommandType.Text, null);
        }

        public XMBContractInfo Update(XMBContractInfo entity, string operationBy)
        {
            hdDbCmdManager.ExecuteTran((tran) =>
            {
                InnerUpdate(tran, entity, operationBy);
            });
            return entity;
        }
        

        #region 内部方法
                /// <summary>
        /// 更新
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="period"></param>
        /// <param name="operationBy"></param>
        /// <returns></returns>
        private XMBContractInfo InnerUpdate(SqlTransaction tran, XMBContractInfo XMBContractInfo, string operationBy)
        {
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@ContractNo", XMBContractInfo.ContractNo));
            cmds.Add(new CmdParameter("@CustomerName", XMBContractInfo.CustomerName));
            cmds.Add(new CmdParameter("@Amount", XMBContractInfo.Amount));
            cmds.Add(new CmdParameter("@AftAmount", XMBContractInfo.AftAmount));
            cmds.Add(new CmdParameter("@Currency", XMBContractInfo.Currency));
            cmds.Add(new CmdParameter("@TotalAmount", XMBContractInfo.TotalAmount));
            cmds.Add(new CmdParameter("@SignDate", XMBContractInfo.SignDate));
            cmds.Add(new CmdParameter("@SignDeptNo", XMBContractInfo.SignDeptNo));
            cmds.Add(new CmdParameter("@SignDeptName", XMBContractInfo.SignDeptName));
            cmds.Add(new CmdParameter("@EngnrDeptNo", XMBContractInfo.EngnrDeptNo));
            cmds.Add(new CmdParameter("@EngnrDeptName", XMBContractInfo.EngnrDeptName));
            cmds.Add(new CmdParameter("@ContractCode", XMBContractInfo.ContractCode));
            cmds.Add(new CmdParameter("@ProjectType", XMBContractInfo.ProjectType));
            cmds.Add(new CmdParameter("@Duration", XMBContractInfo.Duration));
            cmds.Add(new CmdParameter("@StartingDate", XMBContractInfo.StartingDate));
            cmds.Add(new CmdParameter("@PlanEndingDate", XMBContractInfo.PlanEndingDate));
            cmds.Add(new CmdParameter("@Leader", XMBContractInfo.Leader));
            cmds.Add(new CmdParameter("@FileLocation", XMBContractInfo.FileLocation));
            cmds.Add(new CmdParameter("@ProjectClass", XMBContractInfo.ProjectClass));
            cmds.Add(new CmdParameter("@ReceiveCntrctFileDate", XMBContractInfo.ReceiveCntrctFileDate));
            cmds.Add(new CmdParameter("@DeptNo", XMBContractInfo.DeptNo));
            cmds.Add(new CmdParameter("@DeptName", XMBContractInfo.DeptName));
            cmds.Add(new CmdParameter("@InnerContractCode", XMBContractInfo.InnerContractCode));
            cmds.Add(new CmdParameter("@isShow", XMBContractInfo.isShow));
            cmds.Add(new CmdParameter("@BidApplicationNo", XMBContractInfo.BidApplicationNo));
            cmds.Add(new CmdParameter("@BidApplicationCode", XMBContractInfo.BidApplicationCode));
            cmds.Add(new CmdParameter("@DomOvsea", XMBContractInfo.DomOvsea));
            cmds.Add(new CmdParameter("@isFocus", XMBContractInfo.isFocus));
            cmds.Add(new CmdParameter("@ProjectType1", XMBContractInfo.ProjectType1));
            cmds.Add(new CmdParameter("@EngnrPhase", XMBContractInfo.EngnrPhase));
            cmds.Add(new CmdParameter("@CompanyNo", XMBContractInfo.CompanyNo));
            cmds.Add(new CmdParameter("@CompanyName", XMBContractInfo.CompanyName));
            cmds.Add(new CmdParameter("@ProjectAddr", XMBContractInfo.ProjectAddr));
            cmds.Add(new CmdParameter("@ContractName", XMBContractInfo.ContractName));
            cmds.Add(new CmdParameter("@EngnrStructure", XMBContractInfo.EngnrStructure));
            cmds.Add(new CmdParameter("@DesignDeptNo", XMBContractInfo.DesignDeptNo));
            cmds.Add(new CmdParameter("@DesignDeptName", XMBContractInfo.DesignDeptName));
            cmds.Add(new CmdParameter("@SupervisionDeptNo", XMBContractInfo.SupervisionDeptNo));
            cmds.Add(new CmdParameter("@SupervisionDeptName", XMBContractInfo.SupervisionDeptName));
            cmds.Add(new CmdParameter("@PrjDeputyManager", XMBContractInfo.PrjDeputyManager));
            cmds.Add(new CmdParameter("@PrjAudit", XMBContractInfo.PrjAudit));
            cmds.Add(new CmdParameter("@AcceptanceDate", XMBContractInfo.AcceptanceDate));
            cmds.Add(new CmdParameter("@CompleteDate", XMBContractInfo.CompleteDate));
            cmds.Add(new CmdParameter("@Continent", XMBContractInfo.Continent));
            cmds.Add(new CmdParameter("@ProjectNo", XMBContractInfo.ProjectNo));
            cmds.Add(new CmdParameter("@Area", XMBContractInfo.Area));
            cmds.Add(new CmdParameter("@Country", XMBContractInfo.Country));
            cmds.Add(new CmdParameter("@Region", XMBContractInfo.Region));
            cmds.Add(new CmdParameter("@Province", XMBContractInfo.Province));
            cmds.Add(new CmdParameter("@City", XMBContractInfo.City));
            cmds.Add(new CmdParameter("@District", XMBContractInfo.District));
            cmds.Add(new CmdParameter("@PrjClassType", XMBContractInfo.PrjClassType));
            cmds.Add(new CmdParameter("@Overview", XMBContractInfo.Overview));
            cmds.Add(new CmdParameter("@PayoutRatio", XMBContractInfo.PayoutRatio));
            cmds.Add(new CmdParameter("@WarrantyPeriod", XMBContractInfo.WarrantyPeriod));
            cmds.Add(new CmdParameter("@ProjectCode", XMBContractInfo.ProjectCode));
            cmds.Add(new CmdParameter("@RetentionPeriod", XMBContractInfo.RetentionPeriod));
            cmds.Add(new CmdParameter("@PaymentDate", XMBContractInfo.PaymentDate));
            cmds.Add(new CmdParameter("@Remark", XMBContractInfo.Remark));
            cmds.Add(new CmdParameter("@ApprovalStat", XMBContractInfo.ApprovalStat));
            cmds.Add(new CmdParameter("@ExecuteStat", XMBContractInfo.ExecuteStat));
            cmds.Add(new CmdParameter("@WfdefId", XMBContractInfo.WfdefId));
            cmds.Add(new CmdParameter("@RefCategory", XMBContractInfo.RefCategory));
            cmds.Add(new CmdParameter("@OperationBy", operationBy));
            cmds.Add(new CmdParameter("@Infor", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@Ok", "", System.Data.ParameterDirection.Output));
            cmds.Add(new CmdParameter("@ProjectName", XMBContractInfo.ProjectName));
            cmds.Add(new CmdParameter("@CustomerNo", XMBContractInfo.CustomerNo));
            cmds.Add(new CmdParameter("@CustomerCode", XMBContractInfo.CustomerCode));
            ParameterResult pResult = new ParameterResult() { Parameters = cmds.ToArray() };
            hdDbCmdManager.Execute("[ERP_SalesMarketing].[dbo].[Ctrct_PrjContract_Update]", CommandType.StoredProcedure, pResult.Parameters, tran);
            if (!Convert.ToBoolean(pResult["@Ok"]))
                throw new FaultException<ApplicationServiceError>(new ApplicationServiceError()
                {
                    ErrorMessage = pResult["@Infor"].ToString()
                });
            return XMBContractInfo;
        }
        
        #endregion
    }
}
