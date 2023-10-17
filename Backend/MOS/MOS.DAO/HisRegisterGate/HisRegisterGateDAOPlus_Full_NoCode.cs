using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRegisterGate
{
    public partial class HisRegisterGateDAO : EntityBase
    {
        public List<V_HIS_REGISTER_GATE> GetView(HisRegisterGateSO search, CommonParam param)
        {
            List<V_HIS_REGISTER_GATE> result = new List<V_HIS_REGISTER_GATE>();
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

        public V_HIS_REGISTER_GATE GetViewById(long id, HisRegisterGateSO search)
        {
            V_HIS_REGISTER_GATE result = null;

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
