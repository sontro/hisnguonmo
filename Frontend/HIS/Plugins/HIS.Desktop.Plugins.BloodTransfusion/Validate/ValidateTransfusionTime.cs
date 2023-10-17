using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BloodTransfusion.Validate
{
    class ValidateTransfusionTime : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dateEditFrom;
        internal DevExpress.XtraEditors.DateEdit dateEditTo;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dateEditFrom.EditValue != null && dateEditTo.EditValue != null)
                {
                    if (Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dateEditFrom.DateTime) > Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dateEditTo.DateTime))
                    {
                        this.ErrorText = "Thời gian bắt đầu truyền phải lớn hơn thời gian kết thúc truyền";
                        return valid;
                    }
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
