using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.LisSampleUpdate.Validation
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

    class TextEditMaxLengthValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtEdit;
        internal int maxlength;
        internal bool isVali;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool vaild = false;
            try
            {
                if (isVali && (txtEdit == null || string.IsNullOrEmpty(txtEdit.Text)))
                {
                    this.ErrorText = HIS.Desktop.LibraryMessage.MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    return vaild;
                }
                if (maxlength > 0 && txtEdit != null && !string.IsNullOrEmpty(txtEdit.Text) && Inventec.Common.String.CheckString.IsOverMaxLengthUTF8(txtEdit.Text, maxlength))
                {
                    this.ErrorText = "Trường dữ liệu vượt quá maxlength( " + maxlength + " kí tự)";
                    return vaild;
                }
                vaild = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return vaild;
        }
    }
}
