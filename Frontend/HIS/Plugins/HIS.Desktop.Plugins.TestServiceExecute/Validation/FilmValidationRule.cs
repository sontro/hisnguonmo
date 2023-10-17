using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TestServiceExecute.Validation
{
    class FilmValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtNumberOfFilm;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool vaild = false;
            try
            {
                if (txtNumberOfFilm == null) return vaild;
                if (string.IsNullOrEmpty(txtNumberOfFilm.Text)) return vaild;
                if (txtNumberOfFilm.Text.Length > 15) return vaild;
                vaild = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return vaild;
        }
    }
}
