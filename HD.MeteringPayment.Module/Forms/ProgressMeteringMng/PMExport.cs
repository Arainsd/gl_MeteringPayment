using DevExpress.XtraTreeList;
using Erp.CommonData;
using HD.MeteringPayment.Domain.Entity.ProgressMeteringEntity;
using Hondee.Common.HDException;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Module.Forms.ProgressMeteringMng
{
    public static class PMExport
    {
        public static PMDetailContainer mainHandler = new PMDetailContainer();
        /// <summary>
        /// 导入DataTable到清单中
        /// </summary>
        /// <param name="ImportTable"></param>
        public static void ImportWBS_Details(DataTable ImportTable, Dictionary<String, PrjAmountDetail> dicDetail, Dictionary<String, PrjAmountDetail> dicWBS, int LoginRoleType)
        {
            bool validityFormat = true;
            String strParentCode = null;
            if (!ImportTable.Columns.Contains("名称"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("清单编号"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("合同单价"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("合同数量"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("单位"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("中间交工证书号"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("申报数量"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("申报金额"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("监理数量"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("监理金额"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("业主数量"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("业主金额"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("业主金额"))
            {
                validityFormat = false;
            }
            if (!ImportTable.Columns.Contains("系统编号"))
            {
                validityFormat = false;
            }
            if (!validityFormat)
            {
                throw new BusinessException("文件格式不正确,格式：名称|清单编号|合同单价|合同数量|单位|中间交工证书号|申报数量|申报金额|监理数量|监理金额|业主数量|业主金额|系统编号");
            }

            Dictionary<String, DataRow> dicDetailAndWBS = new Dictionary<string, DataRow>();
            dicDetailAndWBS.Clear();
            for (int i = 0; i < ImportTable.Rows.Count; i++)
            {
                DataRow dr = ImportTable.Rows[i];
                string sysCode = dr["系统编号"].ToString();
                if (!string.IsNullOrEmpty(sysCode) && !dicDetailAndWBS.ContainsKey(sysCode))
                {
                    dicDetailAndWBS.Add(sysCode, dr);
                }
            }
            String strTemp = "";
            decimal decTemp = 0;
            #region 申报
            if (LoginRoleType == 1)
            {
                foreach (String key in dicDetailAndWBS.Keys)
                {
                    if (dicDetail.Keys.Contains(key))   //明细值
                    {
                        decTemp = dicDetail[key].PrjApplyQty ?? 0;
                        if ((decTemp.ToString() != dicDetailAndWBS[key]["申报数量"].ToString() && dicDetailAndWBS[key]["申报数量"].ToString() != ""))
                        {
                            //if (int.Parse((dicDetailAndWBS[key]["申报数量"]).ToString()) + dicDetail[key].StartingApplyQty > dicDetail[key].CtrctQty)
                            //{
                            //    throw new BusinessException("导入失败；累计申报数量不能大于合同数量！请修改导入文件后再尝试。");
                            //}
                            decimal applyQty = ObjectHelper.GetDefaultDecimal(dicDetailAndWBS[key]["申报数量"]);
                            decimal startQty = dicDetail[key].StartingApplyQty.HasValue ? dicDetail[key].StartingApplyQty.Value : 0;
                            decimal ctrctQty = dicDetail[key].CtrctQty.HasValue ? dicDetail[key].CtrctQty.Value : 0;
                            if (applyQty + startQty > ctrctQty)
                            {
                                throw new BusinessException("导入失败；累计申报数量不能大于合同数量！请修改导入文件后再尝试。");
                            }

                            dicDetail[key].PrjApplyQty = applyQty;
                            mainHandler.Inport_CellValueChange(dicDetail[key].WbsSysCode, dicDetail[key].PrjApplyQty.ToString());
                        }
                    }
                    if (dicWBS.Keys.Contains(key))
                    {
                        strTemp = (dicWBS[key].MidCertifiNum) ?? "";
                        if ((strTemp.ToString() != dicDetailAndWBS[key]["中间交工证书号"].ToString()))
                        {
                            dicWBS[key].MidCertifiNum = (dicDetailAndWBS[key]["中间交工证书号"]).ToString();
                            dicWBS[key].isChanged = true;
                        }
                    }
                }
            }
            #endregion 申报
            #region 监理
            else if (LoginRoleType == 2)
            {
                foreach (String key in dicDetailAndWBS.Keys)
                {
                    if (dicDetail.Keys.Contains(key))   //明细值
                    {
                        decTemp = dicDetail[key].SupervisionQty ?? 0;
                        if ((decTemp.ToString() != dicDetailAndWBS[key]["监理数量"].ToString() && dicDetailAndWBS[key]["监理数量"].ToString() != ""))
                        {
                            decimal supervisionQty = ObjectHelper.GetDefaultDecimal(dicDetailAndWBS[key]["监理数量"]);
                            decimal startQty = dicDetail[key].StartingApplyQty.HasValue ? dicDetail[key].StartingSupervisionQty.Value : 0;
                            decimal ctrctQty = dicDetail[key].CtrctQty.HasValue ? dicDetail[key].CtrctQty.Value : 0;
                            if (supervisionQty + startQty > ctrctQty)
                            {
                                throw new BusinessException("导入失败；累计监理数量不能大于合同数量！请修改导入文件后再尝试。");
                            }

                            dicDetail[key].SupervisionQty = supervisionQty;
                            mainHandler.Inport_CellValueChange(dicDetail[key].WbsSysCode, dicDetail[key].SupervisionQty.ToString());
                        }
                    }
                    if (dicWBS.Keys.Contains(key))
                    {
                        strTemp = (dicWBS[key].MidCertifiNum) ?? "";
                        if ((strTemp.ToString() != dicDetailAndWBS[key]["中间交工证书号"].ToString()))
                        {
                            dicWBS[key].MidCertifiNum = (dicDetailAndWBS[key]["中间交工证书号"]).ToString();
                            dicWBS[key].isChanged = true;
                        }
                    }
                }
            }
            #endregion 监理
            #region 业主
            else if (LoginRoleType == 3)
            {
                foreach (String key in dicDetailAndWBS.Keys)
                {
                    if (dicDetail.Keys.Contains(key))
                    {
                        decTemp = dicDetail[key].OwnerQty ?? 0;
                        if ((decTemp.ToString() != dicDetailAndWBS[key]["业主数量"].ToString() && dicDetailAndWBS[key]["业主数量"].ToString() != ""))
                        {
                            decimal ownerty = ObjectHelper.GetDefaultDecimal(dicDetailAndWBS[key]["业主数量"]);
                            decimal startQty = dicDetail[key].StartingApplyQty.HasValue ? dicDetail[key].StartingOwnerQty.Value : 0;
                            decimal ctrctQty = dicDetail[key].CtrctQty.HasValue ? dicDetail[key].CtrctQty.Value : 0;
                            if (ownerty + startQty > ctrctQty)
                            {
                                throw new BusinessException("导入失败；累计业主数量不能大于合同数量！请修改导入文件后再尝试。");
                            }

                            dicDetail[key].OwnerQty = ownerty;
                            mainHandler.Inport_CellValueChange(dicDetail[key].WbsSysCode, dicDetail[key].OwnerQty.ToString());
                        }
                    }
                    if (dicWBS.Keys.Contains(key))
                    {
                        strTemp = (dicWBS[key].MidCertifiNum) ?? "";
                        if ((dicWBS[key].MidCertifiNum.ToString() != dicDetailAndWBS[key]["中间交工证书号"].ToString()))
                        {
                            dicWBS[key].MidCertifiNum = (dicDetailAndWBS[key]["中间交工证书号"]).ToString();
                            dicWBS[key].isChanged = true;
                        }
                    }
                }
            }
            #endregion 业主
        }
    }
}
