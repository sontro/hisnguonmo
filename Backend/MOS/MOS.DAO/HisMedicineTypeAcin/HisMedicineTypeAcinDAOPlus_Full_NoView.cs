using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineTypeAcin
{
    public partial class HisMedicineTypeAcinDAO : EntityBase
    {
        public HIS_MEDICINE_TYPE_ACIN GetByCode(string code, HisMedicineTypeAcinSO search)
        {
            HIS_MEDICINE_TYPE_ACIN result = null;

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

        public Dictionary<string, HIS_MEDICINE_TYPE_ACIN> GetDicByCode(HisMedicineTypeAcinSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEDICINE_TYPE_ACIN> result = new Dictionary<string, HIS_MEDICINE_TYPE_ACIN>();
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
