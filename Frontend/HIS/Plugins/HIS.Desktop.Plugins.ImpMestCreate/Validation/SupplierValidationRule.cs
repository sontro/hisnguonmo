using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.Validation
{
    class SupplierValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.LookUpEdit cboImpMestType;
        internal DevExpress.XtraEditors.GridLookUpEdit cboSupplier;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (cboImpMestType == null || cboSupplier == null) return valid;
                if (cboImpMestType.EditValue != null && Convert.ToInt64(cboImpMestType.EditValue) == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__NCC)
                {
                    if (cboSupplier.EditValue == null || String.IsNullOrEmpty(cboSupplier.Text))
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
