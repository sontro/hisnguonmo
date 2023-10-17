using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUnlimitType
{
    partial class HisUnlimitTypeGet : BusinessBase
    {
        internal HisUnlimitTypeGet()
            : base()
        {

        }

        internal HisUnlimitTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_UNLIMIT_TYPE> Get(HisUnlimitTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisUnlimitTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_UNLIMIT_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisUnlimitTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_UNLIMIT_TYPE GetById(long id, HisUnlimitTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisUnlimitTypeDAO.GetById(id, filter.Query());
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
