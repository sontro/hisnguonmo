using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.ViewInfo;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Controls.Session;
using HIS.Desktop.Plugins.UpdateExamServiceReq.Validate;
using Inventec.Common.Adapter;
using Inventec.Core;
using Inventec.Desktop.Common.Controls.ValidationRule;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
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

namespace HIS.Desktop.Plugins.UpdateExamServiceReq
{
    public partial class frmUpdateExamServiceReq : HIS.Desktop.Utility.FormBase
    {
        public void InitValidate()
        {
            try
            {
                UpdateExamServiceReqExamValidate();
                ServiceValidate();
                PatientTypeValidate();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void UpdateExamServiceReqExamValidate()
        {
            try
            {
                GridLookupEditWithTextEditValidationRule roomVali = new GridLookupEditWithTextEditValidationRule();
                roomVali.txtTextEdit = txtRoomCode;
                roomVali.cbo = cboRoom;
                roomVali.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                roomVali.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(txtRoomCode, roomVali);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ServiceValidate()
        {
            try
            {
                GridLookupEditWithTextEditValidationRule serviceVali = new GridLookupEditWithTextEditValidationRule();
                serviceVali.txtTextEdit = txtServiceCode;
                serviceVali.cbo = cboExamServiceReq;
                serviceVali.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                serviceVali.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(txtServiceCode, serviceVali);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void PatientTypeValidate()
        {
            try
            {
                GridLookupEditWithTextEditValidationRule patientTypeVali = new GridLookupEditWithTextEditValidationRule();
                patientTypeVali.txtTextEdit = txtPatientTypeCode;
                patientTypeVali.cbo = cboPatientType;
                patientTypeVali.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                patientTypeVali.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(txtPatientTypeCode, patientTypeVali);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void DateEditValidate()
        {
            try
            {
                DateEditValidationRule dateEditVali = new DateEditValidationRule();
                dateEditVali.dateEdit = dtInstructionTime;
                if (treatment!=null)
                    dateEditVali.inTime = treatment.IN_TIME;
                dateEditVali.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(dtInstructionTime, dateEditVali);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
