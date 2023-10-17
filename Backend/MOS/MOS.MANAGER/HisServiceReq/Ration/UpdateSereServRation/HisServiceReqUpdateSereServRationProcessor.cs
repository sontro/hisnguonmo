using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisSereServRation;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisServiceReq.Ration;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceRati;
using MOS.MANAGER.HisRationTime;
using MOS.MANAGER.HisServicePaty;
using MOS.MANAGER.Config;
using MOS.ServicePaty;
using MOS.MANAGER.HisRationSum;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Ration
{
    class HisServiceReqUpdateSereServRationProcessor : BusinessBase
    {
        private HisSereServRationCreate hisSereServRationCreate;
        private HisSereServRationUpdate hisSereServRationUpdate;
        private HisSereServRationTruncate hisSereServRationTruncate;
        private List<HIS_SERE_SERV_RATION> currentSSRatitions = new List<HIS_SERE_SERV_RATION>();

        internal HisServiceReqUpdateSereServRationProcessor()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqUpdateSereServRationProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServRationCreate = new HisSereServRationCreate(param);
            this.hisSereServRationUpdate = new HisSereServRationUpdate(param);
            this.hisSereServRationTruncate = new HisSereServRationTruncate(param);
        }

        internal bool Run(HisServiceReqRationUpdateSDO data, ref HisServiceReqRationUpdateResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;

                WorkPlaceSDO workPlace = null;
                HIS_TREATMENT treatment = null;
                HIS_SERVICE_REQ serviceReq = null;
                List<RationRequest> rationRequests = null;
                List<V_HIS_SERVICE_PATY> servicePaties = null;
                Preparer preparer = new Preparer();
                List<HIS_SERE_SERV_RATION> updateSereServRations = new List<HIS_SERE_SERV_RATION>();
                List<HIS_SERE_SERV_RATION> deleteSereServRations = new List<HIS_SERE_SERV_RATION>();
                List<HIS_SERE_SERV_RATION> allOldSereServRations = new List<HIS_SERE_SERV_RATION>();

                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                HisServiceReqCheck srChecker = new HisServiceReqCheck(param);
                HisServiceReqRationCheck srRationChecker = new HisServiceReqRationCheck(param);
                HisServiceReqUpdateSereServRationCheck ssRatitionChecker = new HisServiceReqUpdateSereServRationCheck(param);

                valid = valid && ssRatitionChecker.VerifyRequireField(data);
                valid = valid && this.HasWorkPlaceInfo(data.ExecuteRoomId, ref workPlace);
                valid = valid && srChecker.VerifyId(data.ServiceReqId, ref serviceReq);
                valid = valid && srChecker.IsTypeRation(serviceReq);
                valid = valid && srChecker.HasNoRationSum(serviceReq);
                valid = valid && srChecker.IsAdminOrReqAccount(serviceReq);

                valid = valid && treatChecker.VerifyId(serviceReq.TREATMENT_ID, ref treatment);
                valid = valid && treatChecker.IsUnpause(treatment);
                valid = valid && treatChecker.IsUnLock(treatment);
                valid = valid && treatChecker.IsUnLockHein(treatment);

                valid = valid && ssRatitionChecker.IsValidSereServRatition(data.UpdateServices, data.DeleteSereServRationIds, updateSereServRations, deleteSereServRations, serviceReq);
                valid = valid && ssRatitionChecker.IsTypeRationForCreating(data.InsertServices);
                valid = valid && srRationChecker.IsValidServicePaty(data, workPlace.RoomId, workPlace.DepartmentId, treatment, serviceReq.INTRUCTION_TIME, ref servicePaties);

                valid = valid && preparer.PrepareData(data.InsertServices, serviceReq.INTRUCTION_TIME, ref rationRequests);
                if (valid)
                {
                    allOldSereServRations.AddRange(updateSereServRations);
                    allOldSereServRations.AddRange(deleteSereServRations);
                    this.ProcessNewCreation(treatment, serviceReq, workPlace, rationRequests);
                    this.ProcessUpdate(updateSereServRations, data.UpdateServices, serviceReq, workPlace, treatment);
                    this.ProcessDelete(deleteSereServRations, data.DeleteSereServRationIds);
                    resultData = new HisServiceReqRationUpdateResultSDO();
                    resultData.ServiceReq = serviceReq;
                    resultData.SereServRations = currentSSRatitions;
                    result = true;
                    this.ProcessEventLog(serviceReq, currentSSRatitions, allOldSereServRations);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
                this.Rollback();
            }
            return result;
        }

        private void ProcessEventLog(HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV_RATION> currentSSRatitions, List<HIS_SERE_SERV_RATION> allOldSereServRations)
        {
            if (IsNotNull(serviceReq) && IsNotNullOrEmpty(currentSSRatitions))
            {
                Thread threadUpdate = new Thread(new ParameterizedThreadStart(HisServiceReqRationUtil.UpdateRationLog));
                try
                {
                    UpdateRationThreadData threadData = new UpdateRationThreadData();
                    threadData.ServiceReq = serviceReq;
                    threadData.CurrentSereServRations = currentSSRatitions;
                    threadData.OldSereServRations = allOldSereServRations;
                    threadUpdate.Priority = ThreadPriority.Normal;
                    threadUpdate.Start(threadData);
                }
                catch (Exception ex)
                {
                    threadUpdate.Abort();
                    LogSystem.Error(ex);
                }
            }
        }

        private void ProcessDelete(List<HIS_SERE_SERV_RATION> deleteSereServRations, List<long> deleteSereServRationIds)
        {
            if (IsNotNullOrEmpty(deleteSereServRations) && IsNotNullOrEmpty(deleteSereServRationIds))
            {
                if (!this.hisSereServRationTruncate.TruncateList(deleteSereServRations))
                {
                    throw new Exception("Xoa thong tin suat an that bai khi cap nhat thong tin suat an. Rollback");
                }
            }
        }

        private void ProcessUpdate(List<HIS_SERE_SERV_RATION> beforeUpdateSereServRations, List<RationServiceSDO> updateServices, HIS_SERVICE_REQ serviceReq, WorkPlaceSDO workPlace, HIS_TREATMENT treatment)
        {
            if (IsNotNullOrEmpty(beforeUpdateSereServRations) && IsNotNullOrEmpty(updateServices))
            {
                List<HIS_SERE_SERV_RATION> toUpdate = new List<HIS_SERE_SERV_RATION>();
                foreach (var sdo in updateServices)
                {
                    foreach (var item in sdo.RationTimeIds)
                    {
                        var update = beforeUpdateSereServRations.FirstOrDefault(o => o.ID == sdo.SereServRationId.Value);
                        if (update == null)
                        {
                            throw new Exception("Lay ra thong tin suat an that bai khi cap nhat thong tin suat an. Rollback");
                        }
                        else
                        {
                            update.SERVICE_REQ_ID = serviceReq.ID;
                            update.SERVICE_ID = sdo.ServiceId;
                            update.AMOUNT = sdo.Amount;
                            update.PATIENT_TYPE_ID = sdo.PatientTypeId;
                            update.INSTRUCTION_NOTE = sdo.InstructionNote;
                            toUpdate.Add(update);
                        }
                    }
                }

                if (IsNotNullOrEmpty(toUpdate))
                {
                    if (!this.hisSereServRationUpdate.UpdateList(toUpdate))
                    {
                        throw new Exception("Cap nhat thong tin suat an cu that bai khi cap nhat thong tin suat an. Rollback");
                    }
                    currentSSRatitions.AddRange(toUpdate);
                }
            }
        }

        private void ProcessNewCreation(HIS_TREATMENT treatment, HIS_SERVICE_REQ serviceReq, WorkPlaceSDO workPlace, List<RationRequest> rationRequests)
        {
            try
            {
                List<HIS_SERE_SERV_RATION> toInserts = this.MakeNewData(treatment, serviceReq, workPlace.RoomId, workPlace.DepartmentId, rationRequests);
                if (IsNotNullOrEmpty(toInserts))
                {
                    if (!this.hisSereServRationCreate.CreateList(toInserts))
                    {
                        throw new Exception("Them moi cac suat an that bai khi cap nhat thong tin suat an. Rollback");
                    }
                    currentSSRatitions.AddRange(toInserts);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private List<HIS_SERE_SERV_RATION> MakeNewData(HIS_TREATMENT treatment, HIS_SERVICE_REQ serviceReq, long reqRoomId, long reqDepartmentId, List<RationRequest> rationRequests)
        {
            List<HIS_SERE_SERV_RATION> result = new List<HIS_SERE_SERV_RATION>();

            if (IsNotNullOrEmpty(rationRequests))
            {
                foreach (RationRequest rr in rationRequests)
                {
                    HIS_SERE_SERV_RATION toInsert = new HIS_SERE_SERV_RATION();
                    toInsert.SERVICE_REQ_ID = serviceReq.ID;
                    toInsert.SERVICE_ID = rr.ServiceId;
                    toInsert.AMOUNT = rr.Amount;
                    toInsert.PATIENT_TYPE_ID = rr.PatientTypeId;
                    toInsert.INSTRUCTION_NOTE = rr.InstructionNote;
                    result.Add(toInsert);
                }
            }

            return result;
        }

        internal void Rollback()
        {
            this.hisSereServRationUpdate.RollbackData();
            this.hisSereServRationCreate.RollbackData();
        }
    }
}
