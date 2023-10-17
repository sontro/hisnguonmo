using HIS.Desktop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.Validation
{
    class DocumentDateValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.ButtonEdit txtDocumentDate;
        internal DevExpress.XtraEditors.DateEdit dtDocumentDate;
        internal DevExpress.XtraEditors.LookUpEdit cboImpMestType;
        internal bool keyCheck;

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
                        ErrorText = Base.ResourceMessageManager.NguoiDungNhapNgayKhongHopLe;
                        ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                        return valid;
                    }
                    dtDocumentDate.EditValue = dt;
                }

                if (cboImpMestType.EditValue != null
                    && Inventec.Common.TypeConvert.Parse.ToInt64(cboImpMestType.EditValue.ToString() ?? "0") == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__NCC
                    && keyCheck)
                {
                    if (String.IsNullOrEmpty(txtDocumentDate.Text))
                    {
                        ErrorText = Base.ResourceMessageManager.TruongDuLieuBatBuoc;
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
