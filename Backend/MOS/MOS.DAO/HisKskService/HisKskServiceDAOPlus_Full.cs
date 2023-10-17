using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisKskService
{
    public partial class HisKskServiceDAO : EntityBase
    {
        public List<V_HIS_KSK_SERVICE> GetView(HisKskServiceSO search, CommonParam param)
        {
            List<V_HIS_KSK_SERVICE> result = new List<V_HIS_KSK_SERVICE>();

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

        public HIS_KSK_SERVICE GetByCode(string code, HisKskServiceSO search)
        {
            HIS_KSK_SERVICE result = null;

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
        
        public V_HIS_KSK_SERVICE GetViewById(long id, HisKskServiceSO search)
        {
            V_HIS_KSK_SERVICE result = null;

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

        public V_HIS_KSK_SERVICE GetViewByCode(string code, HisKskServiceSO search)
        {
            V_HIS_KSK_SERVICE result = null;

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

        public Dictionary<string, HIS_KSK_SERVICE> GetDicByCode(HisKskServiceSO search, CommonParam param)
        {
            Dictionary<string, HIS_KSK_SERVICE> result = new Dictionary<string, HIS_KSK_SERVICE>();
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
