using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrCheckItem
{
    partial class HisMrCheckItemGet : BusinessBase
    {
        internal HisMrCheckItemGet()
            : base()
        {

        }

        internal HisMrCheckItemGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MR_CHECK_ITEM> Get(HisMrCheckItemFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrCheckItemDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MR_CHECK_ITEM GetById(long id)
        {
            try
            {
                return GetById(id, new HisMrCheckItemFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MR_CHECK_ITEM GetById(long id, HisMrCheckItemFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrCheckItemDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_MR_CHECK_ITEM> GetByMrCheckItemTypeId(long mrCheckItemTypeId)
        {
            try
            {
                HisMrCheckItemFilterQuery filter = new HisMrCheckItemFilterQuery();
                filter.CHECK_ITEM_TYPE_ID = mrCheckItemTypeId;
                return this.Get(filter);
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
