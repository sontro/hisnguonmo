using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisUserAccountBook
{
    public partial class HisUserAccountBookDAO : EntityBase
    {
        public List<V_HIS_USER_ACCOUNT_BOOK> GetView(HisUserAccountBookSO search, CommonParam param)
        {
            List<V_HIS_USER_ACCOUNT_BOOK> result = new List<V_HIS_USER_ACCOUNT_BOOK>();
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

        public V_HIS_USER_ACCOUNT_BOOK GetViewById(long id, HisUserAccountBookSO search)
        {
            V_HIS_USER_ACCOUNT_BOOK result = null;

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
