using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisBloodTypeVolume.Validation
{

	public class ValidationGridTextControls : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
	{
		internal DevExpress.XtraEditors.TextEdit txt;
		internal DevExpress.XtraEditors.GridLookUpEdit cbo;
		public override bool Validate(Control control, object value)
		{
			bool valid = false;
			try
			{
				if (txt == null || cbo == null)
					return valid;
				if (string.IsNullOrEmpty(txt.Text) || cbo.EditValue == null)
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
