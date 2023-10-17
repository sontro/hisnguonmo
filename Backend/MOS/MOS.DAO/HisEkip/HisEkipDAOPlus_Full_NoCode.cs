using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEkip
{
    public partial class HisEkipDAO : EntityBase
    {
        public List<V_HIS_EKIP> GetView(HisEkipSO search, CommonParam param)
        {
            List<V_HIS_EKIP> result = new List<V_HIS_EKIP>();
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

        public V_HIS_EKIP GetViewById(long id, HisEkipSO search)
        {
            V_HIS_EKIP result = null;

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
