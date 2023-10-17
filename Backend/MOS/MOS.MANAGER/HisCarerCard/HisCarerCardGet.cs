using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCarerCard
{
    partial class HisCarerCardGet : BusinessBase
    {
        internal HisCarerCardGet()
            : base()
        {

        }

        internal HisCarerCardGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_CARER_CARD> Get(HisCarerCardFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCarerCardDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CARER_CARD GetById(long id)
        {
            try
            {
                return GetById(id, new HisCarerCardFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CARER_CARD GetById(long id, HisCarerCardFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCarerCardDAO.GetById(id, filter.Query());
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
