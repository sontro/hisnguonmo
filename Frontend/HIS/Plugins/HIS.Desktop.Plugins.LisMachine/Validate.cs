using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.LisMachine
{
    class ValidatespMax : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtEdit;
        internal int maxLenght;

        public override bool Validate(System.Windows.Forms.Control control, object value)
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
                        if (CoutLength > maxLenght)
                        {
                            valid = false;
                            strError += "Vượt quá độ dài cho phép " + maxLenght;
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
