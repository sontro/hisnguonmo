using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.MANAGER.AcsAuthorSystem
{
    partial class AcsAuthorSystemUpdate : BusinessBase
    {
		private List<ACS_AUTHOR_SYSTEM> beforeUpdateAcsAuthorSystems = new List<ACS_AUTHOR_SYSTEM>();
		
        internal AcsAuthorSystemUpdate()
            : base()
        {

        }

        internal AcsAuthorSystemUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(ACS_AUTHOR_SYSTEM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                AcsAuthorSystemCheck checker = new AcsAuthorSystemCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                ACS_AUTHOR_SYSTEM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.AcsAuthorSystemDAO.Update(data))
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsAuthorSystem_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin AcsAuthorSystem that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateAcsAuthorSystems.Add(raw);
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

        internal bool UpdateList(List<ACS_AUTHOR_SYSTEM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                AcsAuthorSystemCheck checker = new AcsAuthorSystemCheck(param);
                List<ACS_AUTHOR_SYSTEM> listRaw = new List<ACS_AUTHOR_SYSTEM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.AcsAuthorSystemDAO.UpdateList(listData))
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsAuthorSystem_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin AcsAuthorSystem that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateAcsAuthorSystems.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateAcsAuthorSystems))
            {
                if (!DAOWorker.AcsAuthorSystemDAO.UpdateList(this.beforeUpdateAcsAuthorSystems))
                {
                    LogSystem.Warn("Rollback du lieu AcsAuthorSystem that bai, can kiem tra lai." + LogUtil.TraceData("AcsAuthorSystems", this.beforeUpdateAcsAuthorSystems));
                }
				this.beforeUpdateAcsAuthorSystems = null;
            }
        }
    }
}
