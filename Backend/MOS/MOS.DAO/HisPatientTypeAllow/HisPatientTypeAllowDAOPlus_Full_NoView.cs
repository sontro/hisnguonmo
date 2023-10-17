using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPatientTypeAllow
{
    public partial class HisPatientTypeAllowDAO : EntityBase
    {
        public HIS_PATIENT_TYPE_ALLOW GetByCode(string code, HisPatientTypeAllowSO search)
        {
            HIS_PATIENT_TYPE_ALLOW result = null;

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

        public Dictionary<string, HIS_PATIENT_TYPE_ALLOW> GetDicByCode(HisPatientTypeAllowSO search, CommonParam param)
        {
            Dictionary<string, HIS_PATIENT_TYPE_ALLOW> result = new Dictionary<string, HIS_PATIENT_TYPE_ALLOW>();
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
