using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.LocalStorage.BackendData;

namespace HIS.Desktop.Plugins.HisSourceMedicine
{
    class ValidateTextCode : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtSourceMedicineCode; 
        internal DevExpress.XtraEditors.TextEdit txtSourceMedicineName; 
        internal bool IsExist;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtSourceMedicineCode == null) return valid;
                if (String.IsNullOrEmpty(txtSourceMedicineCode.Text.Trim()))
                {
                    this.ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    this.ErrorType = ErrorType.Warning;
                    return valid;
                }
                if (IsExist)
                {
                    //string existingName = BackendDataWorker.Get<HIS_SOURCE_MEDICINE>().FirstOrDefault(o => o.SOURCE_MEDICINE_CODE == txtSourceMedicineCode).txtSourceMedicineName;
                    string existingName = (txtSourceMedicineName.Text.Trim());
                    this.ErrorText = string.Format("Mã '{0}' đã được gán với tên '{1}'.", txtSourceMedicineCode.Text.Trim(), existingName);
                    this.ErrorType = ErrorType.Warning;
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
