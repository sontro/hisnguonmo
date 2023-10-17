using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.Desktop.Plugins.Religion.Validtion
{
    class ValidMaxlengthTxtReligionCode : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtReligionCode;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (string.IsNullOrEmpty(txtReligionCode.Text) )
                {
                    this.ErrorText = HIS.Desktop.LibraryMessage.MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    return valid;
                }
                else
                {
                    if (Inventec.Common.String.CountVi.Count(txtReligionCode.Text) > 2 )
                    {
                        this.ErrorText = "Độ dài mã vượt quá " + 2 ;
                        return valid;
                    }
                    else
                    {
                        if (Inventec.Common.String.CountVi.Count(txtReligionCode.Text) > 2)
                        {
                            this.ErrorText = "Độ dài mã vượt quá " + 2;
                            return valid;
                        }
                        
                        else
                            valid = true;
                    }
                    

                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
