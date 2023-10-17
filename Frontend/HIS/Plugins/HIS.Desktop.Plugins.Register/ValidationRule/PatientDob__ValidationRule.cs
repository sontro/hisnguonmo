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
    class PatientDob__ValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.ButtonEdit txtDob;
        internal DevExpress.XtraEditors.DateEdit dtDob;
        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
                bool isChild = CheckIsChild();
                valid = valid && (txtDob != null && dtDob != null);
                if (valid)
                {
                    string strError = "";
                    HIS.Desktop.Plugins.Register.DateUtil.DateValidObject dateValidObject = DateUtil.ValidPatientDob(this.txtDob.Text);
                    valid = !String.IsNullOrEmpty(dateValidObject.OutDate);
                    if (!String.IsNullOrEmpty(dateValidObject.Message))
                    {
                        strError = dateValidObject.Message;
                        valid = false;
                    }

                    this.ErrorText = strError;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }

        bool CheckIsChild()
        {
            bool success = false;
            try
            {
                if (dtDob.EditValue != null && dtDob.DateTime != DateTime.MinValue)
                {
                    DateTime dtNgSinh = dtDob.DateTime;
                    success = MOS.LibraryHein.Bhyt.BhytPatientTypeData.IsChild(dtNgSinh);
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
