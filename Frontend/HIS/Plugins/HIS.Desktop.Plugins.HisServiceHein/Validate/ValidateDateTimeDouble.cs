using HIS.Desktop.LibraryMessage;
using HIS.Desktop.Plugins.Library.ResourceMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServiceHein
{
    class ValidateDateTimeDouble : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dateFrom;
        internal DevExpress.XtraEditors.DateEdit dateTo;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dateFrom == null || dateTo == null)
                    return valid;

                if (dateFrom.EditValue != null && dateTo.EditValue != null && dateFrom.DateTime > dateTo.DateTime)
                {
                    this.ErrorText = GetResource.Get(KeyMessage.ThoiGianTuKhongDuocLonHonThoiGianDen);
                    return valid;
                }

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
