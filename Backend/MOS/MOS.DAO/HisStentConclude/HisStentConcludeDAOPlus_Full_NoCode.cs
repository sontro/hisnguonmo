using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisStentConclude
{
    public partial class HisStentConcludeDAO : EntityBase
    {
        public List<V_HIS_STENT_CONCLUDE> GetView(HisStentConcludeSO search, CommonParam param)
        {
            List<V_HIS_STENT_CONCLUDE> result = new List<V_HIS_STENT_CONCLUDE>();
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

        public V_HIS_STENT_CONCLUDE GetViewById(long id, HisStentConcludeSO search)
        {
            V_HIS_STENT_CONCLUDE result = null;

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
