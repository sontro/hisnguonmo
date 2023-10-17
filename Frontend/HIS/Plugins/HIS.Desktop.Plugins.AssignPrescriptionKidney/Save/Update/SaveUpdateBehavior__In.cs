using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Plugins.AssignPrescriptionKidney.AssignPrescription;
using Inventec.Common.Logging;
using MOS.SDO;
using System;

namespace HIS.Desktop.Plugins.AssignPrescriptionKidney.Save.Update
{
    partial class SaveUpdateBehavior : SaveAbstract, ISave
    {
        object Run__In()
        {
            InPatientPresResultSDO result = null;
            if (this.CheckValid() && OldServiceReq != null)
            {
                frmAssignPrescription.VerifyWarningOverCeiling();

                this.InitBase();

                InPatientPresSDO prescriptionSDO = new InPatientPresSDO();
                prescriptionSDO.Medicines = this.InPatientPresMedicineSDOs;
                prescriptionSDO.Materials = this.InPatientPresMaterialSDOs;
                prescriptionSDO.ServiceReqMaties = this.ServiceReqMaties;
                prescriptionSDO.ServiceReqMeties = this.ServiceReqMeties;
                prescriptionSDO.SerialNumbers = this.PatientPresMaterialBySerialNumberSDOs;
                prescriptionSDO.Id = OldServiceReq.ID;
                prescriptionSDO.TreatmentId = this.TreatmentId;
                prescriptionSDO.PrescriptionTypeId = PrescriptionType.NEW;
                this.ProcessPrescriptionUpdateSDO(prescriptionSDO);
                this.ProcessPrescriptionUpdateSDOICD(prescriptionSDO);

                LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => prescriptionSDO), prescriptionSDO));
                result = new Inventec.Common.Adapter.BackendAdapter(Param).Post<InPatientPresResultSDO>(RequestUriStore.HIS_SERVICE_REQ__IN_PATIENT_PRES_UPDATE, ApiConsumers.MosConsumer, prescriptionSDO, Param);
                if (result == null
                    || result.ServiceReqs == null || result.ServiceReqs.Count == 0
                    || ((result.ServiceReqMaties == null || result.ServiceReqMaties.Count == 0)
                        && (result.ServiceReqMeties == null || result.ServiceReqMeties.Count == 0)
                        && (result.Materials == null || result.Materials.Count == 0)
                        && (result.Medicines == null || result.Medicines.Count == 0))
                    )
                {
                    Inventec.Common.Logging.LogSystem.Debug("Goi api sua don thuoc that bai. Du lieu dau vao____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => prescriptionSDO), prescriptionSDO) + ". Du lieu dau ra____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result), result) + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => Param), Param));
                    result = null;
                }
            }

            return result;
        }

        private void ProcessPrescriptionUpdateSDO(InPatientPresSDO prescriptionSDO)
        {
            try
            {
                if (this.RemedyCount > 0)
                    prescriptionSDO.RemedyCount = this.RemedyCount;
                prescriptionSDO.Advise = this.Advise;
                prescriptionSDO.RequestRoomId = this.RequestRoomId;
                prescriptionSDO.RequestLoginName = this.RequestLoginname;
                prescriptionSDO.RequestUserName = this.RequestUserName;
                if (this.ParentServiceReqId > 0)
                    prescriptionSDO.ParentServiceReqId = this.ParentServiceReqId;
                if (frmAssignPrescription.lciPhieuDieuTri.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    && frmAssignPrescription.cboPhieuDieuTri.EditValue != null)
                {
                    prescriptionSDO.TrackingId = Inventec.Common.TypeConvert.Parse.ToInt64(frmAssignPrescription.cboPhieuDieuTri.EditValue.ToString());
                }
                prescriptionSDO.InstructionTimes = this.InstructionTimes;

                prescriptionSDO.IsHomePres = this.IsHomePres;
                prescriptionSDO.IsKidney = this.IsKidney;
                prescriptionSDO.KidneyTimes = this.KidneyTimes;
                prescriptionSDO.IsExecuteKidneyPres = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessPrescriptionUpdateSDOICD(InPatientPresSDO prescriptionSDO)
        {
            try
            {
                prescriptionSDO.IcdName = this.IcdName;
                prescriptionSDO.IcdCode = this.IcdCode;
                prescriptionSDO.IcdCauseName = this.IcdCauseName;
                prescriptionSDO.IcdCauseCode = this.IcdCauseCode;
                prescriptionSDO.IcdText = this.IcdText;
                prescriptionSDO.IcdSubCode = this.IcdSubCode;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }        
    }
}
