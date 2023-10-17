using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentEndTypeExt
{
    public partial class HisTreatmentEndTypeExtDAO : EntityBase
    {
        public HIS_TREATMENT_END_TYPE_EXT GetByCode(string code, HisTreatmentEndTypeExtSO search)
        {
            HIS_TREATMENT_END_TYPE_EXT result = null;

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

        public Dictionary<string, HIS_TREATMENT_END_TYPE_EXT> GetDicByCode(HisTreatmentEndTypeExtSO search, CommonParam param)
        {
            Dictionary<string, HIS_TREATMENT_END_TYPE_EXT> result = new Dictionary<string, HIS_TREATMENT_END_TYPE_EXT>();
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
