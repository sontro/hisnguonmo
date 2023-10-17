using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarRetyFofi
{
    public partial class SarRetyFofiDAO : EntityBase
    {
        public List<V_SAR_RETY_FOFI> GetView(SarRetyFofiSO search, CommonParam param)
        {
            List<V_SAR_RETY_FOFI> result = new List<V_SAR_RETY_FOFI>();
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

        public V_SAR_RETY_FOFI GetViewById(long id, SarRetyFofiSO search)
        {
            V_SAR_RETY_FOFI result = null;

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
