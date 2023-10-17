using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMR.Desktop.Plugins.EmrFlow.Validate
{
    class ValidateSpinNull : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit txtControl;
        public override bool Validate(Control control, object value)
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
