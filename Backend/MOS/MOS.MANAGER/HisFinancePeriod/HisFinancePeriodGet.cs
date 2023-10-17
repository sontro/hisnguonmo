using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFinancePeriod
{
    partial class HisFinancePeriodGet : BusinessBase
    {
        internal HisFinancePeriodGet()
            : base()
        {

        }

        internal HisFinancePeriodGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_FINANCE_PERIOD> Get(HisFinancePeriodFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFinancePeriodDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_FINANCE_PERIOD GetById(long id)
        {
            try
            {
                return GetById(id, new HisFinancePeriodFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_FINANCE_PERIOD GetById(long id, HisFinancePeriodFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFinancePeriodDAO.GetById(id, filter.Query());
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
