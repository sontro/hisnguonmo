using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentRoom
{
    partial class HisTreatmentRoomUpdate : BusinessBase
    {
		private List<HIS_TREATMENT_ROOM> beforeUpdateHisTreatmentRooms = new List<HIS_TREATMENT_ROOM>();
		
        internal HisTreatmentRoomUpdate()
            : base()
        {

        }

        internal HisTreatmentRoomUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TREATMENT_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentRoomCheck checker = new HisTreatmentRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_TREATMENT_ROOM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.TREATMENT_ROOM_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisTreatmentRoomDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentRoom_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatmentRoom that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisTreatmentRooms.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_TREATMENT_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentRoomCheck checker = new HisTreatmentRoomCheck(param);
                List<HIS_TREATMENT_ROOM> listRaw = new List<HIS_TREATMENT_ROOM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TREATMENT_ROOM_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisTreatmentRoomDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentRoom_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatmentRoom that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisTreatmentRooms.AddRange(listRaw);
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
		
		internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisTreatmentRooms))
            {
                if (!DAOWorker.HisTreatmentRoomDAO.UpdateList(this.beforeUpdateHisTreatmentRooms))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatmentRoom that bai, can kiem tra lai." + LogUtil.TraceData("HisTreatmentRooms", this.beforeUpdateHisTreatmentRooms));
                }
				this.beforeUpdateHisTreatmentRooms = null;
            }
        }
    }
}
