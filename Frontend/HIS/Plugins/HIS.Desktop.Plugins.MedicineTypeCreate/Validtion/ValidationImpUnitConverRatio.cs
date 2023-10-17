using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineTypeCreate.Validtion
{
    class ValidationImpUnitConverRatio : ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinEdit;
        internal DevExpress.XtraEditors.GridLookUpEdit ComboBox;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (ComboBox.EditValue != null)
                {
                    if (spinEdit.EditValue == null)
                    {
                        this.ErrorText = "Trường dữ liệu bắt buộc nhập khi có đơn vị nhập";
                        return valid;
                    }
                    else if (spinEdit.Value <= 0)
                    {
                        this.ErrorText = "Giá trị phải lớn hơn 0";
                        return valid;
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
