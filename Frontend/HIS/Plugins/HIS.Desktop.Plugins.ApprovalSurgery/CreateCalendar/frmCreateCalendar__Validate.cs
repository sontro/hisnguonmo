using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.Plugins.ApprovalSurgery.CreateCalendar.ValidationRule;
using HIS.Desktop.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ApprovalSurgery.CreateCalendar
{
    public partial class frmCreateCalendar : FormBase
    {
        private void ValidateControl()
        {
            try
            {
                ValidationTimeFrom();
                ValidationTimeTo();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidationTimeFrom()
        {
            try
            {
                TimeFromValidationRule mainRule = new TimeFromValidationRule();
                mainRule.timeFrom = dtTimeFrom;
                mainRule.timeTo = dtTimeTo;
                mainRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(dtTimeFrom, mainRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidationTimeTo()
        {
            try
            {
                TimeToValidationRule mainRule = new TimeToValidationRule();
                mainRule.timeFrom = dtTimeFrom;
                mainRule.timeTo = dtTimeTo;
                mainRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(dtTimeTo, mainRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
