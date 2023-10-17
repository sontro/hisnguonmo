using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using MOS.MANAGER.HisEventsCausesDeath;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSevereIllnessInfo
{
    partial class HisSevereIllnessInfoTruncate : BusinessBase
    {
        private List<HIS_SEVERE_ILLNESS_INFO> beforeTruncateHisSevereIllnessInfos = new List<HIS_SEVERE_ILLNESS_INFO>();
        internal HisSevereIllnessInfoTruncate()
            : base()
        {

        }

        internal HisSevereIllnessInfoTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSevereIllnessInfoCheck checker = new HisSevereIllnessInfoCheck(param);
                HIS_SEVERE_ILLNESS_INFO raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    this.beforeTruncateHisSevereIllnessInfos.Add(raw);
                    var oldChildren = new HisEventsCausesDeathGet().GetBySevereIllnessInfoId(id);
                    if (IsNotNullOrEmpty(oldChildren))
                    {
                        if (!DAOWorker.HisEventsCausesDeathDAO.TruncateList(oldChildren))
                        {
                            throw new Exception("Xoa lieu cu HisEventsCausesDeath theo HisSevereIllnessInfo that bai." + LogUtil.TraceData("severeIllnessInfoId", id));
                        }
                    }
                    result = DAOWorker.HisSevereIllnessInfoDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_SEVERE_ILLNESS_INFO> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSevereIllnessInfoCheck checker = new HisSevereIllnessInfoCheck(param);
                List<HIS_SEVERE_ILLNESS_INFO> listRaw = new List<HIS_SEVERE_ILLNESS_INFO>();
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
                    this.beforeTruncateHisSevereIllnessInfos.AddRange(listData);
                    result = DAOWorker.HisSevereIllnessInfoDAO.TruncateList(listData);
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
            if (IsNotNullOrEmpty(this.beforeTruncateHisSevereIllnessInfos))
            {
                if (!DAOWorker.HisSevereIllnessInfoDAO.CreateList(this.beforeTruncateHisSevereIllnessInfos))
                {
                    LogSystem.Warn("Rollback du lieu HisSevereIllnessInfo that bai, can kiem tra lai." + LogUtil.TraceData("HisSevereIllnessInfos", this.beforeTruncateHisSevereIllnessInfos));
                }
            }
        }
    }
}
