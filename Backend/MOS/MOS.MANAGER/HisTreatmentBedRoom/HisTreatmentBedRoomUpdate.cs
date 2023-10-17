using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.HisBedLog.Update;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentBedRoom
{
    partial class HisTreatmentBedRoomUpdate : BusinessBase
    {
        private List<HIS_TREATMENT_BED_ROOM> beforeUpdateHisTreatmentBedRooms = new List<HIS_TREATMENT_BED_ROOM>();
        private HisBedLogUpdate hisBedLogUpdate;

        internal HisTreatmentBedRoomUpdate()
            : base()
        {
            this.hisBedLogUpdate = new HisBedLogUpdate(param);
        }

        internal HisTreatmentBedRoomUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.hisBedLogUpdate = new HisBedLogUpdate(param);
        }

        internal bool Update(HIS_TREATMENT_BED_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentBedRoomCheck checker = new HisTreatmentBedRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_TREATMENT_BED_ROOM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!DAOWorker.HisTreatmentBedRoomDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentBedRoom_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatmentBedRoom that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisTreatmentBedRooms.Add(raw);
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

        internal bool UpdateList(List<HIS_TREATMENT_BED_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentBedRoomCheck checker = new HisTreatmentBedRoomCheck(param);
                List<HIS_TREATMENT_BED_ROOM> listRaw = new List<HIS_TREATMENT_BED_ROOM>();
                
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisTreatmentBedRoomDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentBedRoom_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatmentBedRoom that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisTreatmentBedRooms.AddRange(listRaw);
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

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisTreatmentBedRooms))
            {
                if (!DAOWorker.HisTreatmentBedRoomDAO.UpdateList(this.beforeUpdateHisTreatmentBedRooms))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatmentBedRoom that bai, can kiem tra lai." + LogUtil.TraceData("beforeUpdateHisTreatmentBedRooms", this.beforeUpdateHisTreatmentBedRooms));
                }
                this.beforeUpdateHisTreatmentBedRooms = null;//tranh rollback 2 lan
            }
        }

    }
}
