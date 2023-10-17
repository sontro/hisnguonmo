using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.PatientInfo
{
    class ValidateCMNDPlace : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtCMND;
        internal DevExpress.XtraEditors.TextEdit txtCMNDPlace;
        internal DevExpress.XtraEditors.DateEdit dtDateCMND;
        internal int? maxLength;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtCMND == null || txtCMNDPlace == null || dtDateCMND == null) return valid;

                if ((!string.IsNullOrEmpty(txtCMND.Text) || dtDateCMND.EditValue != null) && string.IsNullOrEmpty(txtCMNDPlace.Text))
                    return valid;
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
