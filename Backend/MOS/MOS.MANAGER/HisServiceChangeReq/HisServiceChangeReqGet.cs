using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceChangeReq
{
    partial class HisServiceChangeReqGet : BusinessBase
    {
        internal HisServiceChangeReqGet()
            : base()
        {

        }

        internal HisServiceChangeReqGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SERVICE_CHANGE_REQ> Get(HisServiceChangeReqFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceChangeReqDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_CHANGE_REQ GetById(long id)
        {
            try
            {
                return GetById(id, new HisServiceChangeReqFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_CHANGE_REQ GetById(long id, HisServiceChangeReqFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceChangeReqDAO.GetById(id, filter.Query());
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
