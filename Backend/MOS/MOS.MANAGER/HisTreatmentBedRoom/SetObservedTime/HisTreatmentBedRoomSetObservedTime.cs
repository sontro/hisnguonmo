using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPatientObservation;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatmentBedRoom.SetObservedTime
{
    class HisTreatmentBedRoomSetObservedTime : BusinessBase
    {
        private HisPatientObservationTruncate patientObservationTruncate;
        private HisPatientObservationCreate patientObservationCreate;
        private HisTreatmentBedRoomUpdate treatmentBedRoomUpdate;
        private List<HIS_PATIENT_OBSERVATION> beforePatientObservations;

        internal HisTreatmentBedRoomSetObservedTime()
            : base()
        {
            this.Init();
        }

        internal HisTreatmentBedRoomSetObservedTime(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.patientObservationTruncate = new HisPatientObservationTruncate();
            this.patientObservationCreate = new HisPatientObservationCreate();
            this.treatmentBedRoomUpdate = new HisTreatmentBedRoomUpdate();
            this.beforePatientObservations = new List<HIS_PATIENT_OBSERVATION>();
        }

        internal bool Run(ObservedTimeSDO data)
        {
            bool result = false;
            try
            {
                HIS_TREATMENT_BED_ROOM raw = null;
                HisTreatmentBedRoomCheck checker = new HisTreatmentBedRoomCheck(param);

                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && checker.VerifyId(data.TreatmentBedRoomId, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && IsGreaterThanZero(data.ObservedTimeFrom) && IsGreaterThanZero(data.ObservedTimeTo) && data.ObservedTimeFrom <= data.ObservedTimeTo;
                if (valid)
                {
                    // Bo theo doi
                    if (data.IsUnobserved)
                    {
                        HisPatientObservationFilterQuery filter = new HisPatientObservationFilterQuery();
                        filter.TREATMENT_BED_ROOM_ID = raw.ID;
                        filter.OBSERVED_TIME_FROM = data.ObservedTimeFrom;
                        filter.OBSERVED_TIME_TO = data.ObservedTimeTo;
                        var observations = new HisPatientObservationGet().Get(filter);
                        if (IsNotNullOrEmpty(observations))
                        {
                            // Xoa HIS_PATIENT_OBSERVATION
                            this.beforePatientObservations.AddRange(observations);
                            if (!this.patientObservationTruncate.TruncateList(observations))
                            {
                                throw new Exception("Xoa du lieu thoi gian theo doi khi bo theo doi that bai. Ket thuc nghiep vu. Rollback");
                            }

                            // CAp nhat HIS_TREATMENT_BED_ROOM
                            List<HIS_PATIENT_OBSERVATION> poMaxId = new HisPatientObservationGet().GetByTreatmentBedRoomId(raw.ID);

                            raw.TDL_OBSERVED_TIME_FROM = IsNotNullOrEmpty(poMaxId) ? (long?)poMaxId.OrderByDescending(o => o.ID).FirstOrDefault().OBSERVED_TIME_FROM : null;
                            raw.TDL_OBSERVED_TIME_TO = IsNotNullOrEmpty(poMaxId) ? (long?)poMaxId.OrderByDescending(o => o.ID).FirstOrDefault().OBSERVED_TIME_TO : null;
                        }
                        else
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                            LogSystem.Error(string.Format("Khong the lay duoc du lieu thoi gian theo doi voi filter gui len(HisPatientObservation): data.TreatmentBedRoomId: {0}, data.ObservedTimeFrom: {1}, data.ObservedTimeTo: {2}", data.TreatmentBedRoomId, data.ObservedTimeFrom, data.ObservedTimeTo));
                        }
                    }
                    else
                    {
                        // Tao moi HIS_PATIENT_OBSERVATION
                        HIS_PATIENT_OBSERVATION toNew = new HIS_PATIENT_OBSERVATION();
                        toNew.TREATMENT_BED_ROOM_ID = raw.ID;
                        toNew.OBSERVED_TIME_FROM = data.ObservedTimeFrom;
                        toNew.OBSERVED_TIME_TO = data.ObservedTimeTo;
                        if (!this.patientObservationCreate.Create(toNew))
                        {
                            throw new Exception("Tao moi du lieu thoi gian theo doi that bai. Ket thuc nghiep vu. Rollback");
                        }

                        // CAp nhat HIS_TREATMENT_BED_ROOM
                        raw.TDL_OBSERVED_TIME_FROM = toNew.OBSERVED_TIME_FROM;
                        raw.TDL_OBSERVED_TIME_TO = toNew.OBSERVED_TIME_TO;
                    }

                    if (!this.treatmentBedRoomUpdate.Update(raw))
                    {
                        throw new Exception("Cap nhat thong tin buong benh khi bo theo doi that bai. Ket thuc nghiep vu. Rollback");
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        public void RollbackData()
        {
            if (this.beforePatientObservations != null)
            {
                if (!DAOWorker.HisPatientObservationDAO.CreateList(this.beforePatientObservations))
                {
                    LogSystem.Warn("Rollback du lieu HisPatientObservation that bai, can kiem tra lai." + LogUtil.TraceData("HisPatientObservation", this.beforePatientObservations));
                }
            }
            this.patientObservationCreate.RollbackData();
            this.treatmentBedRoomUpdate.RollbackData();
        }
    }
}
