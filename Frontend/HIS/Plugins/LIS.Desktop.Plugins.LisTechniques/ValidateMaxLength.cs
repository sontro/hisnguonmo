using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIS.Desktop.Plugins.LisTechniques
{
    class ValidateMaxLength : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtEdit;
        internal int Maxlength;

        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
                valid = valid && (txtEdit != null);
                if (valid)
                {
                    string strError = "";
                    string HisActiveIngredientCode = txtEdit.Text.Trim();
                    int? CoutLength = Inventec.Common.String.CountVi.Count(HisActiveIngredientCode);
                    if (String.IsNullOrEmpty(HisActiveIngredientCode))
                    {
                        valid = false;
                        strError = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    }
                    else
                    {
                        if (CoutLength > Maxlength)
                        {
                            valid = false;
                            strError += "Vượt quá độ dài cho phép " + Maxlength;
                        }
                    }
                    this.ErrorText = strError;
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
