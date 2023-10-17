using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.Validation
{
    class DocumentValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtDocument;
        internal DevExpress.XtraEditors.LookUpEdit cboImpMestType;
        internal bool keyCheck;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtDocument == null) return true;
                if (cboImpMestType.EditValue != null
                    && Inventec.Common.TypeConvert.Parse.ToInt64(cboImpMestType.EditValue.ToString() ?? "0") == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__NCC
                    && keyCheck && string.IsNullOrEmpty(txtDocument.Text)) return valid;

                if (!string.IsNullOrEmpty(txtDocument.Text) && Inventec.Common.String.CheckString.IsOverMaxLengthUTF8(txtDocument.Text, 50))
                {
                    this.ErrorText = "Trường dữ liệu vượt quá maxlength( " + 50 + " kí tự)";
                    return valid;
                }

                valid = true;
            }
            catch (Exception ex)
            {
                valid = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
