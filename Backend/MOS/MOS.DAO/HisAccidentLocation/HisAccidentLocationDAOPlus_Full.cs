using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentLocation
{
    public partial class HisAccidentLocationDAO : EntityBase
    {
        public List<V_HIS_ACCIDENT_LOCATION> GetView(HisAccidentLocationSO search, CommonParam param)
        {
            List<V_HIS_ACCIDENT_LOCATION> result = new List<V_HIS_ACCIDENT_LOCATION>();

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

        public HIS_ACCIDENT_LOCATION GetByCode(string code, HisAccidentLocationSO search)
        {
            HIS_ACCIDENT_LOCATION result = null;

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
        
        public V_HIS_ACCIDENT_LOCATION GetViewById(long id, HisAccidentLocationSO search)
        {
            V_HIS_ACCIDENT_LOCATION result = null;

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

        public V_HIS_ACCIDENT_LOCATION GetViewByCode(string code, HisAccidentLocationSO search)
        {
            V_HIS_ACCIDENT_LOCATION result = null;

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

        public Dictionary<string, HIS_ACCIDENT_LOCATION> GetDicByCode(HisAccidentLocationSO search, CommonParam param)
        {
            Dictionary<string, HIS_ACCIDENT_LOCATION> result = new Dictionary<string, HIS_ACCIDENT_LOCATION>();
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
