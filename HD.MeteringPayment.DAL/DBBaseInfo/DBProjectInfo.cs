using HD.MeteringPayment.Domain.Entity.BaseInfoEntity;
using Hondee.Common.DataConvertor;
using Hondee.Common.DB;
using Hondee.Common.HDException;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace HD.MeteringPayment.DAL.DBBaseInfo
{
    public class DBProjectInfo : IProjectInfo
    {
        private HdDbCmdManager hdDbCmdManager = HdDbCmdManager.GetInstance();
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        public List<ProjectInfo> GetList(string glBidRootNo)
        {
            string sql = @"WITH BidTree AS --递归查询所有广联标段树下面的项目
                           (
                               SELECT *
                                 from ERP_Project.dbo.dtl_gl_projectorg b
                                where BidNo = @GlBidRootNo
                                UNION ALL
                               SELECT ERP_Project.dbo.dtl_gl_projectorg.*
                                 from BidTree
                                 JOIN ERP_Project.dbo.dtl_gl_projectorg on BidTree.BidNo = ERP_Project.dbo.dtl_gl_projectorg.ParentNo
                           )
                           SELECT P.*,B.BidName,B.Category,B.BidCode
                             FROM [ERP_Project].[Project].[Project] P 
                             LEFT OUTER JOIN ERP_Project.dbo.dtl_gl_projectorg B ON P.ProjectNo = B.ProjectNo
                            WHERE P.ProjectNo IN(SELECT DISTINCT ProjectNo FROM BidTree WHERE ISNULL(ProjectNo, '') <> '')
                            ORDER BY B.BidCode";
            List<CmdParameter> cmds = new List<CmdParameter>();
            cmds.Add(new CmdParameter("@GlBidRootNo", glBidRootNo));
            return hdDbCmdManager.QueryForList<ProjectInfo>(sql, System.Data.CommandType.Text, cmds.ToArray());
        }

        /// <summary>
        /// 获取项目的币种和币种汇率局控状态
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public ProjectInfo GetCurrencyAndExchangeRate(ProjectInfo project)
        {
            return hdDbCmdManager.QueryForFirstRow<ProjectInfo>(String.Format(@"SELECT A.ProjectNo
	                                                                                  ,A.ProjectName
                                                                                      ,A.CtrctCurrencyCode AS CurrencyCode
	                                                                                  ,B.Currency
	                                                                                  ,CASE WHEN b.Currency = '人民币' THEN CAST(1 AS BIT) ELSE B.Fixed END AS Fixed
                                                                                  FROM ERP_Project.Project.Project A
                                                                                  LEFT OUTER JOIN ERP_Basedata.Finance.Currency B ON A.CtrctCurrencyCode = B.CurrencyCode
                                                                                 WHERE A.ProjectNo = '{0}'", project.ProjectNo), System.Data.CommandType.Text, null);
        }


    }
}
