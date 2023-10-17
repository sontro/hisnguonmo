using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.EmrSigner.ValitionRule
{
    public class SIGN_IMAGE_Validation : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.PictureEdit picturEdit;
        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
                valid = valid && (picturEdit != null);
                if (valid)
                {
                    if (picturEdit.EditValue != null)
                    {
                        if (picturEdit.Image.Width > 600 || picturEdit.Image.Height > 200)
                        {
                            valid = false;
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
