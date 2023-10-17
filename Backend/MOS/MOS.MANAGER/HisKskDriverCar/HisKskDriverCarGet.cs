using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskDriverCar
{
    partial class HisKskDriverCarGet : BusinessBase
    {
        internal HisKskDriverCarGet()
            : base()
        {

        }

        internal HisKskDriverCarGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_KSK_DRIVER_CAR> Get(HisKskDriverCarFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskDriverCarDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_DRIVER_CAR GetById(long id)
        {
            try
            {
                return GetById(id, new HisKskDriverCarFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_DRIVER_CAR GetById(long id, HisKskDriverCarFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskDriverCarDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_KSK_DRIVER_CAR> GetByServiceReqId(long serviceReqId)
        {
            try
            {
                HisKskDriverCarFilterQuery filter = new HisKskDriverCarFilterQuery();
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
    }
}
