using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.LibraryMessage;
namespace HIS.Desktop.Plugins.MedicineTypeCreate.Validtion
{
    class ValidationAgeMonth : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinAgeFrom;
        internal DevExpress.XtraEditors.SpinEdit spinAgeTo;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spinAgeFrom.EditValue != null && spinAgeTo.EditValue != null && spinAgeFrom.Value >spinAgeTo.Value)
                {
                    this.ErrorText = "Khoảng nhập không hợp lệ";
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
