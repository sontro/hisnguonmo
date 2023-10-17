using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockMaty
{
    class HisMediStockMatyCreate : BusinessBase
    {

        private List<HIS_MEDI_STOCK_MATY> recentHisMediStockMatys = new List<HIS_MEDI_STOCK_MATY>();

        internal HisMediStockMatyCreate()
            : base()
        {

        }

        internal HisMediStockMatyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDI_STOCK_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediStockMatyCheck checker = new HisMediStockMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    result = DAOWorker.HisMediStockMatyDAO.Create(data);
                    if (result) this.recentHisMediStockMatys.Add(data);
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

        internal bool CreateList(List<HIS_MEDI_STOCK_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediStockMatyCheck checker = new HisMediStockMatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisMediStockMatyDAO.CreateList(listData);
                    if (result) this.recentHisMediStockMatys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMediStockMatys))
            {
                if (!DAOWorker.HisMediStockMatyDAO.TruncateList(this.recentHisMediStockMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisMediStockMaty that bai, can kiem tra lai." + LogUtil.TraceData("HisMediStockMaty", this.recentHisMediStockMatys));
                }
            }
        }
    }
}
