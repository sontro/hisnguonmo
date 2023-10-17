using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRoomGroup
{
    partial class HisRoomGroupUpdate : BusinessBase
    {
		private List<HIS_ROOM_GROUP> beforeUpdateHisRoomGroups = new List<HIS_ROOM_GROUP>();
		
        internal HisRoomGroupUpdate()
            : base()
        {

        }

        internal HisRoomGroupUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ROOM_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRoomGroupCheck checker = new HisRoomGroupCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ROOM_GROUP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ROOM_GROUP_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisRoomGroupDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRoomGroup_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRoomGroup that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisRoomGroups.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_ROOM_GROUP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRoomGroupCheck checker = new HisRoomGroupCheck(param);
                List<HIS_ROOM_GROUP> listRaw = new List<HIS_ROOM_GROUP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ROOM_GROUP_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisRoomGroups.AddRange(listRaw);
					if (!DAOWorker.HisRoomGroupDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRoomGroup_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRoomGroup that bai." + LogUtil.TraceData("listData", listData));
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
		
		internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisRoomGroups))
            {
                if (!DAOWorker.HisRoomGroupDAO.UpdateList(this.beforeUpdateHisRoomGroups))
                {
                    LogSystem.Warn("Rollback du lieu HisRoomGroup that bai, can kiem tra lai." + LogUtil.TraceData("HisRoomGroups", this.beforeUpdateHisRoomGroups));
                }
				this.beforeUpdateHisRoomGroups = null;
            }
        }
    }
}
