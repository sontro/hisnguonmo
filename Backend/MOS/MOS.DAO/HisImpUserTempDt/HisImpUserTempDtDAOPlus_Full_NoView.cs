using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisImpUserTempDt
{
    public partial class HisImpUserTempDtDAO : EntityBase
    {
        public HIS_IMP_USER_TEMP_DT GetByCode(string code, HisImpUserTempDtSO search)
        {
            HIS_IMP_USER_TEMP_DT result = null;

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

        public Dictionary<string, HIS_IMP_USER_TEMP_DT> GetDicByCode(HisImpUserTempDtSO search, CommonParam param)
        {
            Dictionary<string, HIS_IMP_USER_TEMP_DT> result = new Dictionary<string, HIS_IMP_USER_TEMP_DT>();
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
