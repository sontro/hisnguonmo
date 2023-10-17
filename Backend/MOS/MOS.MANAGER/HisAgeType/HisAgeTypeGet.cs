using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAgeType
{
    partial class HisAgeTypeGet : BusinessBase
    {
        internal HisAgeTypeGet()
            : base()
        {

        }

        internal HisAgeTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_AGE_TYPE> Get(HisAgeTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAgeTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_AGE_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisAgeTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_AGE_TYPE GetById(long id, HisAgeTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAgeTypeDAO.GetById(id, filter.Query());
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
