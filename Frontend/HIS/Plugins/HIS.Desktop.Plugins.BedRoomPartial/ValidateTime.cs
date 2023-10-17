using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HIS.Desktop.Plugins.BedRoomPartial
{
    internal class ValidateTime : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit txt;

        internal DevExpress.XtraEditors.DateEdit txtTo;
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {

                if (txt == null || txt.DateTime > txtTo.DateTime) return valid;
                if (String.IsNullOrEmpty(txt.Text) || txt.EditValue == null)
                    return valid;
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
