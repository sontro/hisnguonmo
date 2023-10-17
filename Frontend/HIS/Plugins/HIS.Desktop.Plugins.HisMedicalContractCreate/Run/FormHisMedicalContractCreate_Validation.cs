using HIS.Desktop.Plugins.HisMedicalContractCreate.Validation;
using HIS.Desktop.Utility;
using Inventec.Desktop.Common.Controls.ValidationRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisMedicalContractCreate.Run
{
    public partial class FormHisMedicalContractCreate : FormBase
    {
        private void ValidControl()
        {
            try
            {
                ValidateMedicalContract();
                ValidateMetyMaty();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ValidateMedicalContract()
        {
            try
            {
                ControlMaxLengthValidationRule contractCodeValidate = new ControlMaxLengthValidationRule();
                contractCodeValidate.editor = txtMedicalContractCode;
                contractCodeValidate.maxLength = 50;
                contractCodeValidate.IsRequired = true;
                contractCodeValidate.ErrorText = Resources.ResourceLanguageManager.ThieuTruongDuLieuBatBuoc;
                contractCodeValidate.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                dxValidationProviderContract.SetValidationRule(txtMedicalContractCode, contractCodeValidate);

                ControlEditValidationRule supplierValidate = new ControlEditValidationRule();
                supplierValidate.editor = cboSupplier;
                supplierValidate.ErrorText = Resources.ResourceLanguageManager.ThieuTruongDuLieuBatBuoc;
                supplierValidate.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                dxValidationProviderContract.SetValidationRule(cboSupplier, supplierValidate);

                Validation.ValidTimeValidationRule validTimeValidate = new Validation.ValidTimeValidationRule();
                validTimeValidate.dtValidFromDate = dtValidFromDate;
                validTimeValidate.dtValidToDate = dtValidToDate;
                validTimeValidate.ErrorText = Resources.ResourceLanguageManager.HieuLucDenLonHonHieuLucTu;
                validTimeValidate.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                dxValidationProviderContract.SetValidationRule(dtValidFromDate, validTimeValidate);

                ControlMaxLengthValidationRule ContractNameValidate = new ControlMaxLengthValidationRule();
                ContractNameValidate.editor = txtMedicalContractName;
                ContractNameValidate.maxLength = 200;
                ContractNameValidate.IsRequired = false;
                ContractNameValidate.ErrorText = string.Format(Resources.ResourceLanguageManager.NhapQuaMaxlength, "200");
                ContractNameValidate.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                dxValidationProviderContract.SetValidationRule(txtMedicalContractName, ContractNameValidate);

                ControlMaxLengthValidationRule ventureAgreeningValidate = new ControlMaxLengthValidationRule();
                ventureAgreeningValidate.editor = txtVentureAgreening;
                ventureAgreeningValidate.maxLength = 500;
                ventureAgreeningValidate.IsRequired = false;
                ventureAgreeningValidate.ErrorText = string.Format(Resources.ResourceLanguageManager.NhapQuaMaxlength, "500");
                ventureAgreeningValidate.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                dxValidationProviderContract.SetValidationRule(txtVentureAgreening, ventureAgreeningValidate);

                ControlMaxLengthValidationRule noteValidate = new ControlMaxLengthValidationRule();
                noteValidate.editor = txtNote;
                noteValidate.maxLength = 4000;
                noteValidate.IsRequired = false;
                noteValidate.ErrorText = string.Format(Resources.ResourceLanguageManager.NhapQuaMaxlength, "4000");
                noteValidate.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                dxValidationProviderContract.SetValidationRule(txtNote, noteValidate);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ValidateMetyMaty()
        {
            try
            {
                PriceAmountValidationRule amountValidate = new PriceAmountValidationRule();
                amountValidate.spinImpPrice = spAmount;
                amountValidate.Minvalue = 1;
                amountValidate.ErrorText = Resources.ResourceLanguageManager.ThieuTruongDuLieuBatBuoc;
                amountValidate.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                dxValidationProviderMetyMate.SetValidationRule(spAmount, amountValidate);

                PriceAmountValidationRule impPriceValidate = new PriceAmountValidationRule();
                impPriceValidate.spinImpPrice = spContractPrice;
                amountValidate.Minvalue = 0;
                impPriceValidate.ErrorText = Resources.ResourceLanguageManager.ThieuTruongDuLieuBatBuoc;
                impPriceValidate.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                dxValidationProviderMetyMate.SetValidationRule(spContractPrice, impPriceValidate);

                ImpVatRatioValidationRule impVatValidate = new ImpVatRatioValidationRule();
                impVatValidate.spinImpVatRatio = spImpVat;
                impVatValidate.ErrorText = Resources.ResourceLanguageManager.ThieuTruongDuLieuBatBuoc;
                impVatValidate.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                dxValidationProviderMetyMate.SetValidationRule(spImpVat, impVatValidate);

                ControlMaxLengthValidationRule registerNumberValidate = new ControlMaxLengthValidationRule();
                registerNumberValidate.editor = txtRegisterNumber;
                registerNumberValidate.maxLength = 500;
                registerNumberValidate.IsRequired = false;
                registerNumberValidate.ErrorText = string.Format(Resources.ResourceLanguageManager.NhapQuaMaxlength, "4000");
                registerNumberValidate.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                dxValidationProviderContract.SetValidationRule(txtRegisterNumber, registerNumberValidate);

                ControlMaxLengthValidationRule concentraValidate = new ControlMaxLengthValidationRule();
                concentraValidate.editor = txtConcentra;
                concentraValidate.maxLength = 1000;
                concentraValidate.IsRequired = false;
                concentraValidate.ErrorText = string.Format(Resources.ResourceLanguageManager.NhapQuaMaxlength, "4000");
                concentraValidate.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                dxValidationProviderContract.SetValidationRule(txtConcentra, concentraValidate);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
