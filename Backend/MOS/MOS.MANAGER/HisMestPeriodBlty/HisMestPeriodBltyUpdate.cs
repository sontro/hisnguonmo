using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMestPeriodBlty
{
    partial class HisMestPeriodBltyUpdate : BusinessBase
    {
		private List<HIS_MEST_PERIOD_BLTY> beforeUpdateHisMestPeriodBltys = new List<HIS_MEST_PERIOD_BLTY>();
		
        internal HisMestPeriodBltyUpdate()
            : base()
        {

        }

        internal HisMestPeriodBltyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEST_PERIOD_BLTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPeriodBltyCheck checker = new HisMestPeriodBltyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEST_PERIOD_BLTY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisMestPeriodBltys.Add(raw);
					if (!DAOWorker.HisMestPeriodBltyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPeriodBlty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMestPeriodBlty that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_MEST_PERIOD_BLTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPeriodBltyCheck checker = new HisMestPeriodBltyCheck(param);
                List<HIS_MEST_PERIOD_BLTY> listRaw = new List<HIS_MEST_PERIOD_BLTY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisMestPeriodBltys.AddRange(listRaw);
					if (!DAOWorker.HisMestPeriodBltyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPeriodBlty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMestPeriodBlty that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMestPeriodBltys))
            {
                if (!new HisMestPeriodBltyUpdate(param).UpdateList(this.beforeUpdateHisMestPeriodBltys))
                {
                    LogSystem.Warn("Rollback du lieu HisMestPeriodBlty that bai, can kiem tra lai." + LogUtil.TraceData("HisMestPeriodBltys", this.beforeUpdateHisMestPeriodBltys));
                }
            }
        }
    }
}
