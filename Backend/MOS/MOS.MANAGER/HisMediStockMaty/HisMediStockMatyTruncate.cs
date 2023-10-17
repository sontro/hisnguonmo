using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockMaty
{
    class HisMediStockMatyTruncate : BusinessBase
    {
        internal HisMediStockMatyTruncate()
            : base()
        {

        }

        internal HisMediStockMatyTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MEDI_STOCK_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediStockMatyCheck checker = new HisMediStockMatyCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisMediStockMatyDAO.Truncate(data);
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
                HisMediStockMatyCheck checker = new HisMediStockMatyCheck(param);
                List<HIS_MEDI_STOCK_MATY> listRaw = new List<HIS_MEDI_STOCK_MATY>();
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

        internal bool TruncateList(List<HIS_MEDI_STOCK_MATY> listRaw)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listRaw);
                HisMediStockMatyCheck checker = new HisMediStockMatyCheck(param);
                valid = valid && checker.IsUnLock(listRaw);

                if (valid)
                {
                    result = DAOWorker.HisMediStockMatyDAO.TruncateList(listRaw);
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
                List<HIS_MEDI_STOCK_MATY> mediStockMaties = new HisMediStockMatyGet().GetByMediStockId(mediStockId);
                if (IsNotNullOrEmpty(mediStockMaties))
                {
                    result = this.TruncateList(mediStockMaties);
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
