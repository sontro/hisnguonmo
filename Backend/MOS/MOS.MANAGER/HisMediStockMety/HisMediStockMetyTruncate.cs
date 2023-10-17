using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockMety
{
    class HisMediStockMetyTruncate : BusinessBase
    {
        internal HisMediStockMetyTruncate()
            : base()
        {

        }

        internal HisMediStockMetyTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MEDI_STOCK_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediStockMetyCheck checker = new HisMediStockMetyCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisMediStockMetyDAO.Truncate(data);
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

        internal bool TruncateList(List<long> ids)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(ids);
                HisMediStockMetyCheck checker = new HisMediStockMetyCheck(param);
                List<HIS_MEDI_STOCK_METY> listRaw = new List<HIS_MEDI_STOCK_METY>();
                valid = valid && checker.VerifyIds(ids, listRaw);
                if (valid)
                {
                    result = this.TruncateList(listRaw);
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

        internal bool TruncateList(List<HIS_MEDI_STOCK_METY> listRaw)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listRaw);
                HisMediStockMetyCheck checker = new HisMediStockMetyCheck(param);
                valid = valid && checker.IsUnLock(listRaw);

                if (valid)
                {
                    result = DAOWorker.HisMediStockMetyDAO.TruncateList(listRaw);
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

        internal bool TruncateByMediStockId(long mediStockId)
        {
            bool result = false;
            try
            {
                List<HIS_MEDI_STOCK_METY> mediStockMeties = new HisMediStockMetyGet().GetByMediStockId(mediStockId);
                if (IsNotNullOrEmpty(mediStockMeties))
                {
                    result = this.TruncateList(mediStockMeties);
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
    }
}
