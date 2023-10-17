using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediStock
{
    public partial class HisMediStockDAO : EntityBase
    {
        public List<V_HIS_MEDI_STOCK> GetView(HisMediStockSO search, CommonParam param)
        {
            List<V_HIS_MEDI_STOCK> result = new List<V_HIS_MEDI_STOCK>();
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

        public V_HIS_MEDI_STOCK GetViewById(long id, HisMediStockSO search)
        {
            V_HIS_MEDI_STOCK result = null;

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
    }
}
