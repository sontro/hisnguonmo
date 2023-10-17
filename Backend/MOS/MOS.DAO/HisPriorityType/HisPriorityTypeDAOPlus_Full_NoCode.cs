using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPriorityType
{
    public partial class HisPriorityTypeDAO : EntityBase
    {
        public List<V_HIS_PRIORITY_TYPE> GetView(HisPriorityTypeSO search, CommonParam param)
        {
            List<V_HIS_PRIORITY_TYPE> result = new List<V_HIS_PRIORITY_TYPE>();
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

        public V_HIS_PRIORITY_TYPE GetViewById(long id, HisPriorityTypeSO search)
        {
            V_HIS_PRIORITY_TYPE result = null;

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
