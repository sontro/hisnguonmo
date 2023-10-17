using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisUserGroupTemp
{
    partial class HisUserGroupTempUpdate : BusinessBase
    {
		private List<HIS_USER_GROUP_TEMP> beforeUpdateHisUserGroupTemps = new List<HIS_USER_GROUP_TEMP>();
		
        internal HisUserGroupTempUpdate()
            : base()
        {

        }

        internal HisUserGroupTempUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_USER_GROUP_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUserGroupTempCheck checker = new HisUserGroupTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_USER_GROUP_TEMP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisUserGroupTempDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUserGroupTemp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisUserGroupTemp that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisUserGroupTemps.Add(raw);
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

        internal bool UpdateList(List<HIS_USER_GROUP_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisUserGroupTempCheck checker = new HisUserGroupTempCheck(param);
                List<HIS_USER_GROUP_TEMP> listRaw = new List<HIS_USER_GROUP_TEMP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisUserGroupTempDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUserGroupTemp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisUserGroupTemp that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisUserGroupTemps.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisUserGroupTemps))
            {
                if (!DAOWorker.HisUserGroupTempDAO.UpdateList(this.beforeUpdateHisUserGroupTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisUserGroupTemp that bai, can kiem tra lai." + LogUtil.TraceData("HisUserGroupTemps", this.beforeUpdateHisUserGroupTemps));
                }
				this.beforeUpdateHisUserGroupTemps = null;
            }
        }
    }
}
