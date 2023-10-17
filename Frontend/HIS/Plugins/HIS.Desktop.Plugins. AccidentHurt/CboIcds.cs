using HIS.Desktop.Plugins.AccidentHurt.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.AccidentHurt
{
    class CboIcds : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtTextEdit;
        internal DevExpress.XtraEditors.TextEdit txtTextEditPhu;
        internal DevExpress.XtraEditors.GridLookUpEdit cbo;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtTextEdit == null || cbo == null) return valid;
                //if (String.IsNullOrEmpty(txtTextEdit.Text) || cbo.EditValue == null)
                //{
                //    ErrorText = String.Format(ResourceMessage.TruongDuLieuBatBuocPhaiNhap);
                //    return valid;
                //}
                if (!string.IsNullOrEmpty(txtTextEditPhu.Text) && !string.IsNullOrEmpty(txtTextEdit.Text))
                {
                    

                    foreach (var item in txtTextEditPhu.Text.Split(';'))
                    {
                        if (txtTextEdit.Text == item)
                        {
                            ErrorText = "Mã bệnh phụ " + txtTextEdit.Text + " đã được sử dụng cho mã bệnh chính, đề nghị kiểm tra lại.";
                            return valid;
                        }
                    }
                    
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
