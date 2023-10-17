using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediStockPeriod
{
    public partial class HisMediStockPeriodDAO : EntityBase
    {
        public HIS_MEDI_STOCK_PERIOD GetByCode(string code, HisMediStockPeriodSO search)
        {
            HIS_MEDI_STOCK_PERIOD result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public Dictionary<string, HIS_MEDI_STOCK_PERIOD> GetDicByCode(HisMediStockPeriodSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEDI_STOCK_PERIOD> result = new Dictionary<string, HIS_MEDI_STOCK_PERIOD>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {

            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
