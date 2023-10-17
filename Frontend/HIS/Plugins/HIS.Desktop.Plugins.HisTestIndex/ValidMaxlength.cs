using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisTestIndex
{
    class ValidMaxlength : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit textedit;
        internal int Maxlength;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (string.IsNullOrEmpty(textedit.Text))
                {
                    this.ErrorText = HIS.Desktop.LibraryMessage.MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    return valid;
                }
                else
                {
                    if (Inventec.Common.String.CountVi.Count(textedit.Text) > Maxlength)
                    {
                        this.ErrorText = "Độ dài mã vượt quá " + Maxlength;
                        return valid;
                    }
                    else
                    {
                        if (Inventec.Common.String.CountVi.Count(textedit.Text) > Maxlength)
                        {
                            this.ErrorText = "Độ dài mã vượt quá " + Maxlength;
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
