using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDosageForm
{
    public partial class HisDosageFormDAO : EntityBase
    {
        public List<V_HIS_DOSAGE_FORM> GetView(HisDosageFormSO search, CommonParam param)
        {
            List<V_HIS_DOSAGE_FORM> result = new List<V_HIS_DOSAGE_FORM>();

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

        public HIS_DOSAGE_FORM GetByCode(string code, HisDosageFormSO search)
        {
            HIS_DOSAGE_FORM result = null;

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
        
        public V_HIS_DOSAGE_FORM GetViewById(long id, HisDosageFormSO search)
        {
            V_HIS_DOSAGE_FORM result = null;

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

        public V_HIS_DOSAGE_FORM GetViewByCode(string code, HisDosageFormSO search)
        {
            V_HIS_DOSAGE_FORM result = null;

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

        public Dictionary<string, HIS_DOSAGE_FORM> GetDicByCode(HisDosageFormSO search, CommonParam param)
        {
            Dictionary<string, HIS_DOSAGE_FORM> result = new Dictionary<string, HIS_DOSAGE_FORM>();
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
