using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEyeSurgryDesc
{
    public partial class HisEyeSurgryDescDAO : EntityBase
    {
        public List<V_HIS_EYE_SURGRY_DESC> GetView(HisEyeSurgryDescSO search, CommonParam param)
        {
            List<V_HIS_EYE_SURGRY_DESC> result = new List<V_HIS_EYE_SURGRY_DESC>();
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

        public V_HIS_EYE_SURGRY_DESC GetViewById(long id, HisEyeSurgryDescSO search)
        {
            V_HIS_EYE_SURGRY_DESC result = null;

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
