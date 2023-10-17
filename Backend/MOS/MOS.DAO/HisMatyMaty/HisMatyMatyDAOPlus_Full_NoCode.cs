using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMatyMaty
{
    public partial class HisMatyMatyDAO : EntityBase
    {
        public List<V_HIS_MATY_MATY> GetView(HisMatyMatySO search, CommonParam param)
        {
            List<V_HIS_MATY_MATY> result = new List<V_HIS_MATY_MATY>();
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

        public V_HIS_MATY_MATY GetViewById(long id, HisMatyMatySO search)
        {
            V_HIS_MATY_MATY result = null;

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
