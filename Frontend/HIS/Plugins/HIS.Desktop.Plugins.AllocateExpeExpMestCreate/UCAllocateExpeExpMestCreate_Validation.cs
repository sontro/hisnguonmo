using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.LibraryMessage;
using HIS.Desktop.Plugins.AllocateExpeExpMestCreate.Validation;
using Inventec.Desktop.Common.Controls.ValidationRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.AllocateExpeExpMestCreate
{
    public partial class UCAllocateExpeExpMestCreate : UserControl
    {

        private void ValidControlExpAmount()
        {
            try
            {
                ExpAmountValidationRule amountRule = new ExpAmountValidationRule();
                amountRule.spinAmount = spinAmount;
                dxValidationProviderControlLeft.SetValidationRule(spinAmount, amountRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ValidControlMedistock()
        {
            try
            {
                MedistockValidationRule medistockValidationRule = new MedistockValidationRule();
                medistockValidationRule.cboMedistock = cboMedistock;
                dxValidationProviderControlLeft.SetValidationRule(txtExpMedistock, medistockValidationRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ValidControl()
        {
            try
            {
                ValidControlExpAmount();
                ValidControlMedistock();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
       
    }
}
