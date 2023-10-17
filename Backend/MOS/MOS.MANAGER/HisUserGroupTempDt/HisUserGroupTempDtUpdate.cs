using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisUserGroupTempDt
{
    partial class HisUserGroupTempDtUpdate : BusinessBase
    {
		private List<HIS_USER_GROUP_TEMP_DT> beforeUpdateHisUserGroupTempDts = new List<HIS_USER_GROUP_TEMP_DT>();
		
        internal HisUserGroupTempDtUpdate()
            : base()
        {

        }

        internal HisUserGroupTempDtUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_USER_GROUP_TEMP_DT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUserGroupTempDtCheck checker = new HisUserGroupTempDtCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_USER_GROUP_TEMP_DT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisUserGroupTempDtDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUserGroupTempDt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisUserGroupTempDt that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisUserGroupTempDts.Add(raw);
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

        internal bool UpdateList(List<HIS_USER_GROUP_TEMP_DT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisUserGroupTempDtCheck checker = new HisUserGroupTempDtCheck(param);
                List<HIS_USER_GROUP_TEMP_DT> listRaw = new List<HIS_USER_GROUP_TEMP_DT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisUserGroupTempDtDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUserGroupTempDt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisUserGroupTempDt that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisUserGroupTempDts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisUserGroupTempDts))
            {
                if (!DAOWorker.HisUserGroupTempDtDAO.UpdateList(this.beforeUpdateHisUserGroupTempDts))
                {
                    LogSystem.Warn("Rollback du lieu HisUserGroupTempDt that bai, can kiem tra lai." + LogUtil.TraceData("HisUserGroupTempDts", this.beforeUpdateHisUserGroupTempDts));
                }
				this.beforeUpdateHisUserGroupTempDts = null;
            }
        }
    }
}
