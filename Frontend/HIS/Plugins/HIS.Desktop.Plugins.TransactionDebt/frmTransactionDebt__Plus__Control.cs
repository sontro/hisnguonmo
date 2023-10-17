using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList.Nodes;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.UC.SereServTree;
using Inventec.Common.Adapter;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TransactionDebt
{
    public partial class frmTransactionDebt : HIS.Desktop.Utility.FormBase
    {

        //private void txtAccountBookCode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyCode == Keys.Enter)
        //        {
        //            bool valid = false;
        //            if (!String.IsNullOrEmpty(txtAccountBookCode.Text))
        //            {
        //                var listData = ListAccountBook.Where(o => o.ACCOUNT_BOOK_CODE.Contains(txtAccountBookCode.Text)).ToList();
        //                if (listData != null && listData.Count == 1)
        //                {
        //                    valid = true;
        //                    txtAccountBookCode.Text = listData.First().ACCOUNT_BOOK_CODE;
        //                    cboAccountBook.EditValue = listData.First().ID;
        //                    SetDataToDicNumOrderInAccountBook(listData.First());
        //                    GlobalVariables.DefaultAccountBookDebt = new List<V_HIS_ACCOUNT_BOOK>();
        //                    GlobalVariables.DefaultAccountBookDebt.Add(listData.First());
        //                    dtTransactionTime.Focus();
        //                }
        //                else
        //                {
        //                    spinTongTuDen.Text = "";
        //                }
        //            }
        //            if (!valid)
        //            {
        //                cboAccountBook.Focus();
        //                cboAccountBook.ShowPopup();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //    }
        //}

        private void cboAccountBook_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            try
            {
                if (e.CloseMode == DevExpress.XtraEditors.PopupCloseMode.Normal)
                {
                    if (cboAccountBook.EditValue != null)
                    {
                        var account = ListAccountBook.FirstOrDefault(o => o.ID == Convert.ToInt64(cboAccountBook.EditValue));
                        if (account != null)
                        {
                            GlobalVariables.DefaultAccountBookDebt = new List<V_HIS_ACCOUNT_BOOK>();
                            GlobalVariables.DefaultAccountBookDebt.Add(account);
                            SetDataToDicNumOrderInAccountBook(account);
                        }
                    }
                    else
                    {
                        spinTongTuDen.Text = "";
                    }
                    dtTransactionTime.Focus();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtReason_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{TAB}");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void dtTransactionTime_Closed(object sender, ClosedEventArgs e)
        {
            try
            {
                if (e.CloseMode == PopupCloseMode.Normal)
                {
                    if (layoutTongTuDen.Enabled)
                    {
                        spinTongTuDen.Focus();
                        spinTongTuDen.SelectAll();
                    }
                    //else if (lciTranferAmount.Enabled)
                    //{
                    //    spinTransferAmount.Focus();
                    //    spinTransferAmount.SelectAll();
                    //}
                    //else
                    //{
                    //    txtDiscount.Focus();
                    //    txtDiscount.SelectAll();
                    //}
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtTotalAmount_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                FormatSpint(spinTotalAmount);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
