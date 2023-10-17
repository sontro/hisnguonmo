using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TreatmentFinish.Validation
{
    class ValidateWorkPlace : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtWorkPlace;
        internal DevExpress.XtraEditors.GridLookUpEdit cbbWorkPlace;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (!String.IsNullOrEmpty(txtWorkPlace.Text) || cbbWorkPlace.EditValue != null)
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
