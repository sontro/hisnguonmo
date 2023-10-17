using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHoreDhty
{
    public partial class HisHoreDhtyDAO : EntityBase
    {
        public List<V_HIS_HORE_DHTY> GetView(HisHoreDhtySO search, CommonParam param)
        {
            List<V_HIS_HORE_DHTY> result = new List<V_HIS_HORE_DHTY>();
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

        public V_HIS_HORE_DHTY GetViewById(long id, HisHoreDhtySO search)
        {
            V_HIS_HORE_DHTY result = null;

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
