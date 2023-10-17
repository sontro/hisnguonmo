using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisArea
{
    public partial class HisAreaDAO : EntityBase
    {
        public List<V_HIS_AREA> GetView(HisAreaSO search, CommonParam param)
        {
            List<V_HIS_AREA> result = new List<V_HIS_AREA>();
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

        public V_HIS_AREA GetViewById(long id, HisAreaSO search)
        {
            V_HIS_AREA result = null;

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
