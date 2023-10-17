using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.ConfigApplication;
using HIS.Desktop.Plugins.Library.TreatmentEndTypeExt.Data;
using HIS.Desktop.Utility;
using HIS.UC.WorkPlace;
using Inventec.Common.Adapter;
using Inventec.Common.Controls.EditorLoader;
using Inventec.Core;
using Inventec.Desktop.Common.Controls.ValidationRule;
using Inventec.Desktop.Common.LibraryMessage;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Library.TreatmentEndTypeExt.SickLeave
{
    public partial class frmSickLeave : FormBase
    {
        public void ValidateControl()
        {
            try
            {
                ValidationControlWorkPlace();
                ValidationControlMaxLength(txtRelativeName, 100);
                ValidateGridLookupWithTextEdit(cboUser, txtLoginName, dxValidationProvider1);
                ValidationControlBHXH(txtBhxhCode);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void ValidationControlBHXH(TextEdit txt)
        {
            try
            {
                Validation.ValidBhxhCode rule = new Validation.ValidBhxhCode();
                rule.bhxhCode = txt;
                this.dxValidationProvider1.SetValidationRule(txtBhxhCode, rule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ValidationControlWorkPlace()
        {
            try
            {
                Validation.ValidateWorkPlace rule = new Validation.ValidateWorkPlace();
                rule.cboWorkPlace = cboWorkPlace;
                rule.txtWorkPlace = txtPatientWorkPlace;
                this.dxValidationProvider1.SetValidationRule(cboWorkPlace, rule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void ValidationControlAge()
        {
            try
            {
                Validation.ValidateAgeTxt rule = new Validation.ValidateAgeTxt();
                rule.text = txtRelativeName;
                rule.histreatment = treatment;
                rule.ErrorType = ErrorType.Warning;
                if (string.IsNullOrEmpty(txtRelativeName.Text))
                {
                    this.dxValidationProvider1.SetValidationRule(txtRelativeName, rule);
                }


                Validation.ValidateAgeCbo rule_ = new Validation.ValidateAgeCbo();
                rule_.cbo = cboRelativeType;
                rule_.histreatment = treatment;
                rule_.ErrorType = ErrorType.Warning;
                if (cboRelativeType.EditValue == null || string.IsNullOrEmpty(cboRelativeType.Text.ToString())) 
				{
                    this.dxValidationProvider1.SetValidationRule(cboRelativeType, rule_);
				}
               
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void ValidationControlMaxLength(BaseEdit control, int? maxLength, [Optional] bool IsRequest)
        {
            try
            {
                ControlMaxLengthValidationRule validate = new ControlMaxLengthValidationRule();
                validate.editor = control;
                validate.maxLength = maxLength;
                validate.IsRequired = IsRequest;
                validate.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(control, validate);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
