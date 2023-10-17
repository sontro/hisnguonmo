using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarPrintTypeCfg
{
    public partial class SarPrintTypeCfgDAO : EntityBase
    {
        public List<V_SAR_PRINT_TYPE_CFG> GetView(SarPrintTypeCfgSO search, CommonParam param)
        {
            List<V_SAR_PRINT_TYPE_CFG> result = new List<V_SAR_PRINT_TYPE_CFG>();
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

        public V_SAR_PRINT_TYPE_CFG GetViewById(long id, SarPrintTypeCfgSO search)
        {
            V_SAR_PRINT_TYPE_CFG result = null;

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
