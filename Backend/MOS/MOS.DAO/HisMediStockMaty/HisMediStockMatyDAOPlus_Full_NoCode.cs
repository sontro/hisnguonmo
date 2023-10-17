using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediStockMaty
{
    public partial class HisMediStockMatyDAO : EntityBase
    {
        public List<V_HIS_MEDI_STOCK_MATY> GetView(HisMediStockMatySO search, CommonParam param)
        {
            List<V_HIS_MEDI_STOCK_MATY> result = new List<V_HIS_MEDI_STOCK_MATY>();
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

        public V_HIS_MEDI_STOCK_MATY GetViewById(long id, HisMediStockMatySO search)
        {
            V_HIS_MEDI_STOCK_MATY result = null;

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

        public List<V_HIS_MEDI_STOCK_MATY_1> GetView1(HisMediStockMatySO search, CommonParam param)
        {
            List<V_HIS_MEDI_STOCK_MATY_1> result = new List<V_HIS_MEDI_STOCK_MATY_1>();
            try
            {
                result = GetWorker.GetView1(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public V_HIS_MEDI_STOCK_MATY_1 GetView1ById(long id, HisMediStockMatySO search)
        {
            V_HIS_MEDI_STOCK_MATY_1 result = null;

            try
            {
                result = GetWorker.GetView1ById(id, search);
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
