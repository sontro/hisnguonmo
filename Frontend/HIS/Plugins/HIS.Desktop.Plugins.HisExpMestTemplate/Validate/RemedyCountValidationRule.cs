using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisExpMestTemplate.Validate
{
    class RemedyCountValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraLayout.LayoutControlItem lciSoThang;
        internal DevExpress.XtraEditors.TextEdit txtRemedyCount;

        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
                if (lciSoThang.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Never) return valid;

                if (String.IsNullOrEmpty(txtRemedyCount.Text))
                {
                    valid = false;
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
