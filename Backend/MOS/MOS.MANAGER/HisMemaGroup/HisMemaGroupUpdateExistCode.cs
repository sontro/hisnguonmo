using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMemaGroup
{
    partial class HisMemaGroupUpdate : BusinessBase
    {
		private List<HIS_MEMA_GROUP> beforeUpdateHisMemaGroups = new List<HIS_MEMA_GROUP>();
		
        internal HisMemaGroupUpdate()
            : base()
        {

        }

        internal HisMemaGroupUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEMA_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMemaGroupCheck checker = new HisMemaGroupCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEMA_GROUP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.MEMA_GROUP_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisMemaGroups.Add(raw);
					if (!DAOWorker.HisMemaGroupDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMemaGroup_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMemaGroup that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_MEMA_GROUP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMemaGroupCheck checker = new HisMemaGroupCheck(param);
                List<HIS_MEMA_GROUP> listRaw = new List<HIS_MEMA_GROUP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MEMA_GROUP_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisMemaGroups.AddRange(listRaw);
					if (!DAOWorker.HisMemaGroupDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMemaGroup_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMemaGroup that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMemaGroups))
            {
                if (!new HisMemaGroupUpdate(param).UpdateList(this.beforeUpdateHisMemaGroups))
                {
                    LogSystem.Warn("Rollback du lieu HisMemaGroup that bai, can kiem tra lai." + LogUtil.TraceData("HisMemaGroups", this.beforeUpdateHisMemaGroups));
                }
            }
        }
    }
}
