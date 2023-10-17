using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisOtherPaySource
{
    partial class HisOtherPaySourceUpdate : BusinessBase
    {
		private List<HIS_OTHER_PAY_SOURCE> beforeUpdateHisOtherPaySources = new List<HIS_OTHER_PAY_SOURCE>();
		
        internal HisOtherPaySourceUpdate()
            : base()
        {

        }

        internal HisOtherPaySourceUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_OTHER_PAY_SOURCE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisOtherPaySourceCheck checker = new HisOtherPaySourceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_OTHER_PAY_SOURCE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.OTHER_PAY_SOURCE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisOtherPaySourceDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisOtherPaySource_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisOtherPaySource that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisOtherPaySources.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_OTHER_PAY_SOURCE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisOtherPaySourceCheck checker = new HisOtherPaySourceCheck(param);
                List<HIS_OTHER_PAY_SOURCE> listRaw = new List<HIS_OTHER_PAY_SOURCE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.OTHER_PAY_SOURCE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisOtherPaySourceDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisOtherPaySource_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisOtherPaySource that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisOtherPaySources.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisOtherPaySources))
            {
                if (!DAOWorker.HisOtherPaySourceDAO.UpdateList(this.beforeUpdateHisOtherPaySources))
                {
                    LogSystem.Warn("Rollback du lieu HisOtherPaySource that bai, can kiem tra lai." + LogUtil.TraceData("HisOtherPaySources", this.beforeUpdateHisOtherPaySources));
                }
				this.beforeUpdateHisOtherPaySources = null;
            }
        }
    }
}
