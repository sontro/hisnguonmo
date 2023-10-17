using HIS.Desktop.Utility;
using HIS.UC.SecondaryIcd.ADO;
using System;
using System.Linq;

namespace HIS.Desktop.Plugins.ExamServiceReqExecute
{
    public partial class ExamServiceReqExecuteControl : UserControlBase
    {
        private void NextForcusSubIcd()
        {
            try
            {
                txtIcdCodeCause.Focus();
                txtIcdCodeCause.SelectAll();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void NextForcusSubIcdCause()
        {
            try
            {
                txtIcdSubCode.Focus();
                //txtIcdText.SelectAll();
                txtIcdSubCode.SelectionStart = txtIcdText.Text.Length;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal void UcSecondaryIcdReadOnly(bool isReadOnly)
        {
            try
            {
                if (isReadOnly)
                {
                    txtIcdSubCode.ReadOnly = true;
                    txtIcdText.ReadOnly = true;
                }
                else
                {
                    txtIcdSubCode.ReadOnly = false;
                    txtIcdText.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadIcdToControlIcdSub(string icdSubCode, string icdText)
        {
            try
            {
                this.txtIcdSubCode.Text = icdSubCode;
                this.txtIcdText.Text = icdText;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }


        private void UcSecondaryIcdFocusComtrol()
        {
            try
            {
                txtIcdSubCode.Focus();
                txtIcdSubCode.SelectAll();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        internal object UcSecondaryIcdGetValue()
        {
            object result = null;
            try
            {
                SecondaryIcdDataADO outPut = new SecondaryIcdDataADO();

                if (!String.IsNullOrEmpty(txtIcdSubCode.Text.Trim()))
                {
                    outPut.ICD_SUB_CODE = txtIcdSubCode.Text.Trim();
                }
                if (!String.IsNullOrEmpty(txtIcdText.Text.Trim()))
                {
                    outPut.ICD_TEXT = txtIcdText.Text.Trim();
                }
                result = outPut;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private void LoadDataToIcdSub()
        {
            try
            {
                this.isNotProcessWhileChangedTextSubIcd = true;
                this.txtIcdSubCode.Text = (!String.IsNullOrEmpty(this.HisServiceReqView.ICD_CODE) || !String.IsNullOrEmpty(this.HisServiceReqView.ICD_SUB_CODE))
                    ? this.HisServiceReqView.ICD_SUB_CODE : treatment != null && !String.IsNullOrEmpty(treatment.ICD_SUB_CODE)
                    ? treatment.ICD_SUB_CODE : null;
                this.txtIcdText.Text = (!String.IsNullOrEmpty(this.HisServiceReqView.ICD_CODE) || !String.IsNullOrEmpty(this.HisServiceReqView.ICD_SUB_CODE))
                    ? this.HisServiceReqView.ICD_TEXT : treatment != null && !String.IsNullOrEmpty(treatment.ICD_TEXT)
                    ? treatment.ICD_TEXT : null;


                string[] codes = this.txtIcdSubCode.Text.Split(IcdUtil.seperator.ToCharArray());
                this.icdSubcodeAdoChecks = (from m in this.currentIcds select new ADO.IcdADO(m, codes)).ToList();

                customGridViewSubIcdName.BeginUpdate();
                customGridViewSubIcdName.GridControl.DataSource = this.icdSubcodeAdoChecks;
                customGridViewSubIcdName.EndUpdate();
                this.isNotProcessWhileChangedTextSubIcd = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

    }
}