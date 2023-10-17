using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Validate.ValidationRule;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Config;
using HIS.Desktop.Utility;
using Inventec.Desktop.Common.Controls.ValidationRule;
using System;
using System.Runtime.InteropServices;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.LocalStorage.BackendData;
using System.Linq;
using Inventec.Core;
using MOS.Filter;
using Inventec.Common.Adapter;
using HIS.Desktop.ApiConsumer;
using System.Collections.Generic;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute
{
    public partial class SurgServiceReqExecuteControl : UserControlBase
    {
        public async void ValidateControl()
        {
            try
            {
                Inventec.Common.Logging.LogSystem.Debug("ValidateControl");
                if (HisConfigCFG.REQUIRED_GROUPPTTT_OPTION != "1")
                {
                    this.sereServ = (MOS.EFMODEL.DataModels.V_HIS_SERE_SERV_5)grdViewService.GetFocusedRow();
                    Inventec.Common.Logging.LogSystem.Debug("sereServValidate " + sereServ);
                    if (this.sereServ != null)
                    {
                        var surgMisuService = lstService.FirstOrDefault(o => o.ID == sereServ.SERVICE_ID);
                        if (surgMisuService != null)
                        {
                            if (surgMisuService != null)
                            {
                                if (surgMisuService.PTTT_GROUP_ID.HasValue)
                                {
                                    ValidationLookUpWithTextEdit(cbbPtttGroup, txtPtttGroupCode);
                                }
                            }
                        }
                    }
                }
                else
                {
                    ValidationLookUpWithTextEdit(cbbPtttGroup, txtPtttGroupCode);
                }
                ValidationStartTime();
                ValiVuotQuaKyTu();
                ValidationFinishTime();
                if (PriorityIsRequired == 1 || (PriorityIsRequired == 2 && this.serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT))
                    ValidationGridLookUpWithTextEdit(cboLoaiPT, txtLoaiPT);

                if (MethodIsRequired == 1 || (MethodIsRequired == 2 && this.serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT))
                    ValidationGridLookUpWithTextEdit(cboMethod, txtMethodCode);

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidationGridLookUpWithTextEdit(GridLookUpEdit cboLoaiPT, TextEdit txtLoaiPT)
        {
            try
            {
                GridLookupEditWithTextEditValidationRule validate = new GridLookupEditWithTextEditValidationRule();
                validate.cbo = cboLoaiPT;
                validate.txtTextEdit = txtLoaiPT;
                validate.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                validate.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(txtLoaiPT, validate);
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidationLookUpWithTextEdit(LookUpEdit cbo, TextEdit txt)
        {
            try
            {
                LookupEditWithTextEditValidationRule icdMainRule = new LookupEditWithTextEditValidationRule();

                icdMainRule.txtTextEdit = txt;
                icdMainRule.cbo = cbo;
                icdMainRule.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                icdMainRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(txt, icdMainRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidationStartTime()
        {
            try
            {
                StartTimeValidationRule mainRule = new StartTimeValidationRule();
                mainRule.startTime = dtStart;
                mainRule.finishTime = dtFinish;
                mainRule.instructionTime = serviceReq.INTRUCTION_TIME;
                mainRule.treatmentOutTime = this.vhisTreatment.OUT_TIME ?? 0;
                mainRule.keyCheck = this.isAllowEditInfo;
                mainRule.keyCheckStatsTime = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>("HIS.DESKTOP.PLUGINS.SURGSERVICEREQEXECUTE.BIG_START_TIME_CURRENT_TIME") == "1"; ;
                mainRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(dtStart, mainRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidationFinishTime()
        {
            try
            {
                FinishTimeValidationRule mainRule = new FinishTimeValidationRule();
                mainRule.startTime = dtStart;
                mainRule.finishTime = dtFinish;
                mainRule.instructionTime = serviceReq.INTRUCTION_TIME;
                mainRule.treatmentOutTime = this.vhisTreatment.OUT_TIME ?? 0;
                mainRule.keyCheck = this.isAllowEditInfo;
                mainRule.keyCheckStatsTime = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>("HIS.DESKTOP.PLUGINS.SURGSERVICEREQEXECUTE.BIG_START_TIME_CURRENT_TIME") == "1"; ;
                mainRule.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(dtFinish, mainRule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValiVuotQuaKyTu()
        {
            try
            {
                bool mannerRequired = false;
                if (MannerIsRequired == 1 || (MannerIsRequired == 2 && this.serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT))
                    mannerRequired = true;

                ValidationControlMaxLength(txtMANNER, 3000, mannerRequired);
                ValidationControlMaxLength(txtConclude, 1000);
                ValidationControlMaxLength(txtResultNote, 3000);
                ValidationControlMaxLength(txtDescription, 4000);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ValidationControlMaxLength(BaseEdit control, int? maxLength, [Optional] bool isRequired)
        {
            ControlMaxLengthValidationRule validate = new ControlMaxLengthValidationRule();
            validate.editor = control;
            validate.maxLength = maxLength;
            validate.IsRequired = isRequired;
            validate.ErrorText = Resources.ResourceMessage.TruongDuLieuVuotQuaKyTu;
            validate.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
            this.dxValidationProvider1.SetValidationRule(control, validate);
        }
    }
}
