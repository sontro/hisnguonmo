using System;

namespace HIS.Desktop.Plugins.HisTestIndex
{
    class ValidTextEditMaxLenght : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit textEdit;
        internal long maxlength;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (Inventec.Common.String.CountVi.Count(textEdit.Text) > 500)
                {
                    this.ErrorText = "Nhập quá ký tự cho phép 500 ký tự";
                }
                else
                {
                    this.ErrorText = "";
                    valid = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
