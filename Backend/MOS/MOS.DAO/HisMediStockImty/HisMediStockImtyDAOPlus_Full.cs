using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediStockImty
{
    public partial class HisMediStockImtyDAO : EntityBase
    {
        public List<V_HIS_MEDI_STOCK_IMTY> GetView(HisMediStockImtySO search, CommonParam param)
        {
            List<V_HIS_MEDI_STOCK_IMTY> result = new List<V_HIS_MEDI_STOCK_IMTY>();

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

        public HIS_MEDI_STOCK_IMTY GetByCode(string code, HisMediStockImtySO search)
        {
            HIS_MEDI_STOCK_IMTY result = null;

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
        
        public V_HIS_MEDI_STOCK_IMTY GetViewById(long id, HisMediStockImtySO search)
        {
            V_HIS_MEDI_STOCK_IMTY result = null;

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

        public V_HIS_MEDI_STOCK_IMTY GetViewByCode(string code, HisMediStockImtySO search)
        {
            V_HIS_MEDI_STOCK_IMTY result = null;

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

        public Dictionary<string, HIS_MEDI_STOCK_IMTY> GetDicByCode(HisMediStockImtySO search, CommonParam param)
        {
            Dictionary<string, HIS_MEDI_STOCK_IMTY> result = new Dictionary<string, HIS_MEDI_STOCK_IMTY>();
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
