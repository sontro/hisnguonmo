using HIS.Desktop.Utility;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ExpMestDepaCreate
{
    public partial class UCExpMestDepaCreate : UserControlBase
    {
        private void cboExpMediStock_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    bool valid = false;
                    if (!String.IsNullOrEmpty(cboExpMediStock.Text))
                    {
                        string key = cboExpMediStock.Text.ToLower();
                        var listData = listExpMediStock.Where(o => o.MEDI_STOCK_CODE.ToLower().Contains(key) || o.MEDI_STOCK_NAME.ToLower().Contains(key)).ToList();
                        if (listData != null && listData.Count == 1)
                        {
                            valid = true;
                            cboExpMediStock.EditValue = listData.First().MEDI_STOCK_ID;
                            WaitingManager.Show();
                            LoadDataToTreeList(listData.First());
                            SetDataGridControlDetail();
                            txtDescription.Focus();
                            txtDescription.SelectAll();
                            WaitingManager.Hide();
                        }
                    }
                    if (!valid)
                    {
                        cboExpMediStock.Focus();
                        cboExpMediStock.ShowPopup();
                    }
                }
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void cboExpMediStock_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            try
            {
                if (e.CloseMode == DevExpress.XtraEditors.PopupCloseMode.Normal)
                {
                    V_HIS_MEST_ROOM mestRoom = null;
                    bool isResetGridDetail = false;
                    if (cboExpMediStock.EditValue != null && cboExpMediStock.EditValue != cboExpMediStock.OldEditValue)
                    {
                        isResetGridDetail = true;
                        mestRoom = listExpMediStock.FirstOrDefault(o => o.MEDI_STOCK_ID == Convert.ToInt64(cboExpMediStock.EditValue));
                    }
                    else if (cboExpMediStock.EditValue == null)
                    {
                        isResetGridDetail = true;
                    }
                    if (isResetGridDetail)
                    {
                        WaitingManager.Show();
                        LoadDataToTreeList(mestRoom);
                        SetDataGridControlDetail();
                        WaitingManager.Hide();
                    }
                    txtDescription.Focus();
                    txtDescription.SelectAll();
                }
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtDescription_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SetFocusMediOrMateStock();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void spinExpAmount_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtNote.Focus();
                    txtNote.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtNote_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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

        private void xtraTabControlMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            try
            {
                SetFocusMediOrMateStock();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
