using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskPeriodDriver
{
    partial class HisKskPeriodDriverGet : BusinessBase
    {
        internal HisKskPeriodDriverGet()
            : base()
        {

        }

        internal HisKskPeriodDriverGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_KSK_PERIOD_DRIVER> Get(HisKskPeriodDriverFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskPeriodDriverDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_PERIOD_DRIVER GetById(long id)
        {
            try
            {
                return GetById(id, new HisKskPeriodDriverFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_PERIOD_DRIVER GetById(long id, HisKskPeriodDriverFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskPeriodDriverDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_KSK_PERIOD_DRIVER> GetByServiceReqId(long serviceReqId)
        {
            try
            {
                HisKskPeriodDriverFilterQuery filter = new HisKskPeriodDriverFilterQuery();
                filter.SERVICE_REQ_ID = serviceReqId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_KSK_PERIOD_DRIVER> GetByLicenseClassId(long licenseClassId)
        {
            try
            {
                HisKskPeriodDriverFilterQuery filter = new HisKskPeriodDriverFilterQuery();
                filter.LICENSE_CLASS_ID = licenseClassId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_KSK_PERIOD_DRIVER> GetByLicenseClassIds(List<long> licenseClassIds)
        {
            try
            {
                HisKskPeriodDriverFilterQuery filter = new HisKskPeriodDriverFilterQuery();
                filter.LICENSE_CLASS_IDs = licenseClassIds;
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
