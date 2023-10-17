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
    }
}
