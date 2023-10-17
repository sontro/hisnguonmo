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
    }
}
