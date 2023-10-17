using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrChecklist
{
    partial class HisMrChecklistGet : BusinessBase
    {
        internal HisMrChecklistGet()
            : base()
        {

        }

        internal HisMrChecklistGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MR_CHECKLIST> Get(HisMrChecklistFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrChecklistDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MR_CHECKLIST GetById(long id)
        {
            try
            {
                return GetById(id, new HisMrChecklistFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MR_CHECKLIST GetById(long id, HisMrChecklistFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrChecklistDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_MR_CHECKLIST> GetByMrCheckItemId(long mrCheckItemId)
        {
            try
            {
                HisMrChecklistFilterQuery filter = new HisMrChecklistFilterQuery();
                filter.MR_CHECK_ITEM_ID = mrCheckItemId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_MR_CHECKLIST> GetByMrCheckItemIds(List<long> mrCheckItemIds)
        {
            try
            {
                HisMrChecklistFilterQuery filter = new HisMrChecklistFilterQuery();
                filter.MR_CHECK_ITEM_IDs = mrCheckItemIds;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_MR_CHECKLIST> GetByMrCheckSummaryId(long mrCheckSummaryId)
        {
             try
            {
                HisMrChecklistFilterQuery filter = new HisMrChecklistFilterQuery();
                filter.MR_CHECK_SUMMARY_ID = mrCheckSummaryId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_MR_CHECKLIST> GetByMrCheckSummaryIds(List<long> mrCheckSummaryIds)
        {
            try
            {
                HisMrChecklistFilterQuery filter = new HisMrChecklistFilterQuery();
                filter.MR_CHECK_SUMMARY_IDs = mrCheckSummaryIds;
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
