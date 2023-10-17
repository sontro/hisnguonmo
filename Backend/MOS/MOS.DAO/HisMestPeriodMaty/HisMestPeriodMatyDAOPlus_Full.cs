using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPeriodMaty
{
    public partial class HisMestPeriodMatyDAO : EntityBase
    {
        public List<V_HIS_MEST_PERIOD_MATY> GetView(HisMestPeriodMatySO search, CommonParam param)
        {
            List<V_HIS_MEST_PERIOD_MATY> result = new List<V_HIS_MEST_PERIOD_MATY>();

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

        public HIS_MEST_PERIOD_MATY GetByCode(string code, HisMestPeriodMatySO search)
        {
            HIS_MEST_PERIOD_MATY result = null;

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
        
        public V_HIS_MEST_PERIOD_MATY GetViewById(long id, HisMestPeriodMatySO search)
        {
            V_HIS_MEST_PERIOD_MATY result = null;

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

        public V_HIS_MEST_PERIOD_MATY GetViewByCode(string code, HisMestPeriodMatySO search)
        {
            V_HIS_MEST_PERIOD_MATY result = null;

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

        public Dictionary<string, HIS_MEST_PERIOD_MATY> GetDicByCode(HisMestPeriodMatySO search, CommonParam param)
        {
            Dictionary<string, HIS_MEST_PERIOD_MATY> result = new Dictionary<string, HIS_MEST_PERIOD_MATY>();
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
