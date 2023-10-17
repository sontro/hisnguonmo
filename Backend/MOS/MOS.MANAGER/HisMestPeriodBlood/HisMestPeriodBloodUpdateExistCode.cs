using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMestPeriodBlood
{
    partial class HisMestPeriodBloodUpdate : BusinessBase
    {
		private List<HIS_MEST_PERIOD_BLOOD> beforeUpdateHisMestPeriodBloods = new List<HIS_MEST_PERIOD_BLOOD>();
		
        internal HisMestPeriodBloodUpdate()
            : base()
        {

        }

        internal HisMestPeriodBloodUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEST_PERIOD_BLOOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPeriodBloodCheck checker = new HisMestPeriodBloodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEST_PERIOD_BLOOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.MEST_PERIOD_BLOOD_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisMestPeriodBloods.Add(raw);
					if (!DAOWorker.HisMestPeriodBloodDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPeriodBlood_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMestPeriodBlood that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_MEST_PERIOD_BLOOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPeriodBloodCheck checker = new HisMestPeriodBloodCheck(param);
                List<HIS_MEST_PERIOD_BLOOD> listRaw = new List<HIS_MEST_PERIOD_BLOOD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MEST_PERIOD_BLOOD_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisMestPeriodBloods.AddRange(listRaw);
					if (!DAOWorker.HisMestPeriodBloodDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPeriodBlood_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMestPeriodBlood that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMestPeriodBloods))
            {
                if (!new HisMestPeriodBloodUpdate(param).UpdateList(this.beforeUpdateHisMestPeriodBloods))
                {
                    LogSystem.Warn("Rollback du lieu HisMestPeriodBlood that bai, can kiem tra lai." + LogUtil.TraceData("HisMestPeriodBloods", this.beforeUpdateHisMestPeriodBloods));
                }
            }
        }
    }
}
