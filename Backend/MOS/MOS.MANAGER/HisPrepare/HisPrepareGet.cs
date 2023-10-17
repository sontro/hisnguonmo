using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPrepare
{
    partial class HisPrepareGet : BusinessBase
    {
        internal HisPrepareGet()
            : base()
        {

        }

        internal HisPrepareGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_PREPARE> Get(HisPrepareFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPrepareDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PREPARE GetById(long id)
        {
            try
            {
                return GetById(id, new HisPrepareFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PREPARE GetById(long id, HisPrepareFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPrepareDAO.GetById(id, filter.Query());
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
