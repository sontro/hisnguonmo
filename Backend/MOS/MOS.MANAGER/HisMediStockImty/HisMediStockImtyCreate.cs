using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockImty
{
    partial class HisMediStockImtyCreate : BusinessBase
    {
        private List<HIS_MEDI_STOCK_IMTY> recentHisMediStockImtys = new List<HIS_MEDI_STOCK_IMTY>();

        internal HisMediStockImtyCreate()
            : base()
        {

        }

        internal HisMediStockImtyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDI_STOCK_IMTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediStockImtyCheck checker = new HisMediStockImtyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisMediStockImtyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediStockImty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMediStockImty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMediStockImtys.Add(data);
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

        internal bool CreateList(List<HIS_MEDI_STOCK_IMTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediStockImtyCheck checker = new HisMediStockImtyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMediStockImtyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediStockImty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMediStockImty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMediStockImtys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMediStockImtys))
            {
                if (!new HisMediStockImtyTruncate(param).TruncateList(this.recentHisMediStockImtys))
                {
                    LogSystem.Warn("Rollback du lieu HisMediStockImty that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMediStockImtys", this.recentHisMediStockImtys));
                }
            }
        }
    }
}
