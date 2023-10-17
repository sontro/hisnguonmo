using DevExpress.XtraTreeList.Nodes;
using HIS.UC.SereServTree;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.InvoiceCreateForTreatment
{
    public partial class frmInvoiceCreateForTreatment : HIS.Desktop.Utility.FormBase
    {

        private void treeSereServ_CustomDrawNodeCell(SereServADO data, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            try
            {
                if (data != null && !e.Node.HasChildren)
                {
                    if (data.VIR_TOTAL_PATIENT_PRICE.HasValue && data.VIR_TOTAL_PATIENT_PRICE.Value > 0)
                    {
                        //if (data.BILL_ID.HasValue)
                        //    e.Appearance.ForeColor = Color.Blue;
                        //else 
                        if (e.Node.Checked)
                        {
                            e.Appearance.ForeColor = Color.Blue;
                        }
                        else
                        {
                            e.Appearance.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Italic);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void treeSereServ_BeforeCheck(DevExpress.XtraTreeList.Nodes.TreeListNode node)
        {
            try
            {
                if (node != null)
                {
                    if (node.Checked)
                    {
                        node.UncheckAll();
                    }
                    else
                    {
                        node.CheckAll();
                    }
                    while (node.ParentNode != null)
                    {
                        node = node.ParentNode;
                        bool valid = false;
                        foreach (DevExpress.XtraTreeList.Nodes.TreeListNode item in node.Nodes)
                        {
                            if (item.CheckState == CheckState.Checked || item.CheckState == CheckState.Indeterminate)
                            {
                                valid = true;
                                break;
                            }
                        }
                        if (valid)
                        {
                            node.CheckState = CheckState.Checked;
                        }
                        else
                        {
                            node.CheckState = CheckState.Unchecked;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void treeSereServ_AfterCheck(DevExpress.XtraTreeList.Nodes.TreeListNode node, SereServADO data)
        {
            try
            {
                var listData = ssTreeProcessor.GetListCheck(ucSereServTree);
                if (listData != null && listData.Count > 0)
                {
                    this.totalPatientPrice = listData.Sum(s => s.VIR_TOTAL_PATIENT_PRICE ?? 0);
                }
                else
                {
                    this.totalPatientPrice = 0;
                }
                txtAmount.Value = this.totalPatientPrice;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void treeSereServ_CheckAllNode(DevExpress.XtraTreeList.Nodes.TreeListNodes nodes)
        {
            try
            {
                if (nodes != null)
                {
                    foreach (TreeListNode node in nodes)
                    {
                        node.CheckAll();
                        CheckNode(node);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void CheckNode(TreeListNode node)
        {
            try
            {
                if (node != null)
                {
                    foreach (TreeListNode childNode in node.Nodes)
                    {
                        childNode.CheckAll();
                        CheckNode(childNode);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
