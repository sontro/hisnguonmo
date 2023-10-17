using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Desktop.Plugins.EmrBusiness.Validate
{
    class ValidateMaxlangth:DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtControl;
        internal string message;
        internal int? Maxlength;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool vali = true;
            try
            {
                vali = vali && (txtControl != null);
                if (vali)
                {
                    string strError = "";
                    string Lengthtxt= txtControl.Text.Trim();
                    int? CountLength = Inventec.Common.String.CountVi.Count(Lengthtxt);
                   
                    if (String.IsNullOrEmpty(Lengthtxt))
                    {
                        vali = false;
                        strError = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);

                    }
                    else
                    {
                        if (CountLength > Maxlength)
                        {
                            vali = false;
                            strError += "Vượt quá ký tự cho phép " + Maxlength;
                        }
                    }
                    this.ErrorText = strError;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return vali;
        }
    }
}
