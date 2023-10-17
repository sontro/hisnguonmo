using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSeseDepoRepay
{
    public partial class HisSeseDepoRepayDAO : EntityBase
    {
        public List<V_HIS_SESE_DEPO_REPAY> GetView(HisSeseDepoRepaySO search, CommonParam param)
        {
            List<V_HIS_SESE_DEPO_REPAY> result = new List<V_HIS_SESE_DEPO_REPAY>();

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

        public HIS_SESE_DEPO_REPAY GetByCode(string code, HisSeseDepoRepaySO search)
        {
            HIS_SESE_DEPO_REPAY result = null;

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
        
        public V_HIS_SESE_DEPO_REPAY GetViewById(long id, HisSeseDepoRepaySO search)
        {
            V_HIS_SESE_DEPO_REPAY result = null;

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

        public V_HIS_SESE_DEPO_REPAY GetViewByCode(string code, HisSeseDepoRepaySO search)
        {
            V_HIS_SESE_DEPO_REPAY result = null;

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

        public Dictionary<string, HIS_SESE_DEPO_REPAY> GetDicByCode(HisSeseDepoRepaySO search, CommonParam param)
        {
            Dictionary<string, HIS_SESE_DEPO_REPAY> result = new Dictionary<string, HIS_SESE_DEPO_REPAY>();
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
