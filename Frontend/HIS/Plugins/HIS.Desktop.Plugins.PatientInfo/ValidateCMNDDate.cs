using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.PatientInfo
{
    class ValidateCMNDDate : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtCMND;
        internal DevExpress.XtraEditors.TextEdit txtCMNDPlace;
        internal DevExpress.XtraEditors.DateEdit dtDateCMND;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtCMND == null || txtCMNDPlace == null || dtDateCMND == null) return valid;

                if ((!string.IsNullOrEmpty(txtCMND.Text) || !string.IsNullOrEmpty(txtCMNDPlace.Text)) && dtDateCMND.EditValue == null)
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
