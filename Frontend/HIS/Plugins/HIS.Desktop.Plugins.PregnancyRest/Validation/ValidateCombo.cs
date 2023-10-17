using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.DXErrorProvider;

namespace HIS.Desktop.Plugins.PregnancyRest.Validation
{
    class ValidateCombo : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtControl;
        internal DevExpress.XtraEditors.GridLookUpEdit cbo;
        internal bool CheckAll;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool success = false;
            try
            {
                if (CheckAll)
                {
                    if (!String.IsNullOrEmpty(txtControl.Text) && cbo.EditValue != null)
                        success = true;
                }
                else
                {
                    if (!String.IsNullOrEmpty(txtControl.Text) || cbo.EditValue != null)
                        success = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return success;
        }
    }
}
