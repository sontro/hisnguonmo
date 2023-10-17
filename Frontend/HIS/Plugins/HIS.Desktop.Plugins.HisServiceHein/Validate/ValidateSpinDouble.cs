using HIS.Desktop.LibraryMessage;
using HIS.Desktop.Plugins.Library.ResourceMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServiceHein
{
    class ValidateSpinDouble : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spin1;
        internal DevExpress.XtraEditors.SpinEdit spin2;
        internal bool IsRequire;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spin1 == null || spin2 == null)
                    return valid;

                if (spin1.EditValue == null && spin2.EditValue == null && IsRequire)
                {
                    this.ErrorText = GetResource.Get(KeyMessage.TruongDuLieuBatBuoc);
                    return valid;
                }

                if (spin1.EditValue != null && spin2.EditValue != null)
                {
                    this.ErrorText = GetResource.Get(KeyMessage.ChiDuocNhapSoTienHoacTiLeTran);
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
