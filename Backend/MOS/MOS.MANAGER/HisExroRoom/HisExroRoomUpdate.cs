using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExroRoom
{
    partial class HisExroRoomUpdate : BusinessBase
    {
		private List<HIS_EXRO_ROOM> beforeUpdateHisExroRooms = new List<HIS_EXRO_ROOM>();
		
        internal HisExroRoomUpdate()
            : base()
        {

        }

        internal HisExroRoomUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXRO_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExroRoomCheck checker = new HisExroRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EXRO_ROOM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisExroRoomDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExroRoom_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExroRoom that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisExroRooms.Add(raw);
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

        internal bool UpdateList(List<HIS_EXRO_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExroRoomCheck checker = new HisExroRoomCheck(param);
                List<HIS_EXRO_ROOM> listRaw = new List<HIS_EXRO_ROOM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisExroRoomDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExroRoom_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExroRoom that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisExroRooms.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisExroRooms))
            {
                if (!DAOWorker.HisExroRoomDAO.UpdateList(this.beforeUpdateHisExroRooms))
                {
                    LogSystem.Warn("Rollback du lieu HisExroRoom that bai, can kiem tra lai." + LogUtil.TraceData("HisExroRooms", this.beforeUpdateHisExroRooms));
                }
				this.beforeUpdateHisExroRooms = null;
            }
        }
    }
}
