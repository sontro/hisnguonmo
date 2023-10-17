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

        public HIS_ANTIBIOTIC_REQUEST GetByCode(string code, HisAntibioticRequestSO search)
        {
            HIS_ANTIBIOTIC_REQUEST result = null;

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

        public V_HIS_ANTIBIOTIC_REQUEST GetViewByCode(string code, HisAntibioticRequestSO search)
        {
            V_HIS_ANTIBIOTIC_REQUEST result = null;

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

        public Dictionary<string, HIS_ANTIBIOTIC_REQUEST> GetDicByCode(HisAntibioticRequestSO search, CommonParam param)
        {
            Dictionary<string, HIS_ANTIBIOTIC_REQUEST> result = new Dictionary<string, HIS_ANTIBIOTIC_REQUEST>();
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
