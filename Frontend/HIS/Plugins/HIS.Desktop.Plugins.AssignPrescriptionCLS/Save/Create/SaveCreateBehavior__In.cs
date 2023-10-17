using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Plugins.AssignPrescriptionCLS.AssignPrescription;
using Inventec.Common.Logging;
using MOS.SDO;
using System;

namespace HIS.Desktop.Plugins.AssignPrescriptionCLS.Save.Create
{
    partial class SaveCreateBehavior : SaveAbstract, ISave
    {
        object Run__In()
        {
            InPatientPresResultSDO result = null;
            if (this.CheckValid())
            {
                frmAssignPrescription.VerifyWarningOverCeiling();
                this.InitBase();

                InPatientPresSDO prescriptionSDO = new InPatientPresSDO();
                prescriptionSDO.Medicines = this.InPatientPresMedicineSDOs;
                prescriptionSDO.Materials = this.InPatientPresMaterialSDOs;
                prescriptionSDO.ServiceReqMaties = this.PresOutStockMatySDOs;
                prescriptionSDO.ServiceReqMeties = this.PresOutStockMetySDOs;
                prescriptionSDO.SerialNumbers = this.PatientPresMaterialBySerialNumberSDOs;
                prescriptionSDO.PrescriptionTypeId = PrescriptionType.NEW;
                prescriptionSDO.TreatmentId = this.TreatmentId;

                this.ProcessPrescriptionUpdateSDO(prescriptionSDO);
                this.ProcessPrescriptionUpdateSDOICD(prescriptionSDO);
                this.ProcessPrescriptionSDOForSereServInKip(prescriptionSDO);
                LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => prescriptionSDO), prescriptionSDO));
                result = new Inventec.Common.Adapter.BackendAdapter(Param).Post<InPatientPresResultSDO>(RequestUriStore.HIS_SERVICE_REQ__IN_PATIENT_PRES_CREATE, ApiConsumers.MosConsumer, prescriptionSDO, Param);
                if (result == null
                    || result.ServiceReqs == null || result.ServiceReqs.Count == 0
                    || ((result.ServiceReqMaties == null || result.ServiceReqMaties.Count == 0)
                        && (result.ServiceReqMeties == null || result.ServiceReqMeties.Count == 0)
                        && (result.Materials == null || result.Materials.Count == 0)
                        && (result.Medicines == null || result.Medicines.Count == 0))
                    )
                {
                    Inventec.Common.Logging.LogSystem.Debug("Goi api ke don thuoc that bai. Du lieu dau vao____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => prescriptionSDO), prescriptionSDO) + ". Du lieu dau ra____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result), result) + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => Param), Param));
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
                prescriptionSDO.ExpMestReasonId = this.ExpMestReasonId;
                if (this.ParentServiceReqId > 0)
                    prescriptionSDO.ParentServiceReqId = this.ParentServiceReqId;
                prescriptionSDO.InstructionTimes = this.InstructionTimes;            
                prescriptionSDO.IsHomePres = this.IsHomePres;
                prescriptionSDO.IsKidney = this.IsKidney;
                prescriptionSDO.KidneyTimes = this.KidneyTimes;
                prescriptionSDO.IsExecuteKidneyPres = this.IsExecuteKidneyPres;
                prescriptionSDO.ProvisionalDiagnosis = this.ProvisionalDiagnosis;
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

        private void ProcessPrescriptionSDOForSereServInKip(InPatientPresSDO prescriptionSDO)
        {
            try
            {
                if (prescriptionSDO.Materials.Count > 0
                    || prescriptionSDO.Medicines.Count > 0
                    )
                {
                    if (frmAssignPrescription.currentSereServ != null)
                    {
                        foreach (var item in prescriptionSDO.Materials)
                        {
                            item.SereServParentId = frmAssignPrescription.currentSereServ.ID;
                        }

                        foreach (var item in prescriptionSDO.Medicines)
                        {
                            item.SereServParentId = frmAssignPrescription.currentSereServ.ID;
                        }
                    }

                    if (frmAssignPrescription.currentSereServInEkip != null)
                    {
                        foreach (var item in prescriptionSDO.Materials)
                        {
                            item.SereServParentId = frmAssignPrescription.currentSereServInEkip.ID;
                        }

                        foreach (var item in prescriptionSDO.Medicines)
                        {
                            item.SereServParentId = frmAssignPrescription.currentSereServInEkip.ID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

    }
}
