using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceReqMety
{
    public partial class HisServiceReqMetyDAO : EntityBase
    {
        public List<V_HIS_SERVICE_REQ_METY> GetView(HisServiceReqMetySO search, CommonParam param)
        {
            List<V_HIS_SERVICE_REQ_METY> result = new List<V_HIS_SERVICE_REQ_METY>();

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

        public HIS_SERVICE_REQ_METY GetByCode(string code, HisServiceReqMetySO search)
        {
            HIS_SERVICE_REQ_METY result = null;

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
        
        public V_HIS_SERVICE_REQ_METY GetViewById(long id, HisServiceReqMetySO search)
        {
            V_HIS_SERVICE_REQ_METY result = null;

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

        public V_HIS_SERVICE_REQ_METY GetViewByCode(string code, HisServiceReqMetySO search)
        {
            V_HIS_SERVICE_REQ_METY result = null;

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

        public Dictionary<string, HIS_SERVICE_REQ_METY> GetDicByCode(HisServiceReqMetySO search, CommonParam param)
        {
            Dictionary<string, HIS_SERVICE_REQ_METY> result = new Dictionary<string, HIS_SERVICE_REQ_METY>();
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
