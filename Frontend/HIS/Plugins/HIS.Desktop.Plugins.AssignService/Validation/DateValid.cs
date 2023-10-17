using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.DXErrorProvider;

namespace HIS.Desktop.Plugins.AssignService.Validation
{
	internal class DateValid : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
	{
        internal DevExpress.XtraEditors.DateEdit dte1;
        internal DevExpress.XtraEditors.DateEdit dte2;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dte1.EditValue !=null && dte1.DateTime != DateTime.MinValue && dte2.EditValue !=null && dte2.DateTime != DateTime.MinValue && dte1.DateTime > dte2.DateTime)
                {
                    ErrorText = "Thời gian bắt đầu không được lớn hơn thời gian kết thúc";
                    ErrorType = ErrorType.Warning;
                    return valid;
                }
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
