using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBltyService
{
    partial class HisBltyServiceGet : BusinessBase
    {
        internal HisBltyServiceGet()
            : base()
        {

        }

        internal HisBltyServiceGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_BLTY_SERVICE> Get(HisBltyServiceFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBltyServiceDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BLTY_SERVICE GetById(long id)
        {
            try
            {
                return GetById(id, new HisBltyServiceFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BLTY_SERVICE GetById(long id, HisBltyServiceFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBltyServiceDAO.GetById(id, filter.Query());
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
