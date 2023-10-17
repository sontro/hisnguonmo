using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicalContract
{
    public partial class HisMedicalContractDAO : EntityBase
    {
        public List<V_HIS_MEDICAL_CONTRACT> GetView(HisMedicalContractSO search, CommonParam param)
        {
            List<V_HIS_MEDICAL_CONTRACT> result = new List<V_HIS_MEDICAL_CONTRACT>();

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

        public HIS_MEDICAL_CONTRACT GetByCode(string code, HisMedicalContractSO search)
        {
            HIS_MEDICAL_CONTRACT result = null;

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
        
        public V_HIS_MEDICAL_CONTRACT GetViewById(long id, HisMedicalContractSO search)
        {
            V_HIS_MEDICAL_CONTRACT result = null;

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

        public V_HIS_MEDICAL_CONTRACT GetViewByCode(string code, HisMedicalContractSO search)
        {
            V_HIS_MEDICAL_CONTRACT result = null;

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

        public Dictionary<string, HIS_MEDICAL_CONTRACT> GetDicByCode(HisMedicalContractSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEDICAL_CONTRACT> result = new Dictionary<string, HIS_MEDICAL_CONTRACT>();
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
