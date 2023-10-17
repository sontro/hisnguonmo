using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMrCheckItemType
{
    public partial class HisMrCheckItemTypeDAO : EntityBase
    {
        public HIS_MR_CHECK_ITEM_TYPE GetByCode(string code, HisMrCheckItemTypeSO search)
        {
            HIS_MR_CHECK_ITEM_TYPE result = null;

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

        public Dictionary<string, HIS_MR_CHECK_ITEM_TYPE> GetDicByCode(HisMrCheckItemTypeSO search, CommonParam param)
        {
            Dictionary<string, HIS_MR_CHECK_ITEM_TYPE> result = new Dictionary<string, HIS_MR_CHECK_ITEM_TYPE>();
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
