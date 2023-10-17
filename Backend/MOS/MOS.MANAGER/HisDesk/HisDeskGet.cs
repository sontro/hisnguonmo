using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDesk
{
    partial class HisDeskGet : BusinessBase
    {
        internal HisDeskGet()
            : base()
        {

        }

        internal HisDeskGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_DESK> Get(HisDeskFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDeskDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DESK GetById(long id)
        {
            try
            {
                return GetById(id, new HisDeskFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_DESK> GetByIds(List<long> ids)
        {
            try
            {
                HisDeskFilterQuery filter = new HisDeskFilterQuery();
                filter.IDs = ids;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DESK GetById(long id, HisDeskFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDeskDAO.GetById(id, filter.Query());
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
