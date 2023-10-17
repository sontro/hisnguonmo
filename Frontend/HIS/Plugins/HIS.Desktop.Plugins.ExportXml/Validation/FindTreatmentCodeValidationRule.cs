using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportXml.Validation
{
    class FindTreatmentCodeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtFindTreatmentCode;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtFindTreatmentCode == null) return valid;
                //if (!String.IsNullOrEmpty(txtFindTreatmentCode.Text))
                //{
                //    var code = txtFindTreatmentCode.Text.Trim();
                //    long a;
                //    if (!long.TryParse(code, out a))
                //    {
                //        ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.Plugin_Transaction__MaDieuTriPhaiLaCacKyTuSo);
                //        ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                //        return valid;
                //    }
                //}
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
