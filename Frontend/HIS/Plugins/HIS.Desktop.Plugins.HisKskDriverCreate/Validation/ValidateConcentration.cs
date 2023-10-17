using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisKskDriverCreate.Validation
{
    class ValidateConcentration : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spConcentration;
        internal DevExpress.XtraEditors.CheckEdit chkMgKhi;
        internal DevExpress.XtraEditors.CheckEdit chkMgMau;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spConcentration.EditValue == null && spConcentration.Value <= 0)
                {
                    this.ErrorText = Resources.ResourceMessage.TruongDuLieuBatBuoc;
                    return valid;
                }

                if (spConcentration == null || chkMgKhi == null || chkMgMau == null) return valid;

                if ((spConcentration.EditValue != null && spConcentration.Value >= 0 && (chkMgKhi.Checked || chkMgMau.Checked))
                    || ((spConcentration.EditValue == null && spConcentration.Value <= 0) && !chkMgKhi.Checked && !chkMgKhi.Checked))
                    return true;

                if (spConcentration.EditValue != null && spConcentration.Value >= 0 && (!chkMgKhi.Checked && !chkMgMau.Checked))
                    this.ErrorText = Resources.ResourceMessage.ChuaChonThongTinDonViTinh;

                if ((spConcentration.EditValue == null && spConcentration.Value < 0) && (chkMgKhi.Checked || chkMgMau.Checked))
                    this.ErrorText = Resources.ResourceMessage.ChuaNhapThongTinNongDo;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
