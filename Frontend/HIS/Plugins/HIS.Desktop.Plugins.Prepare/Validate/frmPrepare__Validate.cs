using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.ViewInfo;
using HIS.Desktop.Plugins.Prepare.ADO;
using HIS.Desktop.Plugins.Prepare.Validate.ValidateRule;
using HIS.Desktop.Utility;
using Inventec.Desktop.Common.Controls.ValidationRule;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Prepare
{
    public partial class frmPrepare : FormBase
    {
        private void ValidateControl()
        {
            try
            {
                this.TimeFromValidate();
                this.TimeToValidate();
                this.DescriptionValidate();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void TimeFromValidate()
        {
            try
            {
                TimeFromValidationRule mainRule = new TimeFromValidationRule();
                mainRule.timeFrom = dtFromTime;
                mainRule.timeTo = dtToTime;
                mainRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(dtFromTime, mainRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void TimeToValidate()
        {
            try
            {
                TimeToValidationRule mainRule = new TimeToValidationRule();
                mainRule.timeFrom = dtFromTime;
                mainRule.timeTo = dtToTime;
                mainRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(dtToTime, mainRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void DescriptionValidate()
        {
            try
            {
                ControlMaxLengthValidationRule validate = new ControlMaxLengthValidationRule();
                validate.editor = txtDescription;
                validate.maxLength = 500;
                validate.IsRequired = false;
                validate.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(txtDescription, validate);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
			
        }
    }
}
