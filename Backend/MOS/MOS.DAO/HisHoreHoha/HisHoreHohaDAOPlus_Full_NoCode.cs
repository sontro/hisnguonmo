using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHoreHoha
{
    public partial class HisHoreHohaDAO : EntityBase
    {
        public List<V_HIS_HORE_HOHA> GetView(HisHoreHohaSO search, CommonParam param)
        {
            List<V_HIS_HORE_HOHA> result = new List<V_HIS_HORE_HOHA>();
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

        public V_HIS_HORE_HOHA GetViewById(long id, HisHoreHohaSO search)
        {
            V_HIS_HORE_HOHA result = null;

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
