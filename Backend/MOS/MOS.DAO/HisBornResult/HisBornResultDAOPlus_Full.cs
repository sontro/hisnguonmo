using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBornResult
{
    public partial class HisBornResultDAO : EntityBase
    {
        public List<V_HIS_BORN_RESULT> GetView(HisBornResultSO search, CommonParam param)
        {
            List<V_HIS_BORN_RESULT> result = new List<V_HIS_BORN_RESULT>();

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

        public HIS_BORN_RESULT GetByCode(string code, HisBornResultSO search)
        {
            HIS_BORN_RESULT result = null;

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
        
        public V_HIS_BORN_RESULT GetViewById(long id, HisBornResultSO search)
        {
            V_HIS_BORN_RESULT result = null;

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

        public V_HIS_BORN_RESULT GetViewByCode(string code, HisBornResultSO search)
        {
            V_HIS_BORN_RESULT result = null;

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

        public Dictionary<string, HIS_BORN_RESULT> GetDicByCode(HisBornResultSO search, CommonParam param)
        {
            Dictionary<string, HIS_BORN_RESULT> result = new Dictionary<string, HIS_BORN_RESULT>();
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
