using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPaanLiquid
{
    public partial class HisPaanLiquidDAO : EntityBase
    {
        public List<V_HIS_PAAN_LIQUID> GetView(HisPaanLiquidSO search, CommonParam param)
        {
            List<V_HIS_PAAN_LIQUID> result = new List<V_HIS_PAAN_LIQUID>();

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

        public HIS_PAAN_LIQUID GetByCode(string code, HisPaanLiquidSO search)
        {
            HIS_PAAN_LIQUID result = null;

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
        
        public V_HIS_PAAN_LIQUID GetViewById(long id, HisPaanLiquidSO search)
        {
            V_HIS_PAAN_LIQUID result = null;

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

        public V_HIS_PAAN_LIQUID GetViewByCode(string code, HisPaanLiquidSO search)
        {
            V_HIS_PAAN_LIQUID result = null;

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

        public Dictionary<string, HIS_PAAN_LIQUID> GetDicByCode(HisPaanLiquidSO search, CommonParam param)
        {
            Dictionary<string, HIS_PAAN_LIQUID> result = new Dictionary<string, HIS_PAAN_LIQUID>();
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
