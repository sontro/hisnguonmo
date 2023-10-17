using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediStockMety
{
    class HisMediStockMetyUpdate : BusinessBase
    {
        private List<HIS_MEDI_STOCK_METY> recentMediStockMetys = new List<HIS_MEDI_STOCK_METY>();

        internal HisMediStockMetyUpdate()
            : base()
        {

        }

        internal HisMediStockMetyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDI_STOCK_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDI_STOCK_METY raw = null;
                HisMediStockMetyCheck checker = new HisMediStockMetyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!DAOWorker.HisMediStockMetyDAO.Update(data))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisMediStockMety_CapNhatThatBai);
                        throw new Exception("Cap nhat HIS_MEDI_STOCK_METY that bai");
                    }
                    result = true;
                    this.recentMediStockMetys.Add(raw);
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

        internal bool UpdateList(List<HIS_MEDI_STOCK_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_MEDI_STOCK_METY> listRaw = new List<HIS_MEDI_STOCK_METY>();
                valid = IsNotNullOrEmpty(listData);
                HisMediStockMetyCheck checker = new HisMediStockMetyCheck(param);
                checker.VerifyIds(listData.Select(s => s.ID).ToList(), listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMediStockMetyDAO.UpdateList(listData))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisMediStockMety_CapNhatThatBai);
                        throw new Exception("Cap nhat HIS_MEDI_STOCK_METY that bai");
                    }
                    result = true;
                    this.recentMediStockMetys.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_MEDI_STOCK_METY> listData, List<HIS_MEDI_STOCK_METY> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediStockMetyCheck checker = new HisMediStockMetyCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMediStockMetyDAO.UpdateList(listData))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisMediStockMety_CapNhatThatBai);
                        throw new Exception("Cap nhat HIS_MEDI_STOCK_METY that bai");
                    }
                    result = true;
                    this.recentMediStockMetys.AddRange(listBefore);
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
                if (IsNotNullOrEmpty(this.recentMediStockMetys))
                {
                    if (!DAOWorker.HisMediStockMetyDAO.UpdateList(this.recentMediStockMetys))
                    {
                        LogSystem.Warn("Rollback HisMediStockMety that bai. Kiem tra lai du lieu");
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
