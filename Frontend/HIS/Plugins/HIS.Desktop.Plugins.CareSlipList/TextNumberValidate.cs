using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.CareSlipList
{
    class TextNumberValidate: DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtTextEdit;
        long numberCheck; 

        public override bool Validate(Control control, object value)
        {
            numberCheck = Inventec.Common.TypeConvert.Parse.ToInt64(txtTextEdit.Text);
            bool valid = false;
            try
            {
                if (txtTextEdit == null) return valid;
                if (String.IsNullOrEmpty(txtTextEdit.Text))
                    return valid;
                if (numberCheck <= 0) return valid;
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
    }

