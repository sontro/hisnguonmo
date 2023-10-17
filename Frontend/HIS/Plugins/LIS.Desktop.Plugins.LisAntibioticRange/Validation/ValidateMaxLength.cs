using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using Inventec.Common.Logging;
using Inventec.Common.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIS.Desktop.Plugins.LisAntibioticRange.Validation
{
	public class ValidateMaxLength : ValidationRule
	{
		internal BaseEdit textEdit;

		internal int? maxLength;

		public override bool Validate(Control control, object value)
		{
			bool result = false;
			try
			{
				if (textEdit == null)
				{
					return result;
				}
				if (maxLength.HasValue && !string.IsNullOrEmpty(textEdit.Text) && CountVi.Count(textEdit.Text) > maxLength)
				{
					int? num = maxLength;
					base.ErrorText = "Vượt quá độ dài cho phép " + num + " ký tự!";
					base.ErrorType = ErrorType.Warning;
					return result;
				}
				result = true;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}
	}
}
