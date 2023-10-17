using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MaterialTypeCreate.Validtion
{
    class ValidMaxlengthTxtMaterialTypeCodeName : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtMaterialTypeTypeCode;
        internal DevExpress.XtraEditors.TextEdit txtMaterialTypeTypeName;
        internal bool isValidCode = true;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if ((isValidCode && string.IsNullOrEmpty(txtMaterialTypeTypeCode.Text)) || string.IsNullOrEmpty(txtMaterialTypeTypeName.Text))
                {
                    this.ErrorText = HIS.Desktop.LibraryMessage.MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    return valid;
                }
                else
                {
                    if (Inventec.Common.String.CountVi.Count(txtMaterialTypeTypeCode.Text) > 25 && Inventec.Common.String.CountVi.Count(txtMaterialTypeTypeName.Text) > 500)
                    {
                        this.ErrorText = "Độ dài mã vượt quá " + 25 + "||" + "Độ dài tên vượt quá " + 500;
                        return valid;
                    }
                    else
                    {
                        if (Inventec.Common.String.CountVi.Count(txtMaterialTypeTypeCode.Text) > 25)
                        {
                            this.ErrorText = "Độ dài mã vượt quá " + 25;
                            return valid;
                        }
                        else if (Inventec.Common.String.CountVi.Count(txtMaterialTypeTypeName.Text) > 500)
                        {
                            this.ErrorText = "Độ dài tên vượt quá " + 500;
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

