using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestMetyUnit
{
    public partial class HisMestMetyUnitDAO : EntityBase
    {
        public List<V_HIS_MEST_METY_UNIT> GetView(HisMestMetyUnitSO search, CommonParam param)
        {
            List<V_HIS_MEST_METY_UNIT> result = new List<V_HIS_MEST_METY_UNIT>();

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

        public HIS_MEST_METY_UNIT GetByCode(string code, HisMestMetyUnitSO search)
        {
            HIS_MEST_METY_UNIT result = null;

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
        
        public V_HIS_MEST_METY_UNIT GetViewById(long id, HisMestMetyUnitSO search)
        {
            V_HIS_MEST_METY_UNIT result = null;

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

        public V_HIS_MEST_METY_UNIT GetViewByCode(string code, HisMestMetyUnitSO search)
        {
            V_HIS_MEST_METY_UNIT result = null;

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

        public Dictionary<string, HIS_MEST_METY_UNIT> GetDicByCode(HisMestMetyUnitSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEST_METY_UNIT> result = new Dictionary<string, HIS_MEST_METY_UNIT>();
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
