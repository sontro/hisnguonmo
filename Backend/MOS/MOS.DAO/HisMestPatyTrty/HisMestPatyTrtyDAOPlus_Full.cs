using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPatyTrty
{
    public partial class HisMestPatyTrtyDAO : EntityBase
    {
        public List<V_HIS_MEST_PATY_TRTY> GetView(HisMestPatyTrtySO search, CommonParam param)
        {
            List<V_HIS_MEST_PATY_TRTY> result = new List<V_HIS_MEST_PATY_TRTY>();

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

        public HIS_MEST_PATY_TRTY GetByCode(string code, HisMestPatyTrtySO search)
        {
            HIS_MEST_PATY_TRTY result = null;

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
        
        public V_HIS_MEST_PATY_TRTY GetViewById(long id, HisMestPatyTrtySO search)
        {
            V_HIS_MEST_PATY_TRTY result = null;

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

        public V_HIS_MEST_PATY_TRTY GetViewByCode(string code, HisMestPatyTrtySO search)
        {
            V_HIS_MEST_PATY_TRTY result = null;

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

        public Dictionary<string, HIS_MEST_PATY_TRTY> GetDicByCode(HisMestPatyTrtySO search, CommonParam param)
        {
            Dictionary<string, HIS_MEST_PATY_TRTY> result = new Dictionary<string, HIS_MEST_PATY_TRTY>();
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
