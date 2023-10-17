using System;

namespace HIS.Desktop.Plugins.ExpMestDepaCreate
{
	internal class IsRequiredReasonValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
	{


		internal DevExpress.XtraEditors.GridLookUpEdit cbo;

		public override bool Validate(System.Windows.Forms.Control control, object value)
		{
			bool valid = false;
			try
			{
				if (cbo == null) return valid;
				if (cbo.EditValue == null)
				{
					ErrorText = Base.ResourceMessageLang.TruongDuLieuBatBuoc;
					ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
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