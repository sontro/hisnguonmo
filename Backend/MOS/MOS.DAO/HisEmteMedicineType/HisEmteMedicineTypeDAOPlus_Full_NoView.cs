using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmteMedicineType
{
    public partial class HisEmteMedicineTypeDAO : EntityBase
    {
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
