using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PeriodDepartmentList.Validation
{
    class FromExamClinicalAmountValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit txtFromExamClinicalAmount;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtFromExamClinicalAmount == null) return valid;

                var amount = Convert.ToInt64(txtFromExamClinicalAmount.Text);
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
