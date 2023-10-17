using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockExty
{
    partial class HisMediStockExtyCreate : BusinessBase
    {
        private List<HIS_MEDI_STOCK_EXTY> recentHisMediStockExtys = new List<HIS_MEDI_STOCK_EXTY>();

        internal HisMediStockExtyCreate()
            : base()
        {

        }

        internal HisMediStockExtyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDI_STOCK_EXTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediStockExtyCheck checker = new HisMediStockExtyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisMediStockExtyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediStockExty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMediStockExty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMediStockExtys.Add(data);
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

        internal bool CreateList(List<HIS_MEDI_STOCK_EXTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediStockExtyCheck checker = new HisMediStockExtyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMediStockExtyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediStockExty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMediStockExty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMediStockExtys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMediStockExtys))
            {
                if (!new HisMediStockExtyTruncate(param).TruncateList(this.recentHisMediStockExtys))
                {
                    LogSystem.Warn("Rollback du lieu HisMediStockExty that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMediStockExtys", this.recentHisMediStockExtys));
                }
            }
        }
    }
}
