using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBltyService
{
    public partial class HisBltyServiceDAO : EntityBase
    {
        public List<V_HIS_BLTY_SERVICE> GetView(HisBltyServiceSO search, CommonParam param)
        {
            List<V_HIS_BLTY_SERVICE> result = new List<V_HIS_BLTY_SERVICE>();
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

        public V_HIS_BLTY_SERVICE GetViewById(long id, HisBltyServiceSO search)
        {
            V_HIS_BLTY_SERVICE result = null;

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
