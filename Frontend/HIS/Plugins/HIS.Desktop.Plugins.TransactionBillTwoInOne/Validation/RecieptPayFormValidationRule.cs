using HIS.Desktop.Plugins.TransactionBillTwoInOne.ADO;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillTwoInOne.Validation
{
    class RecieptPayFormValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal List<VHisSereServADO> listData = new List<VHisSereServADO>();
        internal DevExpress.XtraEditors.TextEdit txtRecieptPayFormCode;
        internal DevExpress.XtraEditors.TextEdit cboRecieptPayForm;
        internal DevExpress.XtraEditors.CheckEdit checkNotReciept;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtRecieptPayFormCode == null || cboRecieptPayForm == null || checkNotReciept == null) return valid;

                if (listData != null && listData.Count > 0 && !checkNotReciept.Checked)
                {
                    if (String.IsNullOrEmpty(txtRecieptPayFormCode.Text) || cboRecieptPayForm.EditValue == null)
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
