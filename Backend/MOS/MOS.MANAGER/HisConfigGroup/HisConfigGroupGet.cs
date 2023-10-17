using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisConfigGroup
{
    partial class HisConfigGroupGet : BusinessBase
    {
        internal HisConfigGroupGet()
            : base()
        {

        }

        internal HisConfigGroupGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_CONFIG_GROUP> Get(HisConfigGroupFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisConfigGroupDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CONFIG_GROUP GetById(long id)
        {
            try
            {
                return GetById(id, new HisConfigGroupFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CONFIG_GROUP GetById(long id, HisConfigGroupFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisConfigGroupDAO.GetById(id, filter.Query());
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
