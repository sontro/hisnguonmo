using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmrCoverConfig
{
    public partial class HisEmrCoverConfigDAO : EntityBase
    {
        public List<V_HIS_EMR_COVER_CONFIG> GetView(HisEmrCoverConfigSO search, CommonParam param)
        {
            List<V_HIS_EMR_COVER_CONFIG> result = new List<V_HIS_EMR_COVER_CONFIG>();

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

        public HIS_EMR_COVER_CONFIG GetByCode(string code, HisEmrCoverConfigSO search)
        {
            HIS_EMR_COVER_CONFIG result = null;

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
        
        public V_HIS_EMR_COVER_CONFIG GetViewById(long id, HisEmrCoverConfigSO search)
        {
            V_HIS_EMR_COVER_CONFIG result = null;

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

        public V_HIS_EMR_COVER_CONFIG GetViewByCode(string code, HisEmrCoverConfigSO search)
        {
            V_HIS_EMR_COVER_CONFIG result = null;

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

        public Dictionary<string, HIS_EMR_COVER_CONFIG> GetDicByCode(HisEmrCoverConfigSO search, CommonParam param)
        {
            Dictionary<string, HIS_EMR_COVER_CONFIG> result = new Dictionary<string, HIS_EMR_COVER_CONFIG>();
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
