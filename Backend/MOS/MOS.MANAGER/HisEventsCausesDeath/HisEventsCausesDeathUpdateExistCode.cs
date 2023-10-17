using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEventsCausesDeath
{
    partial class HisEventsCausesDeathUpdate : BusinessBase
    {
		private List<HIS_EVENTS_CAUSES_DEATH> beforeUpdateHisEventsCausesDeaths = new List<HIS_EVENTS_CAUSES_DEATH>();
		
        internal HisEventsCausesDeathUpdate()
            : base()
        {

        }

        internal HisEventsCausesDeathUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EVENTS_CAUSES_DEATH data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEventsCausesDeathCheck checker = new HisEventsCausesDeathCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EVENTS_CAUSES_DEATH raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.EVENTS_CAUSES_DEATH_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisEventsCausesDeathDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEventsCausesDeath_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEventsCausesDeath that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisEventsCausesDeaths.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_EVENTS_CAUSES_DEATH> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEventsCausesDeathCheck checker = new HisEventsCausesDeathCheck(param);
                List<HIS_EVENTS_CAUSES_DEATH> listRaw = new List<HIS_EVENTS_CAUSES_DEATH>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.EVENTS_CAUSES_DEATH_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisEventsCausesDeathDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEventsCausesDeath_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEventsCausesDeath that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisEventsCausesDeaths.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisEventsCausesDeaths))
            {
                if (!DAOWorker.HisEventsCausesDeathDAO.UpdateList(this.beforeUpdateHisEventsCausesDeaths))
                {
                    LogSystem.Warn("Rollback du lieu HisEventsCausesDeath that bai, can kiem tra lai." + LogUtil.TraceData("HisEventsCausesDeaths", this.beforeUpdateHisEventsCausesDeaths));
                }
				this.beforeUpdateHisEventsCausesDeaths = null;
            }
        }
    }
}
