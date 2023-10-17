using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpMestTypeUser
{
    partial class HisImpMestTypeUserUpdate : BusinessBase
    {
		private List<HIS_IMP_MEST_TYPE_USER> beforeUpdateHisImpMestTypeUsers = new List<HIS_IMP_MEST_TYPE_USER>();
		
        internal HisImpMestTypeUserUpdate()
            : base()
        {

        }

        internal HisImpMestTypeUserUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_IMP_MEST_TYPE_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestTypeUserCheck checker = new HisImpMestTypeUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_IMP_MEST_TYPE_USER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisImpMestTypeUsers.Add(raw);
					if (!DAOWorker.HisImpMestTypeUserDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestTypeUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestTypeUser that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_IMP_MEST_TYPE_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestTypeUserCheck checker = new HisImpMestTypeUserCheck(param);
                List<HIS_IMP_MEST_TYPE_USER> listRaw = new List<HIS_IMP_MEST_TYPE_USER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisImpMestTypeUsers.AddRange(listRaw);
					if (!DAOWorker.HisImpMestTypeUserDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestTypeUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestTypeUser that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisImpMestTypeUsers))
            {
                if (!new HisImpMestTypeUserUpdate(param).UpdateList(this.beforeUpdateHisImpMestTypeUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisImpMestTypeUser that bai, can kiem tra lai." + LogUtil.TraceData("HisImpMestTypeUsers", this.beforeUpdateHisImpMestTypeUsers));
                }
            }
        }
    }
}
