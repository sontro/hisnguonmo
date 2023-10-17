using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccinationReact
{
    public partial class HisVaccinationReactDAO : EntityBase
    {
        public List<V_HIS_VACCINATION_REACT> GetView(HisVaccinationReactSO search, CommonParam param)
        {
            List<V_HIS_VACCINATION_REACT> result = new List<V_HIS_VACCINATION_REACT>();
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

        public V_HIS_VACCINATION_REACT GetViewById(long id, HisVaccinationReactSO search)
        {
            V_HIS_VACCINATION_REACT result = null;

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
