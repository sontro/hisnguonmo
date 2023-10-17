using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentFinish.Validation
{
    class CboProgramValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.CheckEdit chkDataStore;
        internal DevExpress.XtraEditors.GridLookUpEdit cboProgram;
        internal bool IsMustSetProgram;
        internal long? TreatmentTypeId;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (((chkDataStore.Visible && chkDataStore.CheckState == System.Windows.Forms.CheckState.Checked) || (IsMustSetProgram && TreatmentTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU))
                    && cboProgram.EditValue == null)
                {
                    this.ErrorText = ResourceMessage.BatBuocChonChuongTrinh;
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
