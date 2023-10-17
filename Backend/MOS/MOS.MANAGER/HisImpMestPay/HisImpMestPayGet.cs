using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestPay
{
    partial class HisImpMestPayGet : BusinessBase
    {
        internal HisImpMestPayGet()
            : base()
        {

        }

        internal HisImpMestPayGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_IMP_MEST_PAY> Get(HisImpMestPayFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestPayDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_IMP_MEST_PAY GetById(long id)
        {
            try
            {
                return GetById(id, new HisImpMestPayFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_IMP_MEST_PAY GetById(long id, HisImpMestPayFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestPayDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_IMP_MEST_PAY> GetByImpMestProposeId(long impMestProposeId)
        {
            try
            {
                HisImpMestPayFilterQuery filter = new HisImpMestPayFilterQuery();
                filter.IMP_MEST_PROPOSE_ID = impMestProposeId;
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
