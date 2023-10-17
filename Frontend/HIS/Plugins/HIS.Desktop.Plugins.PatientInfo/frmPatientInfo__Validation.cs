using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.ViewInfo;
using HIS.Desktop.LibraryMessage;
using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Desktop.Common.Controls.ValidationRule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.PatientInfo
{
    public partial class frmPatientInfo : HIS.Desktop.Utility.FormBase
    {
        private void ValidateForm(bool isChild)
        {
            try
            {
                ValidTxtSoThe();
                ValidationSingleControl(txtPatientName);
                ValidationLookUpEditWithTextEdit(cboGender1, txtGender);
                //ValidationSingleControl(dtDOB);
                ValidationSingleControl(txtPatientDOB);
                ValidationSingleText(txtPatientDOB);
                if (isChild)
                {
                    ValidationSingleText(txtCMNDRelative);
                    ValidationSingleControl(txtCMNDRelative);
                }
                ValidateCMNDNumber(txtCMND, dtNgayCap, txtNoiCap);
                ValidateCMNDPlace(txtCMND, dtNgayCap, txtNoiCap);
                ValidateCMNDDate(txtCMND, dtNgayCap, txtNoiCap);

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidateCMNDNumber(TextEdit txtCMND, DateEdit dtDateCMND, TextEdit txtPlaceCMND)
        {
            try
            {
                ValidateCMNDNumber valid = new ValidateCMNDNumber();
                valid.txtCMND = txtCMND;
                valid.txtCMNDPlace = txtPlaceCMND;
                valid.dtDateCMND = dtDateCMND;
                valid.ErrorType = ErrorType.Warning;
                valid.ErrorText = "Thiếu thông tin CMND/CCCD";
                this.dxValidationProvider1.SetValidationRule(txtCMND, valid);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }

        private void ValidateCMNDPlace(TextEdit txtCMND, DateEdit dtDateCMND, TextEdit txtPlaceCMND)
        {
            try
            {
                ValidateCMNDPlace valid = new ValidateCMNDPlace();
                valid.txtCMND = txtCMND;
                valid.txtCMNDPlace = txtPlaceCMND;
                valid.dtDateCMND = dtDateCMND;
                valid.ErrorType = ErrorType.Warning;
                valid.ErrorText = "Thiếu thông tin CMND/CCCD";
                this.dxValidationProvider1.SetValidationRule(txtPlaceCMND, valid);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }

        private void ValidateCMNDDate(TextEdit txtCMND, DateEdit dtDateCMND, TextEdit txtPlaceCMND)
        {
            try
            {
                ValidateCMNDDate valid = new ValidateCMNDDate();
                valid.txtCMND = txtCMND;
                valid.txtCMNDPlace = txtPlaceCMND;
                valid.dtDateCMND = dtDateCMND;
                valid.ErrorType = ErrorType.Warning;
                valid.ErrorText = "Thiếu thông tin CMND/CCCD";
                this.dxValidationProvider1.SetValidationRule(dtDateCMND, valid);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }

        private void ValidTxtSoThe()
        {
            try
            {
                //ValidationBHYTNumber oDobDateRule = new ValidationBHYTNumber();
                //oDobDateRule.txtSoThe = this.txtSoTheBHYT;
                //oDobDateRule.BhytBlackLists = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_BHYT_BLACKLIST>();
                //oDobDateRule.BhytWhiteLists = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_BHYT_WHITELIST>();
                //oDobDateRule.ErrorType = ErrorType.Warning;
                //this.dxValidationProvider1.SetValidationRule(this.txtSoTheBHYT, oDobDateRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidateLookupWithTextEdit(LookUpEdit cbo, TextEdit textEdit)
        {
            try
            {
                LookupEditWithTextEditValidationRule validRule = new LookupEditWithTextEditValidationRule();
                validRule.txtTextEdit = textEdit;
                validRule.cbo = cbo;
                validRule.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                validRule.ErrorType = ErrorType.Warning;
                dxValidationProvider1.SetValidationRule(textEdit, validRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void ValidateGridLookupWithTextEdit(GridLookUpEdit cbo, TextEdit textEdit)
        {
            try
            {
                GridLookupEditWithTextEditValidationRule validRule = new GridLookupEditWithTextEditValidationRule();
                validRule.txtTextEdit = textEdit;
                validRule.cbo = cbo;
                validRule.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                validRule.ErrorType = ErrorType.Warning;
                dxValidationProvider1.SetValidationRule(textEdit, validRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void ValidationSingleText(TextEdit text)
        {
            try
            {
                txtDOBValidate validRule = new txtDOBValidate();
                validRule.txtdob = text;
                validRule.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.NguoiDungNhapNgaySinhKhongHopLe);
                validRule.ErrorType = ErrorType.Warning;
                dxValidationProvider1.SetValidationRule(text, validRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void ValidationSingleControl(BaseEdit control)
        {
            try
            {
                ControlEditValidationRule validRule = new ControlEditValidationRule();
                validRule.editor = control;
                validRule.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                validRule.ErrorType = ErrorType.Warning;
                dxValidationProvider1.SetValidationRule(control, validRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidationLookUpEditWithTextEdit(LookUpEdit control, TextEdit txt)
        {
            try
            {
                Inventec.Desktop.Common.Controls.ValidationRule.LookupEditWithTextEditValidationRule validRule = new Inventec.Desktop.Common.Controls.ValidationRule.LookupEditWithTextEditValidationRule();
                validRule.cbo = control;
                validRule.txtTextEdit = txt;
                validRule.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                validRule.ErrorType = ErrorType.Warning;
                dxValidationProvider1.SetValidationRule(txt, validRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void dxValidationProvider1_ValidationFailed(object sender, DevExpress.XtraEditors.DXErrorProvider.ValidationFailedEventArgs e)
        {
            try
            {
                BaseEdit edit = e.InvalidControl as BaseEdit;
                if (edit == null)
                    return;

                BaseEditViewInfo viewInfo = edit.GetViewInfo() as BaseEditViewInfo;
                if (viewInfo == null)
                    return;

                if (positionHandleControlPatientInfo == -1)
                {
                    positionHandleControlPatientInfo = edit.TabIndex;
                    if (edit.Visible)
                    {
                        edit.SelectAll();
                        edit.Focus();
                    }
                }
                if (positionHandleControlPatientInfo > edit.TabIndex)
                {
                    positionHandleControlPatientInfo = edit.TabIndex;
                    if (edit.Visible)
                    {
                        edit.SelectAll();
                        edit.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
