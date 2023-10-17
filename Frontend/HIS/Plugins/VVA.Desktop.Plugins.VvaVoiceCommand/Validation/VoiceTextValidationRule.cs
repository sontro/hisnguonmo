using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVA.Desktop.Plugins.VvaVoiceCommand.ADO;

namespace VVA.Desktop.Plugins.VvaVoiceCommand.Validation
{
    class VoiceTextValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraGrid.Views.Grid.GridView gridview;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (gridview .GridControl.DataSource == null) return valid;
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
