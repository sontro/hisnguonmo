using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisBranch.HisBranch
{
    internal class ValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtProvince;        
        internal DevExpress.XtraEditors.TextEdit txtDistricts;        
        internal DevExpress.XtraEditors.TextEdit txtCommune;
        internal DevExpress.XtraEditors.LookUpEdit cboProvince;
        internal DevExpress.XtraEditors.LookUpEdit cboDistricts;
        internal DevExpress.XtraEditors.LookUpEdit cboCommune;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtProvince == null || cboProvince == null) return valid;
                if (String.IsNullOrEmpty(txtProvince.Text) || cboProvince.EditValue == null)
                {
                    ErrorText = Base.ResourceMessageLang.TruongDuLieuBatBuoc;
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }

                if (txtDistricts == null || cboDistricts == null) return valid;
                if (String.IsNullOrEmpty(txtDistricts.Text) || cboDistricts.EditValue == null)
                {
                    ErrorText = Base.ResourceMessageLang.TruongDuLieuBatBuoc;
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }

                if (txtCommune == null || cboCommune == null) return valid;
                if (String.IsNullOrEmpty(txtCommune.Text) || cboCommune.EditValue == null)
                {
                    ErrorText = Base.ResourceMessageLang.TruongDuLieuBatBuoc;
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