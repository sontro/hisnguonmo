using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediStockMaty
{
    class HisMediStockMatyUpdate : BusinessBase
    {
        private List<HIS_MEDI_STOCK_MATY> recentMediStockMatys = new List<HIS_MEDI_STOCK_MATY>();

        internal HisMediStockMatyUpdate()
            : base()
        {

        }

        internal HisMediStockMatyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDI_STOCK_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDI_STOCK_MATY raw = null;
                HisMediStockMatyCheck checker = new HisMediStockMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!DAOWorker.HisMediStockMatyDAO.Update(data))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisMediStockMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat HIS_MEDI_STOCK_MATY that bai");
                    }
                    result = true;
                    this.recentMediStockMatys.Add(raw);
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

        internal bool UpdateList(List<HIS_MEDI_STOCK_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_MEDI_STOCK_MATY> listRaw = new List<HIS_MEDI_STOCK_MATY>();
                valid = IsNotNullOrEmpty(listData);
                HisMediStockMatyCheck checker = new HisMediStockMatyCheck(param);
                valid = valid && checker.VerifyIds(listData.Select(s => s.ID).ToList(), listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMediStockMatyDAO.UpdateList(listData))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisMediStockMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat HIS_MEDI_STOCK_MATY that bai");
                    }
                    result = true;
                    this.recentMediStockMatys.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_MEDI_STOCK_MATY> listData, List<HIS_MEDI_STOCK_MATY> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediStockMatyCheck checker = new HisMediStockMatyCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMediStockMatyDAO.UpdateList(listData))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisMediStockMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat HIS_MEDI_STOCK_MATY that bai");
                    }
                    result = true;
                    this.recentMediStockMatys.AddRange(listBefore);
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
            try
            {
                if (IsNotNullOrEmpty(this.recentMediStockMatys))
                {
                    if (!DAOWorker.HisMediStockMatyDAO.UpdateList(this.recentMediStockMatys))
                    {
                        LogSystem.Warn("Rollback HisMediStockMaty that bai. Kiem tra lai du lieu");
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
