using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockMety
{
    class HisMediStockMetyCreate : BusinessBase
    {
        private List<HIS_MEDI_STOCK_METY> recentMediStockMetys = new List<HIS_MEDI_STOCK_METY>();

        internal HisMediStockMetyCreate()
            : base()
        {

        }

        internal HisMediStockMetyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDI_STOCK_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediStockMetyCheck checker = new HisMediStockMetyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    result = DAOWorker.HisMediStockMetyDAO.Create(data);
                    if (result) this.recentMediStockMetys.Add(data);
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

        internal bool CreateList(List<HIS_MEDI_STOCK_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediStockMetyCheck checker = new HisMediStockMetyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisMediStockMetyDAO.CreateList(listData);
                    if (result) this.recentMediStockMetys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentMediStockMetys))
            {
                if (!DAOWorker.HisMediStockMetyDAO.TruncateList(this.recentMediStockMetys))
                {
                    LogSystem.Warn("Rollback du lieu HisMediStockMety that bai, can kiem tra lai." + LogUtil.TraceData("HisMediStockMety", this.recentMediStockMetys));
                }
            }
        }
    }
}
