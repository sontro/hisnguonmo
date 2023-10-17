using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAnticipate
{
    public partial class HisAnticipateDAO : EntityBase
    {
        public List<V_HIS_ANTICIPATE> GetView(HisAnticipateSO search, CommonParam param)
        {
            List<V_HIS_ANTICIPATE> result = new List<V_HIS_ANTICIPATE>();
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

        public V_HIS_ANTICIPATE GetViewById(long id, HisAnticipateSO search)
        {
            V_HIS_ANTICIPATE result = null;

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
