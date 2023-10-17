using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisDocumentBook.Validtion
{
    class ValidationTotalNumber : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spin;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (string.IsNullOrEmpty(spin.Text))
                {
                    this.ErrorText = HIS.Desktop.LibraryMessage.MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    return valid;
                }
                else
                {
                    if (Int32.Parse(spin.Text)<= 0)
                    {
                        this.ErrorText = "tổng số phải lớn hơn " + 0;
                        return valid;
                    }
                    else
                    {
                        if (Int32.Parse(spin.Text) <= 0)
                        {
                            this.ErrorText = "tổng số phải lớn hơn " + 0;
                            return valid;
                        }
                        else
                        {
                            valid = true;
                        }
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
