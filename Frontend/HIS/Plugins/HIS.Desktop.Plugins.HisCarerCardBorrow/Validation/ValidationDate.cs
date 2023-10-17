using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisCarerCardBorrow.Validation
{
	internal class ValidationDate : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
	{
		internal DevExpress.XtraEditors.DateEdit dte;
		public override bool Validate(Control control, object value)
		{
			bool valid = false;
			try
			{
				if (dte.EditValue == null || dte.DateTime == DateTime.MinValue)
				{
					ErrorText = "Trường dữ liệu bắt buộc";
					return valid;
				}
				if (Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dte.DateTime) > Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateTime.Now))
				{
					ErrorText = "Thời gian nhập lớn hơn thời gian hiện tại";
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
