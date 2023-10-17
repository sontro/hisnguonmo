using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRoomTime
{
    partial class HisRoomTimeUpdate : BusinessBase
    {
		private List<HIS_ROOM_TIME> beforeUpdateHisRoomTimes = new List<HIS_ROOM_TIME>();
		
        internal HisRoomTimeUpdate()
            : base()
        {

        }

        internal HisRoomTimeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ROOM_TIME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRoomTimeCheck checker = new HisRoomTimeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ROOM_TIME raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisRoomTimeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRoomTime_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRoomTime that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisRoomTimes.Add(raw);
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

        internal bool UpdateList(List<HIS_ROOM_TIME> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRoomTimeCheck checker = new HisRoomTimeCheck(param);
                List<HIS_ROOM_TIME> listRaw = new List<HIS_ROOM_TIME>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisRoomTimeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRoomTime_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRoomTime that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisRoomTimes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisRoomTimes))
            {
                if (!DAOWorker.HisRoomTimeDAO.UpdateList(this.beforeUpdateHisRoomTimes))
                {
                    LogSystem.Warn("Rollback du lieu HisRoomTime that bai, can kiem tra lai." + LogUtil.TraceData("HisRoomTimes", this.beforeUpdateHisRoomTimes));
                }
				this.beforeUpdateHisRoomTimes = null;
            }
        }
    }
}
