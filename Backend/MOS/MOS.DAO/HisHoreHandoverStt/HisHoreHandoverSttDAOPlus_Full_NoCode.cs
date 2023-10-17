using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHoreHandoverStt
{
    public partial class HisHoreHandoverSttDAO : EntityBase
    {
        public List<V_HIS_HORE_HANDOVER_STT> GetView(HisHoreHandoverSttSO search, CommonParam param)
        {
            List<V_HIS_HORE_HANDOVER_STT> result = new List<V_HIS_HORE_HANDOVER_STT>();
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

        public V_HIS_HORE_HANDOVER_STT GetViewById(long id, HisHoreHandoverSttSO search)
        {
            V_HIS_HORE_HANDOVER_STT result = null;

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
