using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaexVaer
{
    public partial class HisVaexVaerDAO : EntityBase
    {
        public List<V_HIS_VAEX_VAER> GetView(HisVaexVaerSO search, CommonParam param)
        {
            List<V_HIS_VAEX_VAER> result = new List<V_HIS_VAEX_VAER>();

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

        public HIS_VAEX_VAER GetByCode(string code, HisVaexVaerSO search)
        {
            HIS_VAEX_VAER result = null;

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
        
        public V_HIS_VAEX_VAER GetViewById(long id, HisVaexVaerSO search)
        {
            V_HIS_VAEX_VAER result = null;

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

        public V_HIS_VAEX_VAER GetViewByCode(string code, HisVaexVaerSO search)
        {
            V_HIS_VAEX_VAER result = null;

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

        public Dictionary<string, HIS_VAEX_VAER> GetDicByCode(HisVaexVaerSO search, CommonParam param)
        {
            Dictionary<string, HIS_VAEX_VAER> result = new Dictionary<string, HIS_VAEX_VAER>();
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
