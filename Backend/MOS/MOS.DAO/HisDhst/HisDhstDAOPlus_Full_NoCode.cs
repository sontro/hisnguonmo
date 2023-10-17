using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDhst
{
    public partial class HisDhstDAO : EntityBase
    {
        public List<V_HIS_DHST> GetView(HisDhstSO search, CommonParam param)
        {
            List<V_HIS_DHST> result = new List<V_HIS_DHST>();
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

        public V_HIS_DHST GetViewById(long id, HisDhstSO search)
        {
            V_HIS_DHST result = null;

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
