using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.InfantInformation.Validate
{
    class CboHisBirthSertBookValidationRule : ValidationRule
    {
        internal DevExpress.XtraEditors.GridLookUpEdit gridlookup;
        public CboHisBirthSertBookValidationRule()
        {
        }
        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
                if (String.IsNullOrEmpty(gridlookup.Text)) //return true;
                valid = false;

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
