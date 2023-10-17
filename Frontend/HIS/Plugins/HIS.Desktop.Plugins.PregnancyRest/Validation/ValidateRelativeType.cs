using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.DXErrorProvider;

namespace HIS.Desktop.Plugins.PregnancyRest.Validation
{
    class ValidateRelativeType : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.GridLookUpEdit txtControl;
        internal DevExpress.XtraEditors.GridLookUpEdit cbo;
        internal long age;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool success = false;
            try
            {
                if (cbo.EditValue != null)
                {
                    long TreatmentEndTypeExtId = Inventec.Common.TypeConvert.Parse.ToInt64(cbo.EditValue.ToString() ?? "");
                    if (age < 7 && TreatmentEndTypeExtId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__NGHI_OM && txtControl.EditValue==null)
                    {
                        this.ErrorText = "Trường dữ liệu bắt buộc";
                        this.ErrorType = ErrorType.Warning;
                        return success;
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return success;
        }
    }
}
