using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmteMedicineType
{
    public partial class HisEmteMedicineTypeDAO : EntityBase
    {
        public List<V_HIS_EMTE_MEDICINE_TYPE> GetView(HisEmteMedicineTypeSO search, CommonParam param)
        {
            List<V_HIS_EMTE_MEDICINE_TYPE> result = new List<V_HIS_EMTE_MEDICINE_TYPE>();

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

        public HIS_EMTE_MEDICINE_TYPE GetByCode(string code, HisEmteMedicineTypeSO search)
        {
            HIS_EMTE_MEDICINE_TYPE result = null;

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
        
        public V_HIS_EMTE_MEDICINE_TYPE GetViewById(long id, HisEmteMedicineTypeSO search)
        {
            V_HIS_EMTE_MEDICINE_TYPE result = null;

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

        public V_HIS_EMTE_MEDICINE_TYPE GetViewByCode(string code, HisEmteMedicineTypeSO search)
        {
            V_HIS_EMTE_MEDICINE_TYPE result = null;

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

        public Dictionary<string, HIS_EMTE_MEDICINE_TYPE> GetDicByCode(HisEmteMedicineTypeSO search, CommonParam param)
        {
            Dictionary<string, HIS_EMTE_MEDICINE_TYPE> result = new Dictionary<string, HIS_EMTE_MEDICINE_TYPE>();
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
