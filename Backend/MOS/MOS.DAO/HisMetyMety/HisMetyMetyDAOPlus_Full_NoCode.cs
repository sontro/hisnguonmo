using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMetyMety
{
    public partial class HisMetyMetyDAO : EntityBase
    {
        public List<V_HIS_METY_METY> GetView(HisMetyMetySO search, CommonParam param)
        {
            List<V_HIS_METY_METY> result = new List<V_HIS_METY_METY>();
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

        public V_HIS_METY_METY GetViewById(long id, HisMetyMetySO search)
        {
            V_HIS_METY_METY result = null;

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
