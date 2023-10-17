using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSeseDepoRepay
{
    partial class HisSeseDepoRepayUpdate : BusinessBase
    {
        private List<HIS_SESE_DEPO_REPAY> beforeUpdateHisSeseDepoRepays = new List<HIS_SESE_DEPO_REPAY>();

        internal HisSeseDepoRepayUpdate()
            : base()
        {

        }

        internal HisSeseDepoRepayUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SESE_DEPO_REPAY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSeseDepoRepayCheck checker = new HisSeseDepoRepayCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SESE_DEPO_REPAY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisSeseDepoRepays.Add(raw);
                    if (!DAOWorker.HisSeseDepoRepayDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSeseDepoRepay_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSeseDepoRepay that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_SESE_DEPO_REPAY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSeseDepoRepayCheck checker = new HisSeseDepoRepayCheck(param);
                List<HIS_SESE_DEPO_REPAY> listRaw = new List<HIS_SESE_DEPO_REPAY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisSeseDepoRepays.AddRange(listRaw);
                    if (!DAOWorker.HisSeseDepoRepayDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSeseDepoRepay_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSeseDepoRepay that bai." + LogUtil.TraceData("listData", listData));
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

        internal bool UpdateList(List<HIS_SESE_DEPO_REPAY> listData, List<HIS_SESE_DEPO_REPAY> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSeseDepoRepayCheck checker = new HisSeseDepoRepayCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisSeseDepoRepays.AddRange(listBefore);
                    if (!DAOWorker.HisSeseDepoRepayDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSeseDepoRepay_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSeseDepoRepay that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSeseDepoRepays))
            {
                if (!DAOWorker.HisSeseDepoRepayDAO.UpdateList(this.beforeUpdateHisSeseDepoRepays))
                {
                    LogSystem.Warn("Rollback du lieu HisSeseDepoRepay that bai, can kiem tra lai." + LogUtil.TraceData("HisSeseDepoRepays", this.beforeUpdateHisSeseDepoRepays));
                }
            }
        }
    }
}
