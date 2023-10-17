using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRoomTypeModule
{
    partial class HisRoomTypeModuleUpdate : BusinessBase
    {
		private List<HIS_ROOM_TYPE_MODULE> beforeUpdateHisRoomTypeModules = new List<HIS_ROOM_TYPE_MODULE>();
		
        internal HisRoomTypeModuleUpdate()
            : base()
        {

        }

        internal HisRoomTypeModuleUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ROOM_TYPE_MODULE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRoomTypeModuleCheck checker = new HisRoomTypeModuleCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ROOM_TYPE_MODULE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisRoomTypeModules.Add(raw);
					if (!DAOWorker.HisRoomTypeModuleDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRoomTypeModule_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRoomTypeModule that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_ROOM_TYPE_MODULE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRoomTypeModuleCheck checker = new HisRoomTypeModuleCheck(param);
                List<HIS_ROOM_TYPE_MODULE> listRaw = new List<HIS_ROOM_TYPE_MODULE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisRoomTypeModules.AddRange(listRaw);
					if (!DAOWorker.HisRoomTypeModuleDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRoomTypeModule_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRoomTypeModule that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisRoomTypeModules))
            {
                if (!new HisRoomTypeModuleUpdate(param).UpdateList(this.beforeUpdateHisRoomTypeModules))
                {
                    LogSystem.Warn("Rollback du lieu HisRoomTypeModule that bai, can kiem tra lai." + LogUtil.TraceData("HisRoomTypeModules", this.beforeUpdateHisRoomTypeModules));
                }
            }
        }
    }
}
