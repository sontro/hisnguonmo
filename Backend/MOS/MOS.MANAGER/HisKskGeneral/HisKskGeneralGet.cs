using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskGeneral
{
    partial class HisKskGeneralGet : BusinessBase
    {
        internal HisKskGeneralGet()
            : base()
        {

        }

        internal HisKskGeneralGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_KSK_GENERAL> Get(HisKskGeneralFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskGeneralDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_GENERAL GetById(long id)
        {
            try
            {
                return GetById(id, new HisKskGeneralFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_GENERAL GetById(long id, HisKskGeneralFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskGeneralDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_KSK_GENERAL> GetByServiceReqId(long serviceReqId)
        {
            try
            {
                HisKskGeneralFilterQuery filter = new HisKskGeneralFilterQuery();
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
