using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarPrint
{
    public partial class SarPrintDAO : EntityBase
    {
        public List<V_SAR_PRINT> GetView(SarPrintSO search, CommonParam param)
        {
            List<V_SAR_PRINT> result = new List<V_SAR_PRINT>();
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

        public V_SAR_PRINT GetViewById(long id, SarPrintSO search)
        {
            V_SAR_PRINT result = null;

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
