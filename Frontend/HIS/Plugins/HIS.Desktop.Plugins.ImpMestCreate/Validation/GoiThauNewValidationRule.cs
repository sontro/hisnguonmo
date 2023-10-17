using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.Validation
{
    class GoiThauNewValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.GridLookUpEdit cbo;
        internal DevExpress.XtraEditors.LookUpEdit cboImpMestType;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (cboImpMestType == null || cbo == null) return valid;
                if (cbo.Enabled && cboImpMestType.EditValue != null && Convert.ToInt64(cboImpMestType.EditValue) == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__NCC && (cbo.EditValue == null || String.IsNullOrEmpty(cbo.Text)))
                {
                    ErrorText = Base.ResourceMessageManager.LoaiNhapLaNhapTuNhaCungCaoNguoiDungPhaiChonGoiThauHoacTichVaoNgoaiThau;
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
