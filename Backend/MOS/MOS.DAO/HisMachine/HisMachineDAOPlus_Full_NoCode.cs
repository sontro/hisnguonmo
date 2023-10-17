using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMachine
{
    public partial class HisMachineDAO : EntityBase
    {
        public List<V_HIS_MACHINE> GetView(HisMachineSO search, CommonParam param)
        {
            List<V_HIS_MACHINE> result = new List<V_HIS_MACHINE>();
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

        public V_HIS_MACHINE GetViewById(long id, HisMachineSO search)
        {
            V_HIS_MACHINE result = null;

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
