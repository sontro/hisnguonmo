using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisExpMestTemplate.Validate
{
    class TutorialValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtTutorial;
        internal int? DataType = null;

        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
                if (String.IsNullOrEmpty(txtTutorial.Text) && DataType == HIS.Desktop.LocalStorage.BackendData.ADO.MedicineMaterialTypeComboADO.THUOC)
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
