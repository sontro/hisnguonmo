using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors;
using Inventec.Desktop.Common.Controls.ValidationRule;
using Inventec.Desktop.Common.LibraryMessage;
using HFS.APP.Modules.InvoiceBook.ValidationControls;

namespace HIS.Desktop.Plugins.InvoiceBook
{
    public partial class UCInvoiceBook : HIS.Desktop.Utility.UserControlBase
    {
        private void ValidationControls()
        {

            ValidTotal();
            ValidFromNumOrder();
            ValidSymbolCode();
            ValidTemplateCode();
        }

        private void ValidSymbolCode()
        {
            try
            {
                SymbolCodeValidationRule nameRule = new SymbolCodeValidationRule();
                nameRule.txtName = txtSymbolCode;
                nameRule.ErrorText = MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                nameRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(txtSymbolCode, nameRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidTemplateCode()
        {
            try
            {
                TemplateCodeValidationRule nameRule = new TemplateCodeValidationRule();
                nameRule.txtName = txtTemplateCode;
                nameRule.ErrorText = MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                nameRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(txtTemplateCode, nameRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidTotal()
        {
            try
            {
                TotalValidationRule totalRule = new TotalValidationRule();
                totalRule.spinTotal = spTotal;
                totalRule.ErrorText = MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                totalRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(spTotal, totalRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidFromNumOrder()
        {
            try
            {
                FromNumOrderValidationRule copyCountRule = new FromNumOrderValidationRule();
                copyCountRule.spinFromNumOrder = spFromOrder;
                copyCountRule.ErrorText = MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                copyCountRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(spFromOrder, copyCountRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
     
    }
}
