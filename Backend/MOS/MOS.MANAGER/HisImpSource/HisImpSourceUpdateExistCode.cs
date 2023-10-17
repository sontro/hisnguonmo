using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpSource
{
    partial class HisImpSourceUpdate : BusinessBase
    {
		private List<HIS_IMP_SOURCE> beforeUpdateHisImpSources = new List<HIS_IMP_SOURCE>();
		
        internal HisImpSourceUpdate()
            : base()
        {

        }

        internal HisImpSourceUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_IMP_SOURCE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpSourceCheck checker = new HisImpSourceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_IMP_SOURCE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.IMP_SOURCE_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisImpSources.Add(raw);
					if (!DAOWorker.HisImpSourceDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpSource_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpSource that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_IMP_SOURCE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpSourceCheck checker = new HisImpSourceCheck(param);
                List<HIS_IMP_SOURCE> listRaw = new List<HIS_IMP_SOURCE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.IMP_SOURCE_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisImpSources.AddRange(listRaw);
					if (!DAOWorker.HisImpSourceDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpSource_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpSource that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisImpSources))
            {
                if (!new HisImpSourceUpdate(param).UpdateList(this.beforeUpdateHisImpSources))
                {
                    LogSystem.Warn("Rollback du lieu HisImpSource that bai, can kiem tra lai." + LogUtil.TraceData("HisImpSources", this.beforeUpdateHisImpSources));
                }
            }
        }
    }
}
