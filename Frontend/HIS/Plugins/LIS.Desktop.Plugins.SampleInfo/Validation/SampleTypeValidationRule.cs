using ACS.EFMODEL.DataModels;
using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.LibraryMessage;
using HIS.Desktop.LocalStorage.BackendData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Desktop.Plugins.SampleInfo.Validation
{
    class SampleTypeValidationRule : ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtSampleTypeCode;
        internal DevExpress.XtraEditors.GridLookUpEdit cboSampleType;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (cboSampleType == null || txtSampleTypeCode == null) return valid;
                if (String.IsNullOrWhiteSpace(txtSampleTypeCode.Text) || cboSampleType.EditValue == null)
                {
                    ErrorText = MessageUtil.GetMessage(Message.Enum.TruongDuLieuBatBuoc);
                    ErrorType = ErrorType.Warning;
                    return valid;
                }
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
