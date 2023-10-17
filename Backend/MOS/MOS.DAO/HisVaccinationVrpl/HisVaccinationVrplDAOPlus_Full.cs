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

        public HIS_VACCINATION_VRPL GetByCode(string code, HisVaccinationVrplSO search)
        {
            HIS_VACCINATION_VRPL result = null;

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

        public V_HIS_VACCINATION_VRPL GetViewByCode(string code, HisVaccinationVrplSO search)
        {
            V_HIS_VACCINATION_VRPL result = null;

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

        public Dictionary<string, HIS_VACCINATION_VRPL> GetDicByCode(HisVaccinationVrplSO search, CommonParam param)
        {
            Dictionary<string, HIS_VACCINATION_VRPL> result = new Dictionary<string, HIS_VACCINATION_VRPL>();
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
