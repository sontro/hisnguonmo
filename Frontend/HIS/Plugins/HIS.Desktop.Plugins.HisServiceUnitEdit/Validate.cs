using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.LibraryMessage;
using HIS.Desktop.Plugins.HisServiceUnitEdit.Resources;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisServiceUnitEdit
{
    public delegate bool IsValid(ControlEditValidationRule validADO, out string errorText);

     public class ControlEditValidationRule : ValidationRule
    {
         public object editor;

         public IsValid IsValid;

         public int? Maxlength;

         public bool IsValidControl = false;

        public override bool Validate(Control control, object value)
        {
            bool flag = false;
            bool result;
            try
            {
                if (this.editor == null)
                {
                    result = flag;
                    return result;
                }
                if (this.IsValidControl)
                {
                    return true;
                }
                string errorText = "";
                if (this.IsValid != null && this.IsValid(this,out errorText))
                {
                    this.ErrorText = errorText;
                    result = flag;
                    return result;
                }
                flag = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            result = flag;
            return result;
        }
    }
}
