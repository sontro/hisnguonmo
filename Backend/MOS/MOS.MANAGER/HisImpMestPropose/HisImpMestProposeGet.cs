using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestPropose
{
    partial class HisImpMestProposeGet : BusinessBase
    {
        internal HisImpMestProposeGet()
            : base()
        {

        }

        internal HisImpMestProposeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_IMP_MEST_PROPOSE> Get(HisImpMestProposeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestProposeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_IMP_MEST_PROPOSE GetById(long id)
        {
            try
            {
                return GetById(id, new HisImpMestProposeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_IMP_MEST_PROPOSE GetById(long id, HisImpMestProposeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestProposeDAO.GetById(id, filter.Query());
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
