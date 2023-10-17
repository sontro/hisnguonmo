using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrCheckItemType
{
    partial class HisMrCheckItemTypeGet : BusinessBase
    {
        internal HisMrCheckItemTypeGet()
            : base()
        {

        }

        internal HisMrCheckItemTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MR_CHECK_ITEM_TYPE> Get(HisMrCheckItemTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrCheckItemTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MR_CHECK_ITEM_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisMrCheckItemTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MR_CHECK_ITEM_TYPE GetById(long id, HisMrCheckItemTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrCheckItemTypeDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
