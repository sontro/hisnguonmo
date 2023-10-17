using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisUserGroupTemp
{
    public partial class HisUserGroupTempDAO : EntityBase
    {
        public List<V_HIS_USER_GROUP_TEMP> GetView(HisUserGroupTempSO search, CommonParam param)
        {
            List<V_HIS_USER_GROUP_TEMP> result = new List<V_HIS_USER_GROUP_TEMP>();

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

        public HIS_USER_GROUP_TEMP GetByCode(string code, HisUserGroupTempSO search)
        {
            HIS_USER_GROUP_TEMP result = null;

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
        
        public V_HIS_USER_GROUP_TEMP GetViewById(long id, HisUserGroupTempSO search)
        {
            V_HIS_USER_GROUP_TEMP result = null;

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

        public V_HIS_USER_GROUP_TEMP GetViewByCode(string code, HisUserGroupTempSO search)
        {
            V_HIS_USER_GROUP_TEMP result = null;

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

        public Dictionary<string, HIS_USER_GROUP_TEMP> GetDicByCode(HisUserGroupTempSO search, CommonParam param)
        {
            Dictionary<string, HIS_USER_GROUP_TEMP> result = new Dictionary<string, HIS_USER_GROUP_TEMP>();
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
