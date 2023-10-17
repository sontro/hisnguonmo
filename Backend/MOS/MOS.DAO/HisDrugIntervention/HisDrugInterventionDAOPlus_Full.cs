using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDrugIntervention
{
    public partial class HisDrugInterventionDAO : EntityBase
    {
        public List<V_HIS_DRUG_INTERVENTION> GetView(HisDrugInterventionSO search, CommonParam param)
        {
            List<V_HIS_DRUG_INTERVENTION> result = new List<V_HIS_DRUG_INTERVENTION>();

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

        public HIS_DRUG_INTERVENTION GetByCode(string code, HisDrugInterventionSO search)
        {
            HIS_DRUG_INTERVENTION result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
        
        public V_HIS_DRUG_INTERVENTION GetViewById(long id, HisDrugInterventionSO search)
        {
            V_HIS_DRUG_INTERVENTION result = null;

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

        public V_HIS_DRUG_INTERVENTION GetViewByCode(string code, HisDrugInterventionSO search)
        {
            V_HIS_DRUG_INTERVENTION result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public Dictionary<string, HIS_DRUG_INTERVENTION> GetDicByCode(HisDrugInterventionSO search, CommonParam param)
        {
            Dictionary<string, HIS_DRUG_INTERVENTION> result = new Dictionary<string, HIS_DRUG_INTERVENTION>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {
            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
