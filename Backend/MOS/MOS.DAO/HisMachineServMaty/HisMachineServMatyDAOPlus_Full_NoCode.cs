using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMachineServMaty
{
    public partial class HisMachineServMatyDAO : EntityBase
    {
        public List<V_HIS_MACHINE_SERV_MATY> GetView(HisMachineServMatySO search, CommonParam param)
        {
            List<V_HIS_MACHINE_SERV_MATY> result = new List<V_HIS_MACHINE_SERV_MATY>();
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

        public V_HIS_MACHINE_SERV_MATY GetViewById(long id, HisMachineServMatySO search)
        {
            V_HIS_MACHINE_SERV_MATY result = null;

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
