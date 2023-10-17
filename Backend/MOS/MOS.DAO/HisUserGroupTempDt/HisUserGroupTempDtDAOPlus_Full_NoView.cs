using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisUserGroupTempDt
{
    public partial class HisUserGroupTempDtDAO : EntityBase
    {
        public HIS_USER_GROUP_TEMP_DT GetByCode(string code, HisUserGroupTempDtSO search)
        {
            HIS_USER_GROUP_TEMP_DT result = null;

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

        public Dictionary<string, HIS_USER_GROUP_TEMP_DT> GetDicByCode(HisUserGroupTempDtSO search, CommonParam param)
        {
            Dictionary<string, HIS_USER_GROUP_TEMP_DT> result = new Dictionary<string, HIS_USER_GROUP_TEMP_DT>();
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
