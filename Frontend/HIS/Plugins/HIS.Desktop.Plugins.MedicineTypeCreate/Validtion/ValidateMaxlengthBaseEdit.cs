using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;

namespace HIS.Desktop.Plugins.MedicineTypeCreate.Validtion
{
  public  class ValidateMaxLengthBaseEdit : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.BaseEdit baseEdit;
        internal int? maxLength;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (baseEdit == null) return valid;
                if (!String.IsNullOrEmpty(baseEdit.Text) && Inventec.Common.String.CountVi.Count(baseEdit.Text) > maxLength)
                {
                    this.ErrorText = "Trường dữ liệu vượt quá " + maxLength + " ký tự";
                    this.ErrorType = ErrorType.Warning;
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
