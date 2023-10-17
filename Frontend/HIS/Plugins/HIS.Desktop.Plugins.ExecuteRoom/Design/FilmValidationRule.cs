using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExecuteRoom.Design
{
    class FilmValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtNumberOfFilm;
        internal bool isCheck;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool vaild = false;
            try
            {
                if (txtNumberOfFilm == null) return vaild;
                if (isCheck && string.IsNullOrEmpty(txtNumberOfFilm.Text)) return vaild;
                if (!string.IsNullOrEmpty(txtNumberOfFilm.Text) && txtNumberOfFilm.Text.Length > 15)
                {
                    this.ErrorText = "Trường dữ liệu vướt quá max lenght (15 ký tự)";
                    return vaild;
                }
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
