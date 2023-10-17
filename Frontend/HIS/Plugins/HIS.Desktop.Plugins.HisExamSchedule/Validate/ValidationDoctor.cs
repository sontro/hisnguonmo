using HIS.Desktop.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;

namespace HIS.Desktop.Plugins.HisExamSchedule.Validate
{
    class ValidationDoctor : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.GridLookUpEdit cboDoctor;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            
            try
            {
                GridCheckMarksSelection gridCheckMark = cboDoctor.Properties.Tag as GridCheckMarksSelection;
                if (gridCheckMark != null && gridCheckMark.SelectedCount > 0)
                {
                    valid = true;
                }
                else
                {
                    this.ErrorText = "Trường dữ liệu bắt buộc.";
                    this.ErrorType = ErrorType.Warning;
                    return valid;
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);                
            }
            
            return valid;
        }

    }
}
