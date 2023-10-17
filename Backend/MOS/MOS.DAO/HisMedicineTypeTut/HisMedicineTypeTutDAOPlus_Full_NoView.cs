using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineTypeTut
{
    public partial class HisMedicineTypeTutDAO : EntityBase
    {
        public HIS_MEDICINE_TYPE_TUT GetByCode(string code, HisMedicineTypeTutSO search)
        {
            HIS_MEDICINE_TYPE_TUT result = null;

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

        public Dictionary<string, HIS_MEDICINE_TYPE_TUT> GetDicByCode(HisMedicineTypeTutSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEDICINE_TYPE_TUT> result = new Dictionary<string, HIS_MEDICINE_TYPE_TUT>();
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
