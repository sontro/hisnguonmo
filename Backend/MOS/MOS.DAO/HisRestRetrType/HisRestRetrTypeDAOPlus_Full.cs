using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRestRetrType
{
    public partial class HisRestRetrTypeDAO : EntityBase
    {
        public List<V_HIS_REST_RETR_TYPE> GetView(HisRestRetrTypeSO search, CommonParam param)
        {
            List<V_HIS_REST_RETR_TYPE> result = new List<V_HIS_REST_RETR_TYPE>();

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

        public HIS_REST_RETR_TYPE GetByCode(string code, HisRestRetrTypeSO search)
        {
            HIS_REST_RETR_TYPE result = null;

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
        
        public V_HIS_REST_RETR_TYPE GetViewById(long id, HisRestRetrTypeSO search)
        {
            V_HIS_REST_RETR_TYPE result = null;

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

        public V_HIS_REST_RETR_TYPE GetViewByCode(string code, HisRestRetrTypeSO search)
        {
            V_HIS_REST_RETR_TYPE result = null;

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

        public Dictionary<string, HIS_REST_RETR_TYPE> GetDicByCode(HisRestRetrTypeSO search, CommonParam param)
        {
            Dictionary<string, HIS_REST_RETR_TYPE> result = new Dictionary<string, HIS_REST_RETR_TYPE>();
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
