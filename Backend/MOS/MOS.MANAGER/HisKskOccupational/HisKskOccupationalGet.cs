using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskOccupational
{
    partial class HisKskOccupationalGet : BusinessBase
    {
        internal HisKskOccupationalGet()
            : base()
        {

        }

        internal HisKskOccupationalGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_KSK_OCCUPATIONAL> Get(HisKskOccupationalFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskOccupationalDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_OCCUPATIONAL GetById(long id)
        {
            try
            {
                return GetById(id, new HisKskOccupationalFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_OCCUPATIONAL GetById(long id, HisKskOccupationalFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskOccupationalDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_KSK_OCCUPATIONAL> GetByServiceReqId(long serviceReqId)
        {
            try
            {
                HisKskOccupationalFilterQuery filter = new HisKskOccupationalFilterQuery();
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
