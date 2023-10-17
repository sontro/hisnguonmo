using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.Plugins.ExamServiceReqExecute.Validate.ValidateRule;
using HIS.Desktop.Utility;
using HIS.UC.Icd.ADO;
using System;
using System.Drawing;
using System.Linq;

namespace HIS.Desktop.Plugins.ExamServiceReqExecute
{
    public partial class ExamServiceReqExecuteControl : UserControlBase
    {
        private void LoadRequiredCause(bool isRequired)
        {
            try
            {
                ValidationICDCause(10, 500, isRequired);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private object UcIcdCauseGetValue()
        {
            object result = null;
            try
            {
                IcdInputADO outPut = new IcdInputADO();
                if (txtIcdCodeCause.ErrorText == "")
                {
                    if (chkEditIcdCause.Checked)
                        outPut.ICD_NAME = txtIcdMainTextCause.Text.Trim();
                    else
                        outPut.ICD_NAME = cboIcdsCause.Text.Trim();

                    if (!String.IsNullOrEmpty(txtIcdCodeCause.Text.Trim()))
                    {
                        outPut.ICD_CODE = txtIcdCodeCause.Text.Trim();
                    }
                }
                else
                    outPut = null;
                result = outPut;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private void ValidationICDCause(int? maxLengthCode, int? maxLengthText, bool isRequired)
        {
            try
            {
                if (isRequired)
                {
                    lciIcdTextCause.AppearanceItemCaption.ForeColor = Color.Maroon;

                    IcdValidationRuleControl icdMainRule = new IcdValidationRuleControl();
                    icdMainRule.txtIcdCode = txtIcdCodeCause;
                    icdMainRule.btnBenhChinh = cboIcdsCause;
                    icdMainRule.txtMainText = txtIcdMainTextCause;
                    icdMainRule.chkCheck = chkEditIcdCause;
                    icdMainRule.maxLengthCode = maxLengthCode;
                    icdMainRule.maxLengthText = maxLengthText;
                    icdMainRule.IsObligatoryTranferMediOrg = this.IsObligatoryTranferMediOrg;
                    icdMainRule.ErrorText = Resources.ResourceMessage.TruongDuLieuBatBuoc;
                    icdMainRule.ErrorType = ErrorType.Warning;
                    dxValidationProviderForLeftPanel.SetValidationRule(txtIcdCodeCause, icdMainRule);
                }
                else
                {
                    lciIcdTextCause.AppearanceItemCaption.ForeColor = new System.Drawing.Color();
                    txtIcdCodeCause.ErrorText = "";
                    dxValidationProviderForLeftPanel.RemoveControlError(txtIcdCodeCause);
                    dxValidationProviderForLeftPanel.SetValidationRule(txtIcdCodeCause, null);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ResetValidationICDCause()
        {
            try
            {
                lciIcdTextCause.AppearanceItemCaption.ForeColor = Color.Black;
                dxValidationProviderForLeftPanel.SetValidationRule(txtIcdCodeCause, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void UCIcdCauseInit()
        {
            try
            {
                DataToComboChuanDoanTD(cboIcdsCause, this.currentIcds.Where(o => o.IS_CAUSE == 1)
                    .OrderBy(p => p.ICD_CODE).ToList());
                chkEditIcdCause.Enabled = (this.autoCheckIcd != 2);
                //txtIcdCode.Focus();
                //txtIcdCode.SelectAll();
                
                gvIcdSubCode.BeginUpdate();
                gvIcdSubCode.GridControl.DataSource = this.currentIcds.ToList();
                gvIcdSubCode.EndUpdate();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadIcdCauseToControl(string icdCode, string icdName)
        {
            try
            {
                if (!string.IsNullOrEmpty(icdCode))
                {
                    var icd = this.currentIcds.Where(p => p.ICD_CODE == (icdCode)).FirstOrDefault();
                    if (icd != null)
                    {
                        txtIcdCodeCause.Text = icd.ICD_CODE;
                        cboIcdsCause.EditValue = icd.ID;
                        if ((autoCheckIcd == 1) || (!String.IsNullOrEmpty(icdName) && (icdName ?? "").Trim().ToLower() != (icd.ICD_NAME ?? "").Trim().ToLower()))
                        {
                            chkEditIcdCause.Checked = (this.autoCheckIcd != 2);
                            txtIcdMainTextCause.Text = icdName;
                        }
                        else
                        {
                            chkEditIcdCause.Checked = false;
                            txtIcdMainTextCause.Text = icd.ICD_NAME;
                        }
                    }
                    else
                    {
                        txtIcdCodeCause.Text = null;
                        cboIcdsCause.EditValue = null;
                        txtIcdMainTextCause.Text = null;
                        chkEditIcdCause.Checked = false;
                    }
                }
                else if (!string.IsNullOrEmpty(icdName))
                {
                    chkEditIcdCause.Checked = (this.autoCheckIcd != 2);
                    txtIcdMainTextCause.Text = icdName;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void UcIcdCauseFocusComtrol()
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
    }
}