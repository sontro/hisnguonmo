using HIS.Desktop.Plugins.ConfirmPresBlood.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ConfirmPresBlood.Validation
{
    class ExpiredDateValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dtExpiredDate;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dtExpiredDate == null) return valid;
                if (dtExpiredDate.EditValue == null || dtExpiredDate.DateTime == DateTime.MinValue)
                {
                    ErrorText = ResourceMessage.TruongDuLieuBatBuoc;
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }

                if (dtExpiredDate.DateTime <= DateTime.Now)
                {
                    ErrorText = ResourceMessage.HanDungKhongDuocBeHonHienTai;
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
