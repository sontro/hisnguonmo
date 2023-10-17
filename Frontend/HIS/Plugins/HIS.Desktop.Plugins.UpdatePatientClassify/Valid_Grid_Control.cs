using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.UpdatePatientClassify
{
	internal class Valid_Grid_Control : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.GridLookUpEdit cbo
            ;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (cbo == null)
                    return valid;
                if (cbo.EditValue == null)
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
