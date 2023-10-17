using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InfantInformation.Validate
{
    class InfantValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtInfantMidwife1;
        internal DevExpress.XtraEditors.TextEdit txtInfantMidwife2;
        internal DevExpress.XtraEditors.TextEdit txtInfantMidwife3;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (String.IsNullOrEmpty(txtInfantMidwife1.Text))
                {
                    if (String.IsNullOrEmpty(txtInfantMidwife2.Text))
                    {
                        if (String.IsNullOrEmpty(txtInfantMidwife3.Text))
                        {
                            ErrorText = HIS.Desktop.LibraryMessage.MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                            ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                            return valid;
                        }
                    }
                    
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
