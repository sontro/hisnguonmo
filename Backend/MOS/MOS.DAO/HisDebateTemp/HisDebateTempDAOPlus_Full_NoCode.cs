using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDebateTemp
{
    public partial class HisDebateTempDAO : EntityBase
    {
        public List<V_HIS_DEBATE_TEMP> GetView(HisDebateTempSO search, CommonParam param)
        {
            List<V_HIS_DEBATE_TEMP> result = new List<V_HIS_DEBATE_TEMP>();
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

        public V_HIS_DEBATE_TEMP GetViewById(long id, HisDebateTempSO search)
        {
            V_HIS_DEBATE_TEMP result = null;

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
