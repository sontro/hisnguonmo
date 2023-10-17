using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEyeSurgryDesc
{
    public partial class HisEyeSurgryDescDAO : EntityBase
    {
        public List<V_HIS_EYE_SURGRY_DESC> GetView(HisEyeSurgryDescSO search, CommonParam param)
        {
            List<V_HIS_EYE_SURGRY_DESC> result = new List<V_HIS_EYE_SURGRY_DESC>();

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

        public HIS_EYE_SURGRY_DESC GetByCode(string code, HisEyeSurgryDescSO search)
        {
            HIS_EYE_SURGRY_DESC result = null;

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
        
        public V_HIS_EYE_SURGRY_DESC GetViewById(long id, HisEyeSurgryDescSO search)
        {
            V_HIS_EYE_SURGRY_DESC result = null;

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

        public V_HIS_EYE_SURGRY_DESC GetViewByCode(string code, HisEyeSurgryDescSO search)
        {
            V_HIS_EYE_SURGRY_DESC result = null;

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

        public Dictionary<string, HIS_EYE_SURGRY_DESC> GetDicByCode(HisEyeSurgryDescSO search, CommonParam param)
        {
            Dictionary<string, HIS_EYE_SURGRY_DESC> result = new Dictionary<string, HIS_EYE_SURGRY_DESC>();
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
