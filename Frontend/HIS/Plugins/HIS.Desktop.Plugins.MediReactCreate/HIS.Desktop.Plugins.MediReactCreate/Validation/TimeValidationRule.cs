using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HIS.Desktop.Plugins.MediReactCreate.Validation
{
    class TimeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
     internal DevExpress.XtraEditors.DateEdit DateEdit1;
     internal DevExpress.XtraEditors.DateEdit DateEdit2;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
             if (DateEdit1 == null) return valid;
             if (DateEdit2 == null) return valid;
             if (DateEdit1.EditValue == null || DateEdit2.EditValue == null || Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateEdit2.DateTime)
 < Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateEdit1.DateTime))
                {
                    ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.NguoiDungNhapNgayKhongHopLe);
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
