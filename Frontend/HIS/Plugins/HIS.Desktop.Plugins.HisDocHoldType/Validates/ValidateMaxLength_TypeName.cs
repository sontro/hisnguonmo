using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisDocHoldType.Validates
{
   
    class ValidateMaxLength_TypeName : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtEdit;
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

                        if (CoutLength > 100)
                        {
                            valid = false;
                            strError += ((!String.IsNullOrEmpty(strError) ? "\r\n" : "") + String.Format(ResourcesMassage.TenLoaiVuaQuaMaxlength, 100));
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
