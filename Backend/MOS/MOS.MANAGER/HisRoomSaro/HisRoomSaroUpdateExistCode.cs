using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRoomSaro
{
    partial class HisRoomSaroUpdate : BusinessBase
    {
		private List<HIS_ROOM_SARO> beforeUpdateHisRoomSaros = new List<HIS_ROOM_SARO>();
		
        internal HisRoomSaroUpdate()
            : base()
        {

        }

        internal HisRoomSaroUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ROOM_SARO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRoomSaroCheck checker = new HisRoomSaroCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ROOM_SARO raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ROOM_SARO_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisRoomSaroDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRoomSaro_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRoomSaro that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisRoomSaros.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_ROOM_SARO> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRoomSaroCheck checker = new HisRoomSaroCheck(param);
                List<HIS_ROOM_SARO> listRaw = new List<HIS_ROOM_SARO>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ROOM_SARO_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisRoomSaroDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRoomSaro_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRoomSaro that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisRoomSaros.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisRoomSaros))
            {
                if (!DAOWorker.HisRoomSaroDAO.UpdateList(this.beforeUpdateHisRoomSaros))
                {
                    LogSystem.Warn("Rollback du lieu HisRoomSaro that bai, can kiem tra lai." + LogUtil.TraceData("HisRoomSaros", this.beforeUpdateHisRoomSaros));
                }
				this.beforeUpdateHisRoomSaros = null;
            }
        }
    }
}
