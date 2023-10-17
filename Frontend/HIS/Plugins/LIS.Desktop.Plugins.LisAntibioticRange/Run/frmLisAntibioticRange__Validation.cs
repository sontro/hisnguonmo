using DevExpress.XtraEditors.DXErrorProvider;
using LIS.Desktop.Plugins.LisAntibioticRange.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIS.Desktop.Plugins.LisAntibioticRange.Run
{
	public partial class frmLisAntibioticRange
	{

		private void ValidateForm()
		{
			try
			{
				ValidateBacterium();
				ValidateAntibiotic();
				SetMaxLengthMin();
				SetMaxLengthMax();
				SetMaxLengthSri();
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}

		private void SetMaxLengthMin()
		{
			try
			{
				ValidateMaxLength valid = new ValidateMaxLength();
				valid.textEdit = txtMinValue;
				valid.maxLength = 100;
				this.dxValidationProvider1.SetValidationRule(txtMinValue, valid);
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}

		private void SetMaxLengthSri()
		{
			try
			{
				ValidateMaxLength valid = new ValidateMaxLength();
				valid.textEdit = txtSriValue;
				valid.maxLength = 100;
				this.dxValidationProvider1.SetValidationRule(txtSriValue, valid);
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}

		private void SetMaxLengthMax()
		{
			try
			{
				ValidateMaxLength valid = new ValidateMaxLength();
				valid.textEdit = txtMaxValue;
				valid.maxLength = 100;
				this.dxValidationProvider1.SetValidationRule(txtMaxValue, valid);
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}

		private void ValidateBacterium()
		{
			try
			{
				ValidationControls valid = new ValidationControls();
				valid.cbo = this.cboBacterium;
				valid.txt = this.txtBacterium;
				valid.ErrorText = "Trường dữ liệu bắt buộc";
				valid.ErrorType = ErrorType.Warning;
				this.dxValidationProvider1.SetValidationRule(txtBacterium, valid);
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}

		private void ValidateAntibiotic()
		{
			try
			{
				ValidationControls valid = new ValidationControls();
				valid.cbo = this.cboAntibiotic;
				valid.txt = this.txtActibiotic;
				valid.ErrorText = "Trường dữ liệu bắt buộc";
				valid.ErrorType = ErrorType.Warning;
				this.dxValidationProvider1.SetValidationRule(txtActibiotic, valid);
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Warn(ex);
			}
		}
	}
}
