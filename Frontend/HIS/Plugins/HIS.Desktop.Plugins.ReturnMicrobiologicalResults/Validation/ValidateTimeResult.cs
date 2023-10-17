using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ReturnMicrobiologicalResults.Validation
{
    class ValidateTimeResult : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dteKq;
        internal DevExpress.XtraEditors.DateEdit dteLm;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dteKq == null && dteLm == null) return valid;
                if (dteKq.EditValue != null && dteKq.DateTime != DateTime.MinValue && dteLm.EditValue != null && dteLm.DateTime != DateTime.MinValue && Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dteKq.DateTime) < Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dteLm.DateTime))
                {
                    ErrorText = "Thời gian trả kết quả không được nhỏ hơn thời gian lấy mẫu";
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
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
