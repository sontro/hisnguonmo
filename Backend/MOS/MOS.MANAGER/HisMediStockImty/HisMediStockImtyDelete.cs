using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediStockImty
{
    partial class HisMediStockImtyDelete : BusinessBase
    {
        internal HisMediStockImtyDelete()
            : base()
        {

        }

        internal HisMediStockImtyDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEDI_STOCK_IMTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediStockImtyCheck checker = new HisMediStockImtyCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_STOCK_IMTY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMediStockImtyDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEDI_STOCK_IMTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediStockImtyCheck checker = new HisMediStockImtyCheck(param);
                List<HIS_MEDI_STOCK_IMTY> listRaw = new List<HIS_MEDI_STOCK_IMTY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMediStockImtyDAO.DeleteList(listData);
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
