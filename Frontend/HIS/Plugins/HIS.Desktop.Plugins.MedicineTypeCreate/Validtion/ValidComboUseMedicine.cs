using DevExpress.XtraEditors;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.MedicineTypeCreate.Validtion
{
    public class ValidComboUseMedicine : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        public TextEdit txtTextEdit;

        public GridLookUpEdit cbo;

        public override bool Validate(Control control, object value)
        {
            bool result = false;
            try
            {
                if (cbo != null && txtTextEdit != null && (string.IsNullOrEmpty(((Control)(object)txtTextEdit).Text) || cbo.EditValue == null))
                {
                    return result;
                }

                result = true;
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return result;
            }
        }
    }
}
