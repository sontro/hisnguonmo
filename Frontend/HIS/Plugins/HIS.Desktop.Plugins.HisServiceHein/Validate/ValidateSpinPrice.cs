using HIS.Desktop.LibraryMessage;
using HIS.Desktop.Plugins.Library.ResourceMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServiceHein
{
    class ValidateSpinPrice : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spin;
        internal DevExpress.XtraEditors.SpinEdit spinVat;
        internal bool IsRequire;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spin.EditValue == null && spinVat.EditValue == null)
                {
                    this.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    return valid;
                }

                if (spin.EditValue != null && spinVat.EditValue != null)
                {
                    this.ErrorText = GetResource.Get(KeyMessage.ChiDuocNhapSoTienHoacTiLeTran);
                    return valid;
                }

                if (spin.Value < 0 && spin.EditValue != null)
                {
                    this.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuKhongNhanGiaTriAm);
                    return valid;
                }
                if (spin.Value > 999999999999999999)
                {
                    this.ErrorText = "Vượt quá giá trị cho phép";
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
