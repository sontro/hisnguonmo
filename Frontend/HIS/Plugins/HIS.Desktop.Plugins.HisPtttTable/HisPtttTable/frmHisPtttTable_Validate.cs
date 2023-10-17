using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Desktop.LibraryMessage;
using HIS.Desktop.Plugins.HisPtttTable.Validate;
using Inventec.Common.Logging;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors;
using Inventec.Desktop.Common.Controls.ValidationRule;

namespace HIS.Desktop.Plugins.HisPtttTable.HisPtttTable
{
    public partial class frmHisPtttTable : HIS.Desktop.Utility.FormBase
    {
        private void Validate()
        {
            try
            {
                ValidatetxtDocHoldTypeName();
                ValidatetxtDocHoldTypeCode();
                ValidationControlTextEdit(txtExcuteRoomID);
            }
            catch (Exception ex)
            {

                LogSystem.Warn(ex);
            }
        }
        private void ValidatetxtDocHoldTypeName()
        {
            try
            {
                ValidateMaxLength_TypeName validate = new ValidateMaxLength_TypeName();
                validate.txtcontrol = txtPtttTableName;
                validate.ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                validate.ErrorType = ErrorType.Warning;
                dxValidationProvider1.SetValidationRule(txtPtttTableName, validate);
            }
            catch (Exception ex)
            {

                LogSystem.Warn(ex);
            }
        }
        private void ValidatetxtDocHoldTypeCode()
        {
            try
            {
                ValiDateMaxLength_TypeCode validate = new ValiDateMaxLength_TypeCode();
                validate.txtcontrol = txtPtttTableCode;
                validate.ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                validate.ErrorType = ErrorType.Warning;
                dxValidationProvider1.SetValidationRule(txtPtttTableCode, validate);
            }
            catch (Exception ex)
            {

                LogSystem.Warn(ex);
            }
        }
        private void ValidationControlTextEdit(TextEdit control)
        {
            try
            {
                ControlEditValidationRule validate = new ControlEditValidationRule();
                validate.editor = control;
                validate.ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                validate.ErrorType = ErrorType.Warning;
                dxValidationProvider1.SetValidationRule(control, validate);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }
    }
}
