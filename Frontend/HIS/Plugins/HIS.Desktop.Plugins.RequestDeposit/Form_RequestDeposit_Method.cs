using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Core;
using MOS.Filter;
using Inventec.Common.Adapter;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.LocalStorage.LocalData;

namespace HIS.Desktop.Plugins.RequestDeposit
{
    public partial class Form_RequestDeposit : HIS.Desktop.Utility.FormBase
    {
        private void getDataDepositReq(long treatmentid)
        {
            try
            {
                CommonParam param = new CommonParam();
                HisDepositReqViewFilter filter = new HisDepositReqViewFilter();
                SetFilter(ref filter);
                filter.TREATMENT_ID = treatmentid;
                filter.ORDER_FIELD = "MODIFY_TIME";
                filter.ORDER_DIRECTION = "DESC";
                listDepositReq = new BackendAdapter(param).Get<List<V_HIS_DEPOSIT_REQ>>(HisRequestUriStore.HIS_DEPOSIT_REQ_GETVIEW, ApiConsumer.ApiConsumers.MosConsumer, filter, null);

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void SetFilter(ref HisDepositReqViewFilter filter)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDepositReqCode.Text))
                {
                    string code = txtDepositReqCode.Text.Trim();
                    if (code.Length < 8 && checkDigit(code))
                    {
                        code = string.Format("{0:00000000}", Convert.ToInt64(code));
                        txtDepositReqCode.Text = code;
                    }
                    filter.DEPOSIT_REQ_CODE__EXACT = txtDepositReqCode.Text;
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private bool checkDigit(string s)
        {
            bool result = false;
            try
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (char.IsDigit(s[i]) == true) result = true;
                    else return false;
                }
                return result;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return result;
            }
        }

        private void FillDataToGridDepositReq()
        {
            try
            {
                this.listDepositReqProcessor.Reload(this.ucRequestDeposit, listDepositReq);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        /*
                private void getDataSelectRow()
                {
                    try
                    {
                        currentdepositReq = null;  
                        var result = (V_HIS_DEPOSIT_REQ)this.listDepositReqProcessor.GetSelectRow(ucRequestDeposit);
                        if (result != null)
                        {
                            currentdepositReq = result;
                            txtAmount.Text = currentdepositReq.AMOUNT.ToString();
                            txtGhiChu.Text = currentdepositReq.DESCRIPTION;
                        }
                        else
                        {
                            currentdepositReq = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(ex);
                    }
                }
        */

        private void RemoveValidate()
        {
            dxValidationProvider1.RemoveControlError(spinEditPrice);
        }
        private void SetDefaultValue()
        {
            try
            {
                txtGhiChu.Text = null;
                spinEditPrice.EditValue = null;
                this.action = GlobalVariables.ActionAdd;
                EnableControlChanged(action);
                RemoveValidate();
                spinEditPrice.Focus();
                spinEditPrice.SelectAll();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSession.Warn(ex);
            }

        }

        private void EnableControlChanged(int action)
        {
            try
            {
                btnEdit.Enabled = (action == GlobalVariables.ActionEdit);
                btnAdd.Enabled = (action == GlobalVariables.ActionAdd);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

    }
}
