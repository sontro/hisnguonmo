using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisCarerCardBorrow.Validation
{
	internal class ValidationLabel : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
	{
		internal DevExpress.XtraEditors.LabelControl lbl;
		public override bool Validate(Control control, object value)
		{
			bool valid = false;
			try
			{
				if (lbl == null || string.IsNullOrEmpty(lbl.Text))
					return valid;				
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
