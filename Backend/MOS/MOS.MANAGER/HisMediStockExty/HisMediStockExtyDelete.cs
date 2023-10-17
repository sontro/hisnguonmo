using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediStockExty
{
    partial class HisMediStockExtyDelete : BusinessBase
    {
        internal HisMediStockExtyDelete()
            : base()
        {

        }

        internal HisMediStockExtyDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MEDI_STOCK_EXTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediStockExtyCheck checker = new HisMediStockExtyCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_STOCK_EXTY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMediStockExtyDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MEDI_STOCK_EXTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediStockExtyCheck checker = new HisMediStockExtyCheck(param);
                List<HIS_MEDI_STOCK_EXTY> listRaw = new List<HIS_MEDI_STOCK_EXTY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMediStockExtyDAO.DeleteList(listData);
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
