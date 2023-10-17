using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttGroupBest
{
    partial class HisPtttGroupBestGet : BusinessBase
    {
        internal HisPtttGroupBestGet()
            : base()
        {

        }

        internal HisPtttGroupBestGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_PTTT_GROUP_BEST> Get(HisPtttGroupBestFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPtttGroupBestDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PTTT_GROUP_BEST GetById(long id)
        {
            try
            {
                return GetById(id, new HisPtttGroupBestFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PTTT_GROUP_BEST GetById(long id, HisPtttGroupBestFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPtttGroupBestDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_PTTT_GROUP_BEST> GetByPtttGroupId(long ptttGroupId)
        {
            try
            {
                HisPtttGroupBestFilterQuery filter = new HisPtttGroupBestFilterQuery();
                filter.PTTT_GROUP_ID = ptttGroupId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        internal List<HIS_PTTT_GROUP_BEST> GetByServiceId(long serviceId)
        {
            try
            {
                HisPtttGroupBestFilterQuery filter = new HisPtttGroupBestFilterQuery();
                filter.BED_SERVICE_TYPE_ID = serviceId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
