using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceMachine
{
    public partial class HisServiceMachineDAO : EntityBase
    {
        public List<V_HIS_SERVICE_MACHINE> GetView(HisServiceMachineSO search, CommonParam param)
        {
            List<V_HIS_SERVICE_MACHINE> result = new List<V_HIS_SERVICE_MACHINE>();
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

        public V_HIS_SERVICE_MACHINE GetViewById(long id, HisServiceMachineSO search)
        {
            V_HIS_SERVICE_MACHINE result = null;

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
