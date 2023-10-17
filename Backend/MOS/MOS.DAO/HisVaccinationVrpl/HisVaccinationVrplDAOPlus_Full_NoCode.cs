using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccinationVrpl
{
    public partial class HisVaccinationVrplDAO : EntityBase
    {
        public List<V_HIS_VACCINATION_VRPL> GetView(HisVaccinationVrplSO search, CommonParam param)
        {
            List<V_HIS_VACCINATION_VRPL> result = new List<V_HIS_VACCINATION_VRPL>();
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

        public V_HIS_VACCINATION_VRPL GetViewById(long id, HisVaccinationVrplSO search)
        {
            V_HIS_VACCINATION_VRPL result = null;

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
