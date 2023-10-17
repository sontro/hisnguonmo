using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid.Views.Base;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData.ADO;
using HIS.Desktop.Plugins.DispenseMedicine.ADO;
using HIS.Desktop.Plugins.DispenseMedicine.Validate;
using HIS.Desktop.Utility;
using Inventec.Common.Adapter;
using Inventec.Core;
using Inventec.Desktop.Common.Controls.ValidationRule;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.DispenseMedicine
{
    public partial class frmDispenseMedicine : FormBase
    {

        private void ValidateControl()
        {
            try
            {
                //ValidatePackage();
                ValidateMaty();
                ValidateMety();
                
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidateExpTime()
        {
            try
            {
                if (dtExpTime.EditValue != null)
                {
                    ExpDateValidationRule valiRule = new ExpDateValidationRule();
                    valiRule.dtDate = dtExpTime;
                    valiRule.ErrorType = ErrorType.Warning;
                    this.dxValidationProvider2.SetValidationRule(dtExpTime, valiRule);
                }
                else
                {
                    this.dxValidationProvider2.SetValidationRule(dtExpTime, null);
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidateMaty()
        {
            try
            {
                if (!String.IsNullOrEmpty(txtThuocChePham.Text))
                {
                    lciMatyAmount.AppearanceItemCaption.ForeColor = Color.Maroon;
                    AmountValidationRule valiRuleAmount = new AmountValidationRule();
                    valiRuleAmount.spinAmount = spinMateAmount;
                    valiRuleAmount.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                    valiRuleAmount.ErrorType = ErrorType.Warning;
                    this.dxValidationProvider1.SetValidationRule(spinMateAmount, valiRuleAmount);
                }
                else
                {
                    lciMatyAmount.AppearanceItemCaption.ForeColor = Color.Black;
                    this.dxValidationProvider1.SetValidationRule(spinMateAmount, null);
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidatePackage()
        {
            try
            {
                ControlEditValidationRule valiRule = new ControlEditValidationRule();
                valiRule.editor = txtPackageNumber;
                valiRule.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                valiRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(txtPackageNumber, valiRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidateMety()
        {
            try
            {
                ControlEditValidationRule valiRule = new ControlEditValidationRule();
                valiRule.editor = txtThuocThanhPham;
                valiRule.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                valiRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider2.SetValidationRule(txtThuocThanhPham, valiRule);

                AmountValidationRule valiRuleAmount = new AmountValidationRule();
                valiRuleAmount.spinAmount = spinMetyAmount;
                valiRuleAmount.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                valiRuleAmount.ErrorType = ErrorType.Warning;
                this.dxValidationProvider2.SetValidationRule(spinMetyAmount, valiRuleAmount);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
