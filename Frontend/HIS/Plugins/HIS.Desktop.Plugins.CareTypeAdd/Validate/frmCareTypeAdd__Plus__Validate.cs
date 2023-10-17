using DevExpress.Utils;
using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.Plugins.CareTypeAdd.Validate.ValidationRule;
using HIS.Desktop.LibraryMessage;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.CareTypeAdd
{
    public partial class frmCareTypeAdd : HIS.Desktop.Utility.FormBase
    {
        private void ValidCareTypeCode()
        {
            CareTypeCode__ValidationRule oCareTypeRule = new CareTypeCode__ValidationRule();
            oCareTypeRule.txtCareTypeCode = txtCareTypeCode;
            oCareTypeRule.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
            oCareTypeRule.ErrorType = ErrorType.Warning;
            this.dxValidationProvider.SetValidationRule(txtCareTypeCode, oCareTypeRule);
        }

        private void ValidCareTypeName()
        {
            CareTypeName__ValidationRule oCareTypeRule = new CareTypeName__ValidationRule();
            oCareTypeRule.txtCareTypeName = txtCareTypeName;
            oCareTypeRule.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
            oCareTypeRule.ErrorType = ErrorType.Warning;
            this.dxValidationProvider.SetValidationRule(txtCareTypeName, oCareTypeRule);
        }

        private void ValidControl()
        {
            try
            {
                ValidCareTypeCode();
                ValidCareTypeName();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
