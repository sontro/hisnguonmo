using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSereServRation
{
    public partial class HisSereServRationDAO : EntityBase
    {
        public List<V_HIS_SERE_SERV_RATION> GetView(HisSereServRationSO search, CommonParam param)
        {
            List<V_HIS_SERE_SERV_RATION> result = new List<V_HIS_SERE_SERV_RATION>();
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

        public V_HIS_SERE_SERV_RATION GetViewById(long id, HisSereServRationSO search)
        {
            V_HIS_SERE_SERV_RATION result = null;

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
