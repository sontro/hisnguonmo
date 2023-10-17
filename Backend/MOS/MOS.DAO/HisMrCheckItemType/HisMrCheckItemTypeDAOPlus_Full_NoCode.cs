using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMrCheckItemType
{
    public partial class HisMrCheckItemTypeDAO : EntityBase
    {
        public List<V_HIS_MR_CHECK_ITEM_TYPE> GetView(HisMrCheckItemTypeSO search, CommonParam param)
        {
            List<V_HIS_MR_CHECK_ITEM_TYPE> result = new List<V_HIS_MR_CHECK_ITEM_TYPE>();
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

        public V_HIS_MR_CHECK_ITEM_TYPE GetViewById(long id, HisMrCheckItemTypeSO search)
        {
            V_HIS_MR_CHECK_ITEM_TYPE result = null;

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
