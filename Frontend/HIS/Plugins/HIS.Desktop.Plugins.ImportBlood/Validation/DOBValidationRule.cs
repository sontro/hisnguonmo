using HIS.Desktop.Plugins.ImportBlood.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ImportBlood.Validation
{
    class DOBValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {

        internal DevExpress.XtraEditors.DateEdit dtPatientDob;
        internal DevExpress.XtraEditors.ButtonEdit txtPatientDob;
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtPatientDob == null || dtPatientDob == null)
                    return valid;
                if (string.IsNullOrEmpty(txtPatientDob.Text) || dtPatientDob.EditValue == null)
                    return valid;
                if (dtPatientDob.DateTime == DateTime.MinValue)
                {
                    this.ErrorText = ResourceMessageLang.NhapNgaySinhKhongDungDinhDang;
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
