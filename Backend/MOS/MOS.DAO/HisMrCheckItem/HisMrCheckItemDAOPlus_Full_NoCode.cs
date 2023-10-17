using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMrCheckItem
{
    public partial class HisMrCheckItemDAO : EntityBase
    {
        public List<V_HIS_MR_CHECK_ITEM> GetView(HisMrCheckItemSO search, CommonParam param)
        {
            List<V_HIS_MR_CHECK_ITEM> result = new List<V_HIS_MR_CHECK_ITEM>();
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

        public V_HIS_MR_CHECK_ITEM GetViewById(long id, HisMrCheckItemSO search)
        {
            V_HIS_MR_CHECK_ITEM result = null;

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
