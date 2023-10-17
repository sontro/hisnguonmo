using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRationTime
{
    partial class HisRationTimeUpdate : BusinessBase
    {
		private List<HIS_RATION_TIME> beforeUpdateHisRationTimes = new List<HIS_RATION_TIME>();
		
        internal HisRationTimeUpdate()
            : base()
        {

        }

        internal HisRationTimeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_RATION_TIME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRationTimeCheck checker = new HisRationTimeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_RATION_TIME raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.RATION_TIME_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisRationTimeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationTime_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRationTime that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisRationTimes.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_RATION_TIME> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRationTimeCheck checker = new HisRationTimeCheck(param);
                List<HIS_RATION_TIME> listRaw = new List<HIS_RATION_TIME>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.RATION_TIME_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisRationTimeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationTime_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRationTime that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisRationTimes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisRationTimes))
            {
                if (!DAOWorker.HisRationTimeDAO.UpdateList(this.beforeUpdateHisRationTimes))
                {
                    LogSystem.Warn("Rollback du lieu HisRationTime that bai, can kiem tra lai." + LogUtil.TraceData("HisRationTimes", this.beforeUpdateHisRationTimes));
                }
				this.beforeUpdateHisRationTimes = null;
            }
        }
    }
}
