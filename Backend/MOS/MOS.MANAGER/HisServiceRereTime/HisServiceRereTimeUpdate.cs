using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceRereTime
{
    partial class HisServiceRereTimeUpdate : BusinessBase
    {
		private List<HIS_SERVICE_RERE_TIME> beforeUpdateHisServiceRereTimes = new List<HIS_SERVICE_RERE_TIME>();
		
        internal HisServiceRereTimeUpdate()
            : base()
        {

        }

        internal HisServiceRereTimeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERVICE_RERE_TIME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceRereTimeCheck checker = new HisServiceRereTimeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERVICE_RERE_TIME raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisServiceRereTimeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceRereTime_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceRereTime that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisServiceRereTimes.Add(raw);
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

        internal bool UpdateList(List<HIS_SERVICE_RERE_TIME> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceRereTimeCheck checker = new HisServiceRereTimeCheck(param);
                List<HIS_SERVICE_RERE_TIME> listRaw = new List<HIS_SERVICE_RERE_TIME>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisServiceRereTimeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceRereTime_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceRereTime that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisServiceRereTimes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisServiceRereTimes))
            {
                if (!DAOWorker.HisServiceRereTimeDAO.UpdateList(this.beforeUpdateHisServiceRereTimes))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceRereTime that bai, can kiem tra lai." + LogUtil.TraceData("HisServiceRereTimes", this.beforeUpdateHisServiceRereTimes));
                }
				this.beforeUpdateHisServiceRereTimes = null;
            }
        }
    }
}
