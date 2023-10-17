using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEventsCausesDeath
{
    partial class HisEventsCausesDeathTruncate : BusinessBase
    {
        private List<HIS_EVENTS_CAUSES_DEATH> beforeTruncateHisEventsCausesDeaths = new List<HIS_EVENTS_CAUSES_DEATH>();
        internal HisEventsCausesDeathTruncate()
            : base()
        {

        }

        internal HisEventsCausesDeathTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEventsCausesDeathCheck checker = new HisEventsCausesDeathCheck(param);
                HIS_EVENTS_CAUSES_DEATH raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    this.beforeTruncateHisEventsCausesDeaths.Add(raw);
                    result = DAOWorker.HisEventsCausesDeathDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_EVENTS_CAUSES_DEATH> listData)
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
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    this.beforeTruncateHisEventsCausesDeaths.AddRange(listRaw);
                    result = DAOWorker.HisEventsCausesDeathDAO.TruncateList(listData);
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
            if (IsNotNullOrEmpty(this.beforeTruncateHisEventsCausesDeaths))
            {
                if (!DAOWorker.HisEventsCausesDeathDAO.CreateList(this.beforeTruncateHisEventsCausesDeaths))
                {
                    LogSystem.Warn("Rollback du lieu HisEventsCausesDeath that bai, can kiem tra lai." + LogUtil.TraceData("HisEventsCausesDeaths", this.beforeTruncateHisEventsCausesDeaths));
                }
            }
        }
    }
}
