using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionCancel.Validation
{
    class ComboCancelReasonValidation : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.GridLookUpEdit comboBox;
        internal DevExpress.XtraEditors.MemoEdit memoEdit;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if ((comboBox.Text.Trim()=="" || comboBox.EditValue==null )&& memoEdit.Text.Trim()=="")
                {
                    ErrorText = Base.ResourceMessageLang.BanChuaNhapLyDoHuy;
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
