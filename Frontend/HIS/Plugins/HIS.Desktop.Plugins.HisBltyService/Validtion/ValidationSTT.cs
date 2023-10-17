using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisBltyService.Validtion
{
    class ValidationSTT : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spin;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (string.IsNullOrEmpty(spin.Text)) 
                {
                    valid = true;
                    return valid;
                }
                else
                {
                    if (Int32.Parse(spin.Text) < 0)
                    {
                        this.ErrorText = "Số thứ tự phải lớn hơn hoặc bằng " + 0;
                        return valid;
                    }
                    else if (Inventec.Common.String.CountVi.Count(spin.Text) > 19)
                    {
                        this.ErrorText = "Độ dài vượt quá " + 19;
                        return valid;
                    }
                    else
                    {
                        if (Inventec.Common.String.CountVi.Count(spin.Text) > 19)
                        {
                            this.ErrorText = "Độ dài vượt quá " + 19;
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
