using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisFormTypeCfgData
{
    public partial class HisFormTypeCfgDataDAO : EntityBase
    {
        public List<V_HIS_FORM_TYPE_CFG_DATA> GetView(HisFormTypeCfgDataSO search, CommonParam param)
        {
            List<V_HIS_FORM_TYPE_CFG_DATA> result = new List<V_HIS_FORM_TYPE_CFG_DATA>();
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

        public V_HIS_FORM_TYPE_CFG_DATA GetViewById(long id, HisFormTypeCfgDataSO search)
        {
            V_HIS_FORM_TYPE_CFG_DATA result = null;

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
