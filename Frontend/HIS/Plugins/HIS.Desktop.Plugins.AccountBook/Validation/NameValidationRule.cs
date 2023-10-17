using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisAccountBookList.Validation
{
    class NameValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtAccountBookName;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool vaild = false;
            try
            {
                if (txtAccountBookName == null) return vaild;
                if (string.IsNullOrEmpty(txtAccountBookName.Text)) return vaild;
                if (!String.IsNullOrEmpty(txtAccountBookName.Text))
                {
                    if (Inventec.Common.String.CountVi.Count(txtAccountBookName.Text) > 100)
                    {
                        this.ErrorText = "Độ dài tên vượt quá " + 100 + " ký tự.";
                        return vaild;
                    }
                }

                vaild = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return vaild;
        }
    }
}
