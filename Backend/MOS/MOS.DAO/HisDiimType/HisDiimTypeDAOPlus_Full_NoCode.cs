using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDiimType
{
    public partial class HisDiimTypeDAO : EntityBase
    {
        public List<V_HIS_DIIM_TYPE> GetView(HisDiimTypeSO search, CommonParam param)
        {
            List<V_HIS_DIIM_TYPE> result = new List<V_HIS_DIIM_TYPE>();
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

        public V_HIS_DIIM_TYPE GetViewById(long id, HisDiimTypeSO search)
        {
            V_HIS_DIIM_TYPE result = null;

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
