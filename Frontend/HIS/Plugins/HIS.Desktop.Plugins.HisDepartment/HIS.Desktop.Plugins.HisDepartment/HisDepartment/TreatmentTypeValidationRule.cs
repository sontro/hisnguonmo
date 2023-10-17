using HIS.Desktop.LibraryMessage;
using HIS.Desktop.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisDepartment.HisDepartment
{
    class TreatmentTypeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.CheckEdit chkIsClinical;
        internal DevExpress.XtraEditors.GridLookUpEdit cboTreatmentType;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (chkIsClinical == null || cboTreatmentType == null) return valid;
                if (chkIsClinical.Checked)
                {
                    GridCheckMarksSelection gridCheckMark = cboTreatmentType.Properties.Tag as GridCheckMarksSelection;
                    if (gridCheckMark == null || gridCheckMark.SelectedCount <= 0)
                    {
                        ErrorText = "Trường dữ liệu bắt buộc với khoa lâm sàng";
                        ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
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
