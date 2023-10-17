using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PeriodDepartmentList.Validation
{
    class EndTreatmentAmountValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit txtEndTreatmentAmount;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtEndTreatmentAmount == null) return valid;

                var amount = Convert.ToInt64(txtEndTreatmentAmount.Text);
                if (amount < 0)
                {
                    ErrorText = MessageUtil.GetMessage(Message.Enum.SoLuongKhongDuocBeHonKhong);
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
