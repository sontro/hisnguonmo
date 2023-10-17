using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAntibioticRequest
{
    public partial class HisAntibioticRequestDAO : EntityBase
    {
        public List<V_HIS_ANTIBIOTIC_REQUEST> GetView(HisAntibioticRequestSO search, CommonParam param)
        {
            List<V_HIS_ANTIBIOTIC_REQUEST> result = new List<V_HIS_ANTIBIOTIC_REQUEST>();
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

        public V_HIS_ANTIBIOTIC_REQUEST GetViewById(long id, HisAntibioticRequestSO search)
        {
            V_HIS_ANTIBIOTIC_REQUEST result = null;

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
