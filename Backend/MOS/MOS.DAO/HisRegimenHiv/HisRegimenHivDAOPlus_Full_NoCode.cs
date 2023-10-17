using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRegimenHiv
{
    public partial class HisRegimenHivDAO : EntityBase
    {
        public List<V_HIS_REGIMEN_HIV> GetView(HisRegimenHivSO search, CommonParam param)
        {
            List<V_HIS_REGIMEN_HIV> result = new List<V_HIS_REGIMEN_HIV>();
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

        public V_HIS_REGIMEN_HIV GetViewById(long id, HisRegimenHivSO search)
        {
            V_HIS_REGIMEN_HIV result = null;

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
