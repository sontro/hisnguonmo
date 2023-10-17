using ACS.EFMODEL.DataModels;
using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.LibraryMessage;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.Plugins.ServiceReqUpdateInstruction.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqUpdateInstruction.Validation
{
    class RequestUserValidationRule : ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtLoginname;
        internal DevExpress.XtraEditors.GridLookUpEdit cboUser;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (cboUser == null || txtLoginname == null) return valid;
                if (cboUser.Enabled && (String.IsNullOrWhiteSpace(txtLoginname.Text) || cboUser.EditValue == null))
                {
                    ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    ErrorType = ErrorType.Warning;
                    return valid;
                }
                if (cboUser.EditValue != null)
                {
                    ACS_USER user = BackendDataWorker.Get<ACS_USER>().FirstOrDefault(o => o.IS_ACTIVE == 1 && o.LOGINNAME.Equals(cboUser.EditValue.ToString()));
                    if (user == null)
                    {
                        ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                        ErrorType = ErrorType.Warning;
                        return valid;
                    }

                    if (String.IsNullOrWhiteSpace(user.USERNAME))
                    {
                        ErrorText = String.Format(ResourceMessage.NguoiChiDinhKhongCoThongTinHoTen, user.LOGINNAME);
                        ErrorType = ErrorType.Warning;
                        return valid;
                    }
                }
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
