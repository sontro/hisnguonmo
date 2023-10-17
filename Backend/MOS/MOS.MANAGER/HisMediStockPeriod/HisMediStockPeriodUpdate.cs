using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockPeriod
{
    class HisMediStockPeriodUpdate : BusinessBase
    {
        private List<HIS_MEDI_STOCK_PERIOD> beforeUpdateHisMediStockPeriods = new List<HIS_MEDI_STOCK_PERIOD>();

        internal HisMediStockPeriodUpdate()
            : base()
        {

        }

        internal HisMediStockPeriodUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDI_STOCK_PERIOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDI_STOCK_PERIOD raw = null;
                HisMediStockPeriodCheck checker = new HisMediStockPeriodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!DAOWorker.HisMediStockPeriodDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediStockPeriod_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediStockPeriod that bai." + LogUtil.TraceData("data", data));
                    }

                    this.beforeUpdateHisMediStockPeriods.Add(raw);
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

        internal bool Update(HIS_MEDI_STOCK_PERIOD data, HIS_MEDI_STOCK_PERIOD before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDI_STOCK_PERIOD raw = null;
                HisMediStockPeriodCheck checker = new HisMediStockPeriodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(before);
                if (valid)
                {
                    if (!DAOWorker.HisMediStockPeriodDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediStockPeriod_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediStockPeriod that bai." + LogUtil.TraceData("data", data));
                    }

                    this.beforeUpdateHisMediStockPeriods.Add(before);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMediStockPeriods))
            {
                if (!DAOWorker.HisMediStockPeriodDAO.UpdateList(this.beforeUpdateHisMediStockPeriods))
                {
                    LogSystem.Warn("Rollback du lieu HisMediStockPeriod that bai, can kiem tra lai." + LogUtil.TraceData("HisMediStockPeriods", this.beforeUpdateHisMediStockPeriods));
                }
            }
        }
    }
}
