using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccinationStt
{
    public partial class HisVaccinationSttDAO : EntityBase
    {
        public List<V_HIS_VACCINATION_STT> GetView(HisVaccinationSttSO search, CommonParam param)
        {
            List<V_HIS_VACCINATION_STT> result = new List<V_HIS_VACCINATION_STT>();
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

        public V_HIS_VACCINATION_STT GetViewById(long id, HisVaccinationSttSO search)
        {
            V_HIS_VACCINATION_STT result = null;

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
