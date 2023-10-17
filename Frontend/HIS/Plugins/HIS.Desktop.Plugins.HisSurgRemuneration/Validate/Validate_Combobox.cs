using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisSurgRemuneration.Validate
{
    class Validate_Combobox:DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.GridLookUpEdit txtControl;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool succes = true;
            try
            {
                string strError = "";

                if (txtControl.EditValue == null)
                {
                    succes = false;
                    strError = "Trường dữ liệu bắt buộc";
                }
                this.ErrorText = strError;

            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Error(ex);

            }
            return succes;
        }
    }
}
