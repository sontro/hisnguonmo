using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVareVart
{
    public partial class HisVareVartDAO : EntityBase
    {
        public List<V_HIS_VARE_VART> GetView(HisVareVartSO search, CommonParam param)
        {
            List<V_HIS_VARE_VART> result = new List<V_HIS_VARE_VART>();
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

        public V_HIS_VARE_VART GetViewById(long id, HisVareVartSO search)
        {
            V_HIS_VARE_VART result = null;

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
