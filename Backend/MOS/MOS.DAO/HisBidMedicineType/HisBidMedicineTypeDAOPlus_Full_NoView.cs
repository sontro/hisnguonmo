using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBidMedicineType
{
    public partial class HisBidMedicineTypeDAO : EntityBase
    {
        public HIS_BID_MEDICINE_TYPE GetByCode(string code, HisBidMedicineTypeSO search)
        {
            HIS_BID_MEDICINE_TYPE result = null;

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

        public Dictionary<string, HIS_BID_MEDICINE_TYPE> GetDicByCode(HisBidMedicineTypeSO search, CommonParam param)
        {
            Dictionary<string, HIS_BID_MEDICINE_TYPE> result = new Dictionary<string, HIS_BID_MEDICINE_TYPE>();
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
