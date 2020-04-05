using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HD.MeteringPayment.Domain.Entity.ContractBoqEntity;
using DevExpress.XtraTreeList.Nodes;

namespace HD.MeteringPayment.Module.Forms.WBSBoqMng
{
    public partial class frmWBSRelation : DevExpress.XtraEditors.XtraForm
    {
        #region 
        private List<ContractBoi> _lstContractBoi = new List<ContractBoi>();
        public List<ContractBoi> LstContractBoi
        {
            get
            {
                return _lstContractBoi;
            }
            set
            {
                _lstContractBoi = value;
                tlDetail.DataSource = value;
                tlDetail.RefreshDataSource();
                tlDetail.ExpandAll();
            }
        }

        public List<ContractBoi> selectBoi
        {
            get
            {
                List<TreeListNode> nodes = tlDetail.GetAllCheckedNodes();
                if (nodes != null && nodes.Count > 0)
                {
                    List<ContractBoi> result = new List<ContractBoi>();
                    nodes.ForEach(m =>
                        {
                            ContractBoi node = tlDetail.GetDataRecordByNode(m) as ContractBoi;
                            result.Add(node);
                        });
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        public frmWBSRelation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarOK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定关联所选合同清单项？", "确认操作", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定取消？", "确认操作", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
        }

        private void tlDetail_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            if (e.PrevState == CheckState.Unchecked)
            {
                if (e.Node.HasChildren)//如果存在子节点，则不可以选中
                {
                    e.Node.CheckState = CheckState.Unchecked;
                    e.CanCheck = false;
                    return;
                }
                ContractBoi boi = tlDetail.GetDataRecordByNode(e.Node) as ContractBoi;
                if (boi != null)
                {
                    if ((boi.CtrctQty ?? 0) <= 0) //如果未关联数量为0，则不可选中
                    {
                        e.Node.CheckState = CheckState.Unchecked;
                        e.CanCheck = false;
                        return;
                    }
                }
            }
        }
    }
}