using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccinationVrty
{
    public partial class HisVaccinationVrtyDAO : EntityBase
    {
        public List<V_HIS_VACCINATION_VRTY> GetView(HisVaccinationVrtySO search, CommonParam param)
        {
            List<V_HIS_VACCINATION_VRTY> result = new List<V_HIS_VACCINATION_VRTY>();
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

        public V_HIS_VACCINATION_VRTY GetViewById(long id, HisVaccinationVrtySO search)
        {
            V_HIS_VACCINATION_VRTY result = null;

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
