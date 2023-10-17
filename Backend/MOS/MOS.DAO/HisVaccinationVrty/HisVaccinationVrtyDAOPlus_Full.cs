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

        public HIS_VACCINATION_VRTY GetByCode(string code, HisVaccinationVrtySO search)
        {
            HIS_VACCINATION_VRTY result = null;

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

        public V_HIS_VACCINATION_VRTY GetViewByCode(string code, HisVaccinationVrtySO search)
        {
            V_HIS_VACCINATION_VRTY result = null;

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

        public Dictionary<string, HIS_VACCINATION_VRTY> GetDicByCode(HisVaccinationVrtySO search, CommonParam param)
        {
            Dictionary<string, HIS_VACCINATION_VRTY> result = new Dictionary<string, HIS_VACCINATION_VRTY>();
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
