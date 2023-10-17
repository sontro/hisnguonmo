using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccinationExam
{
    public partial class HisVaccinationExamDAO : EntityBase
    {
        public List<V_HIS_VACCINATION_EXAM> GetView(HisVaccinationExamSO search, CommonParam param)
        {
            List<V_HIS_VACCINATION_EXAM> result = new List<V_HIS_VACCINATION_EXAM>();

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

        public HIS_VACCINATION_EXAM GetByCode(string code, HisVaccinationExamSO search)
        {
            HIS_VACCINATION_EXAM result = null;

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
        
        public V_HIS_VACCINATION_EXAM GetViewById(long id, HisVaccinationExamSO search)
        {
            V_HIS_VACCINATION_EXAM result = null;

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

        public V_HIS_VACCINATION_EXAM GetViewByCode(string code, HisVaccinationExamSO search)
        {
            V_HIS_VACCINATION_EXAM result = null;

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

        public Dictionary<string, HIS_VACCINATION_EXAM> GetDicByCode(HisVaccinationExamSO search, CommonParam param)
        {
            Dictionary<string, HIS_VACCINATION_EXAM> result = new Dictionary<string, HIS_VACCINATION_EXAM>();
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
