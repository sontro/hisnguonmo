using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisBed;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.HisTreatment;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentBedRoom
{
    partial class HisTreatmentBedRoomCreate : BusinessBase
    {
        private HIS_TREATMENT_BED_ROOM recentHisTreatmentBedRoom;
        private HisBedLogCreate hisBedLogCreate;

        internal HisTreatmentBedRoomCreate()
            : base()
        {
            this.hisBedLogCreate = new HisBedLogCreate(param);
        }

        internal HisTreatmentBedRoomCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisBedLogCreate = new HisBedLogCreate(param);
        }

        internal bool Create(HIS_TREATMENT_BED_ROOM data)
        {
            return this.Create(data, null, null, null, null);
        }

        internal bool Create(HIS_TREATMENT_BED_ROOM data, long? bedServiceTypeId, long? shareCount, long? patientTypeId, long? primaryPatientTypeId)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;

                data.ADD_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                data.ADD_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                HisTreatmentBedRoomCheck checker = new HisTreatmentBedRoomCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ValidTreatmentTypeCode(data);
                valid = valid && treatmentChecker.VerifyId(data.TREATMENT_ID, ref treatment);
                valid = valid && checker.IsNotInRoom(data);
                valid = valid && checker.IsExistsDepartmentTran(data);
                valid = valid && checker.IsValidTime(data, treatment);
                if (valid)
                {
                    if (!DAOWorker.HisTreatmentBedRoomDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentBedRoom_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTreatmentBedRoom that bai." + LogUtil.TraceData("data", data));
                    }
                    else
                    {
                        this.ProcessBedLog(data, treatment, bedServiceTypeId, shareCount, patientTypeId, primaryPatientTypeId);
                    }

                    this.recentHisTreatmentBedRoom = data;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessBedLog(HIS_TREATMENT_BED_ROOM data, HIS_TREATMENT treatment, long? bedServiceTypeId, long? shareCount, long? patientTypeId, long? primaryPatientTypeId)
        {
            if (data.BED_ID.HasValue)
            {
                HIS_BED_LOG bedLog = new HIS_BED_LOG();
                bedLog.BED_ID = data.BED_ID.Value;
                bedLog.TREATMENT_BED_ROOM_ID = data.ID;
                bedLog.START_TIME = data.ADD_TIME;
                bedLog.BED_SERVICE_TYPE_ID = bedServiceTypeId;
                bedLog.SHARE_COUNT = shareCount;
                bedLog.PATIENT_TYPE_ID = patientTypeId;
                bedLog.PRIMARY_PATIENT_TYPE_ID = primaryPatientTypeId;

                if (!this.hisBedLogCreate.Create(bedLog, treatment))
                {
                    throw new Exception("Rollback du lieu");
                }
            }
        }

        internal void RollbackData()
        {
            if (this.recentHisTreatmentBedRoom != null)
            {
                if (!DAOWorker.HisTreatmentBedRoomDAO.Truncate(this.recentHisTreatmentBedRoom))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatmentBedRoom that bai, can kiem tra lai." + LogUtil.TraceData("HisTreatmentBedRoom", this.recentHisTreatmentBedRoom));
                }
            }
            this.hisBedLogCreate.RollbackData();
        }
    }
}
