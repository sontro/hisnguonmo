using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBhytParam
{
    public partial class HisBhytParamDAO : EntityBase
    {
        public List<V_HIS_BHYT_PARAM> GetView(HisBhytParamSO search, CommonParam param)
        {
            List<V_HIS_BHYT_PARAM> result = new List<V_HIS_BHYT_PARAM>();

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

        public HIS_BHYT_PARAM GetByCode(string code, HisBhytParamSO search)
        {
            HIS_BHYT_PARAM result = null;

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
        
        public V_HIS_BHYT_PARAM GetViewById(long id, HisBhytParamSO search)
        {
            V_HIS_BHYT_PARAM result = null;

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

        public V_HIS_BHYT_PARAM GetViewByCode(string code, HisBhytParamSO search)
        {
            V_HIS_BHYT_PARAM result = null;

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

        public Dictionary<string, HIS_BHYT_PARAM> GetDicByCode(HisBhytParamSO search, CommonParam param)
        {
            Dictionary<string, HIS_BHYT_PARAM> result = new Dictionary<string, HIS_BHYT_PARAM>();
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
