using HIS.Desktop.Plugins.TransactionBillTwoInOne.ADO;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillTwoInOne.Validation
{
    class RecieptAccountBookValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal List<VHisSereServADO> listData = new List<VHisSereServADO>();
        internal DevExpress.XtraEditors.TextEdit txtRecieptAccountBookCode;
        internal DevExpress.XtraEditors.TextEdit cboRecieptAccountBook;
        internal DevExpress.XtraEditors.CheckEdit checNotkReciept;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtRecieptAccountBookCode == null || cboRecieptAccountBook == null || checNotkReciept == null) return valid;
                if (listData != null && listData.Count > 0 && !checNotkReciept.Checked)
                {
                    if (String.IsNullOrEmpty(txtRecieptAccountBookCode.Text) || cboRecieptAccountBook.EditValue == null)
                    {
                        ErrorText = Base.ResourceMessageLang.TruongDuLieuBatBuoc;
                        ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                        return valid;
                    }
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
