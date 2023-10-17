using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAntibioticMicrobi
{
    public partial class HisAntibioticMicrobiDAO : EntityBase
    {
        public List<V_HIS_ANTIBIOTIC_MICROBI> GetView(HisAntibioticMicrobiSO search, CommonParam param)
        {
            List<V_HIS_ANTIBIOTIC_MICROBI> result = new List<V_HIS_ANTIBIOTIC_MICROBI>();

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

        public HIS_ANTIBIOTIC_MICROBI GetByCode(string code, HisAntibioticMicrobiSO search)
        {
            HIS_ANTIBIOTIC_MICROBI result = null;

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
        
        public V_HIS_ANTIBIOTIC_MICROBI GetViewById(long id, HisAntibioticMicrobiSO search)
        {
            V_HIS_ANTIBIOTIC_MICROBI result = null;

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

        public V_HIS_ANTIBIOTIC_MICROBI GetViewByCode(string code, HisAntibioticMicrobiSO search)
        {
            V_HIS_ANTIBIOTIC_MICROBI result = null;

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

        public Dictionary<string, HIS_ANTIBIOTIC_MICROBI> GetDicByCode(HisAntibioticMicrobiSO search, CommonParam param)
        {
            Dictionary<string, HIS_ANTIBIOTIC_MICROBI> result = new Dictionary<string, HIS_ANTIBIOTIC_MICROBI>();
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
