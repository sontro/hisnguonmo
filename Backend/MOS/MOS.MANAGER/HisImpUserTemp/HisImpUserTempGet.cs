using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpUserTemp
{
    partial class HisImpUserTempGet : BusinessBase
    {
        internal HisImpUserTempGet()
            : base()
        {

        }

        internal HisImpUserTempGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_IMP_USER_TEMP> Get(HisImpUserTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpUserTempDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_IMP_USER_TEMP GetById(long id)
        {
            try
            {
                return GetById(id, new HisImpUserTempFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_IMP_USER_TEMP GetById(long id, HisImpUserTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpUserTempDAO.GetById(id, filter.Query());
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
