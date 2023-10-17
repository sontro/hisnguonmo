using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisBranch.HisBranch
{
    class ValidationWarningText : ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit textEdit;        
        internal DevExpress.XtraEditors.LookUpEdit cbo;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (textEdit == null || cbo == null) return valid;
                if (String.IsNullOrEmpty(textEdit.Text) || cbo.EditValue == null)
                {
                    ErrorText = Base.ResourceMessageLang.TruongDuLieuBatBuoc;
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }
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