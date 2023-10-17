using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRationSum
{
    partial class HisRationSumUpdate : BusinessBase
    {
        private List<HIS_RATION_SUM> beforeUpdateHisRationSums = new List<HIS_RATION_SUM>();

        internal HisRationSumUpdate()
            : base()
        {

        }

        internal HisRationSumUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_RATION_SUM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRationSumCheck checker = new HisRationSumCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_RATION_SUM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!DAOWorker.HisRationSumDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationSum_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRationSum that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisRationSums.Add(raw);
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

        internal bool Update(HIS_RATION_SUM data, HIS_RATION_SUM before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRationSumCheck checker = new HisRationSumCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(before);
                if (valid)
                {
                    if (!DAOWorker.HisRationSumDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationSum_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRationSum that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisRationSums.Add(before);
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

        internal bool UpdateList(List<HIS_RATION_SUM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRationSumCheck checker = new HisRationSumCheck(param);
                List<HIS_RATION_SUM> listRaw = new List<HIS_RATION_SUM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisRationSumDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationSum_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRationSum that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisRationSums.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisRationSums))
            {
                if (!DAOWorker.HisRationSumDAO.UpdateList(this.beforeUpdateHisRationSums))
                {
                    LogSystem.Warn("Rollback du lieu HisRationSum that bai, can kiem tra lai." + LogUtil.TraceData("HisRationSums", this.beforeUpdateHisRationSums));
                }
                this.beforeUpdateHisRationSums = null;
            }
        }
    }
}
