using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarPrintType
{
    public partial class SarPrintTypeDAO : EntityBase
    {
        public List<V_SAR_PRINT_TYPE> GetView(SarPrintTypeSO search, CommonParam param)
        {
            List<V_SAR_PRINT_TYPE> result = new List<V_SAR_PRINT_TYPE>();
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

        public V_SAR_PRINT_TYPE GetViewById(long id, SarPrintTypeSO search)
        {
            V_SAR_PRINT_TYPE result = null;

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
