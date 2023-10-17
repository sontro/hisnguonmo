using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPatientTypeRoom
{
    partial class HisPatientTypeRoomUpdate : BusinessBase
    {
		private List<HIS_PATIENT_TYPE_ROOM> beforeUpdateHisPatientTypeRooms = new List<HIS_PATIENT_TYPE_ROOM>();
		
        internal HisPatientTypeRoomUpdate()
            : base()
        {

        }

        internal HisPatientTypeRoomUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PATIENT_TYPE_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientTypeRoomCheck checker = new HisPatientTypeRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PATIENT_TYPE_ROOM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisPatientTypeRoomDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientTypeRoom_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPatientTypeRoom that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisPatientTypeRooms.Add(raw);
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

        internal bool UpdateList(List<HIS_PATIENT_TYPE_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPatientTypeRoomCheck checker = new HisPatientTypeRoomCheck(param);
                List<HIS_PATIENT_TYPE_ROOM> listRaw = new List<HIS_PATIENT_TYPE_ROOM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisPatientTypeRoomDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientTypeRoom_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPatientTypeRoom that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisPatientTypeRooms.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPatientTypeRooms))
            {
                if (!DAOWorker.HisPatientTypeRoomDAO.UpdateList(this.beforeUpdateHisPatientTypeRooms))
                {
                    LogSystem.Warn("Rollback du lieu HisPatientTypeRoom that bai, can kiem tra lai." + LogUtil.TraceData("HisPatientTypeRooms", this.beforeUpdateHisPatientTypeRooms));
                }
				this.beforeUpdateHisPatientTypeRooms = null;
            }
        }
    }
}
