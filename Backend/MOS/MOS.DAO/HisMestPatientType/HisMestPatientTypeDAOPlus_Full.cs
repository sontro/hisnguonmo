using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPatientType
{
    public partial class HisMestPatientTypeDAO : EntityBase
    {
        public List<V_HIS_MEST_PATIENT_TYPE> GetView(HisMestPatientTypeSO search, CommonParam param)
        {
            List<V_HIS_MEST_PATIENT_TYPE> result = new List<V_HIS_MEST_PATIENT_TYPE>();

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

        public HIS_MEST_PATIENT_TYPE GetByCode(string code, HisMestPatientTypeSO search)
        {
            HIS_MEST_PATIENT_TYPE result = null;

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
        
        public V_HIS_MEST_PATIENT_TYPE GetViewById(long id, HisMestPatientTypeSO search)
        {
            V_HIS_MEST_PATIENT_TYPE result = null;

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

        public V_HIS_MEST_PATIENT_TYPE GetViewByCode(string code, HisMestPatientTypeSO search)
        {
            V_HIS_MEST_PATIENT_TYPE result = null;

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

        public Dictionary<string, HIS_MEST_PATIENT_TYPE> GetDicByCode(HisMestPatientTypeSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEST_PATIENT_TYPE> result = new Dictionary<string, HIS_MEST_PATIENT_TYPE>();
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
