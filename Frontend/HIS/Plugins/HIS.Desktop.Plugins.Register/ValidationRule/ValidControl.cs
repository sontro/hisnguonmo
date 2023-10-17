using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Register.Valid
{
    class Valid_Province_Control : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtProvince;
        internal DevExpress.XtraEditors.LookUpEdit cboProvince;
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtProvince == null || cboProvince == null)
                    return valid;
                if (string.IsNullOrEmpty(txtProvince.Text) || cboProvince.EditValue == null)
                    return valid;
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }

    class Valid_District_Control : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtDistrict;
        internal DevExpress.XtraEditors.LookUpEdit cboDistrict;
        
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtDistrict == null || cboDistrict == null)
                    return valid;
                if (string.IsNullOrEmpty(txtDistrict.Text) || cboDistrict.EditValue == null)
                    return valid;
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }

    class Valid_Commune_Control : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtCommune;
        internal DevExpress.XtraEditors.LookUpEdit cboCommune;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtCommune == null || cboCommune == null)
                    return valid;
                if (string.IsNullOrEmpty(txtCommune.Text) || cboCommune.EditValue == null)
                    return valid;
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
