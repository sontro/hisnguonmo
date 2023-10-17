using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisAccountBookList.Validation
{
    class TemplateValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtTemplateCode;
        internal DevExpress.XtraEditors.TextEdit txtSymbolCode;
        internal DevExpress.XtraEditors.GridLookUpEdit cboEInvoiceSys;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtTemplateCode != null && txtSymbolCode != null && cboEInvoiceSys != null)
                {
                    if (!String.IsNullOrEmpty(txtTemplateCode.Text))
                    {
                        if (Inventec.Common.String.CountVi.Count(txtTemplateCode.Text) > 11)
                        {
                            this.ErrorText = "Độ dài ký hiệu vượt quá " + 11 + " ký tự.";
                            return valid;
                        }
                    }
                    if (String.IsNullOrEmpty(txtTemplateCode.Text) && !String.IsNullOrEmpty(txtSymbolCode.Text)) return valid;

                    if (cboEInvoiceSys.EditValue != null && String.IsNullOrEmpty(txtTemplateCode.Text)
                        && Inventec.Common.TypeConvert.Parse.ToInt64(cboEInvoiceSys.EditValue.ToString()) != IMSys.DbConfig.HIS_RS.HIS_EINVOICE_TYPE.ID__BKAV) return valid;
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
