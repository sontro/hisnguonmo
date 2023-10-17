using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.LibraryMessage;
using HIS.Desktop.Plugins.ScnAccidentHurt.Resources;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ScnAccidentHurt
{
    public partial class frmAccidentHurt : HIS.Desktop.Utility.FormBase
    {
        internal void ValidationControl()
        {
            try
            {
                //ValidationAccidentLocaltion();
                //ValidationAccidentBodyPart();
                //ValidationAccidentCare();
                //ValidationAccidentHelmet();
                ValidationAccidentHurtType();
                //ValidationAccidentPoison();
                //ValidationAccidentResult();
                //ValidationAccidentVehicle();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);

            }
        }

        private void ValidationAccidentLocaltion()
        {
            try
            {
                ComboWithTextEditValidationRule icdMainRule = new ComboWithTextEditValidationRule();
                icdMainRule.txtTextEdit = txtAccidentLocaltion;
                icdMainRule.cbo = cboAccidentLocaltion;
                icdMainRule.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                icdMainRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider.SetValidationRule(txtAccidentLocaltion, icdMainRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidationAccidentBodyPart()
        {
            try
            {
                ComboWithTextEditValidationRule icdMainRule = new ComboWithTextEditValidationRule();
                icdMainRule.txtTextEdit = txtAccidentBodyPart;
                icdMainRule.cbo = cboAccidentBodyPart;
                icdMainRule.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                icdMainRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider.SetValidationRule(txtAccidentBodyPart, icdMainRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidationAccidentCare()
        {
            try
            {
                ComboWithTextEditValidationRule icdMainRule = new ComboWithTextEditValidationRule();
                icdMainRule.txtTextEdit = txtAccidentCare;
                icdMainRule.cbo = cboAccidentCare;
                icdMainRule.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                icdMainRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider.SetValidationRule(txtAccidentCare, icdMainRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidationAccidentHelmet()
        {
            try
            {
                ComboWithTextEditValidationRule icdMainRule = new ComboWithTextEditValidationRule();
                icdMainRule.txtTextEdit = txtHelmet;
                icdMainRule.cbo = cboHelmet;
                icdMainRule.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                icdMainRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider.SetValidationRule(txtHelmet, icdMainRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidationAccidentHurtType()
        {
            try
            {
                ComboWithTextEditValidationRule icdMainRule = new ComboWithTextEditValidationRule();
                icdMainRule.txtTextEdit = txtAccidentHurtType;
                icdMainRule.cbo = cboAccidentHurtType;
                icdMainRule.ErrorText = String.Format(ResourceMessage.TruongDuLieuBatBuocPhaiNhap);
                icdMainRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider.SetValidationRule(txtAccidentHurtType, icdMainRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidationAccidentPoison()
        {
            try
            {
                ComboWithTextEditValidationRule icdMainRule = new ComboWithTextEditValidationRule();
                icdMainRule.txtTextEdit = txtAccidentPoison;
                icdMainRule.cbo = cboAccidentPoison;
                icdMainRule.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                icdMainRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider.SetValidationRule(txtAccidentPoison, icdMainRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidationAccidentResult()
        {
            try
            {
                ComboWithTextEditValidationRule icdMainRule = new ComboWithTextEditValidationRule();
                icdMainRule.txtTextEdit = txtAccidentResult;
                icdMainRule.cbo = cboAccidentResult;
                icdMainRule.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                icdMainRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider.SetValidationRule(txtAccidentResult, icdMainRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidationAccidentVehicle()
        {
            try
            {
                ComboWithTextEditValidationRule icdMainRule = new ComboWithTextEditValidationRule();
                icdMainRule.txtTextEdit = txtAccidentVehicle;
                icdMainRule.cbo = cboAccidentVehicle;
                icdMainRule.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                icdMainRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider.SetValidationRule(txtAccidentVehicle, icdMainRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
