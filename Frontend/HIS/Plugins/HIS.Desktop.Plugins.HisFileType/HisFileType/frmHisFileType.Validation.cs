using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.LibraryMessage;
using HIS.Desktop.Plugins.HisFileType.Validation;
using Inventec.Desktop.Common.Controls.ValidationRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisFileType.HisFileType
{
    public partial class frmHisFileType
    {
        private void ValidateForm()
        {
            try
            {
                ValidationControlTextEditFileTypeCode();
                ValidationControlTextEditFileTypeName();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidationControlTextEditFileTypeName()
        {
            try
            {
                //ControlEditValidationRule validRule = new ControlEditValidationRule();
                //validRule.editor = txtName;
                //validRule.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                //validRule.ErrorType = ErrorType.Warning;
                //dxValidationProviderEditorInfo.SetValidationRule(txtName, validRule);

                FileTypeValidationRule validRule = new FileTypeValidationRule();
                validRule.txtControl = this.txtName;
                validRule.maxLength = 100;
                dxValidationProviderEditorInfo.SetValidationRule(txtName, validRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidationControlTextEditFileTypeCode()
        {
            try
            {
                //ControlEditValidationRule validRule = new ControlEditValidationRule();
                //validRule.editor = txtCode;
                //validRule.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                //validRule.ErrorType = ErrorType.Warning;
                //dxValidationProviderEditorInfo.SetValidationRule(txtCode, validRule);

                FileTypeValidationRule validRule = new FileTypeValidationRule();
                validRule.txtControl = this.txtCode;
                validRule.maxLength = 10;
                dxValidationProviderEditorInfo.SetValidationRule(txtCode, validRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
