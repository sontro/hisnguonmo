using DevExpress.XtraEditors.Controls;
using HIS.Desktop.Utility;
using Inventec.Desktop.Common.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Register.ValidationRule
{
    class Career__ValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtCareerCode;
        internal DevExpress.XtraEditors.LookUpEdit cboCareer;
        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
                valid = valid && (txtCareerCode != null && cboCareer != null);
                if (valid)
                {
                    string provinceCode = txtCareerCode.Text.Trim();
                    if (String.IsNullOrEmpty(provinceCode) || cboCareer.EditValue == null || (long)cboCareer.EditValue == 0)
                    {
                        valid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}