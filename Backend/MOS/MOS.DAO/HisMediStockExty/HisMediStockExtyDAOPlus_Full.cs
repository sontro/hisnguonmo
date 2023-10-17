using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediStockExty
{
    public partial class HisMediStockExtyDAO : EntityBase
    {
        public List<V_HIS_MEDI_STOCK_EXTY> GetView(HisMediStockExtySO search, CommonParam param)
        {
            List<V_HIS_MEDI_STOCK_EXTY> result = new List<V_HIS_MEDI_STOCK_EXTY>();

            try
            {
                result = GetWorker.GetView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public HIS_MEDI_STOCK_EXTY GetByCode(string code, HisMediStockExtySO search)
        {
            HIS_MEDI_STOCK_EXTY result = null;

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
        
        public V_HIS_MEDI_STOCK_EXTY GetViewById(long id, HisMediStockExtySO search)
        {
            V_HIS_MEDI_STOCK_EXTY result = null;

            try
            {
                result = GetWorker.GetViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public V_HIS_MEDI_STOCK_EXTY GetViewByCode(string code, HisMediStockExtySO search)
        {
            V_HIS_MEDI_STOCK_EXTY result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public Dictionary<string, HIS_MEDI_STOCK_EXTY> GetDicByCode(HisMediStockExtySO search, CommonParam param)
        {
            Dictionary<string, HIS_MEDI_STOCK_EXTY> result = new Dictionary<string, HIS_MEDI_STOCK_EXTY>();
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
