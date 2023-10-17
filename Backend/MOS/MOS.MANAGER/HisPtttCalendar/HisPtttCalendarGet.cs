using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttCalendar
{
    partial class HisPtttCalendarGet : BusinessBase
    {
        internal HisPtttCalendarGet()
            : base()
        {

        }

        internal HisPtttCalendarGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_PTTT_CALENDAR> Get(HisPtttCalendarFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPtttCalendarDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PTTT_CALENDAR GetById(long id)
        {
            try
            {
                return GetById(id, new HisPtttCalendarFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_PTTT_CALENDAR> GetById(List<long> ids)
        {
            if (IsNotNullOrEmpty(ids))
            {
                HisPtttCalendarFilterQuery filter = new HisPtttCalendarFilterQuery();
                filter.IDs = ids;
                return this.Get(filter);
            }
            return null;
        }

        internal HIS_PTTT_CALENDAR GetById(long id, HisPtttCalendarFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPtttCalendarDAO.GetById(id, filter.Query());
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
