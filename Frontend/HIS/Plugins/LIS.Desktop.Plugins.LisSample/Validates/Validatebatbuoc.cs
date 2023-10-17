using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LIS.Desktop.Plugins.LisSample.Validates
{
    class Validatebatbuoc: DevExpress.XtraEditors.DXErrorProvider.ValidationRule

    {
        internal DevExpress.XtraEditors.GridLookUpEdit txtControl;
        

        public override bool Validate(Control control, object value)
        {
            bool succes = true;
            try
            {
                string strError = "";
                if (txtControl.EditValue == null)
                {
                    succes = false;
                    strError = " Trường dữ liệu bắt buộc ";

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
