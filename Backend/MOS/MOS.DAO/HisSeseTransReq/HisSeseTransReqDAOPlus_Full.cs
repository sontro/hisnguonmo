using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSeseTransReq
{
    public partial class HisSeseTransReqDAO : EntityBase
    {
        public List<V_HIS_SESE_TRANS_REQ> GetView(HisSeseTransReqSO search, CommonParam param)
        {
            List<V_HIS_SESE_TRANS_REQ> result = new List<V_HIS_SESE_TRANS_REQ>();

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

        public HIS_SESE_TRANS_REQ GetByCode(string code, HisSeseTransReqSO search)
        {
            HIS_SESE_TRANS_REQ result = null;

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
        
        public V_HIS_SESE_TRANS_REQ GetViewById(long id, HisSeseTransReqSO search)
        {
            V_HIS_SESE_TRANS_REQ result = null;

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

        public V_HIS_SESE_TRANS_REQ GetViewByCode(string code, HisSeseTransReqSO search)
        {
            V_HIS_SESE_TRANS_REQ result = null;

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

        public Dictionary<string, HIS_SESE_TRANS_REQ> GetDicByCode(HisSeseTransReqSO search, CommonParam param)
        {
            Dictionary<string, HIS_SESE_TRANS_REQ> result = new Dictionary<string, HIS_SESE_TRANS_REQ>();
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
