using HIS.Desktop.Plugins.ImpMestViewDetail.ImpMestViewDetail.Resources;
using HIS.Desktop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestViewDetail.Validation
{
    class DocumentDateValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.ButtonEdit txtDocumentDate;
        internal DevExpress.XtraEditors.DateEdit dtDocumentDate;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtDocumentDate == null || dtDocumentDate == null) return valid;
                if (!String.IsNullOrEmpty(txtDocumentDate.Text))
                {
                    var dt = DateTimeHelper.ConvertDateStringToSystemDate(txtDocumentDate.Text);
                    if (dt == null || dt.Value == DateTime.MinValue)
                    {
                        ErrorText = ResourceMessage.NguoiDungNhapNgayKhongHopLe;
                        ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                        return valid;
                    }
                    dtDocumentDate.EditValue = dt;
                }

                if (String.IsNullOrEmpty(txtDocumentDate.Text))
                {
                    ErrorText = HIS.Desktop.LibraryMessage.MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
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
