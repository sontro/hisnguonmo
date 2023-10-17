using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceHein
{
    partial class HisServiceHeinGet : BusinessBase
    {
        internal HisServiceHeinGet()
            : base()
        {

        }

        internal HisServiceHeinGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SERVICE_HEIN> Get(HisServiceHeinFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceHeinDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_HEIN GetById(long id)
        {
            try
            {
                return GetById(id, new HisServiceHeinFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_HEIN GetById(long id, HisServiceHeinFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceHeinDAO.GetById(id, filter.Query());
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
