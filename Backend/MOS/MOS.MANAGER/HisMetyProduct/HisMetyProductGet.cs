using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMetyProduct
{
    partial class HisMetyProductGet : BusinessBase
    {
        internal HisMetyProductGet()
            : base()
        {

        }

        internal HisMetyProductGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_METY_PRODUCT> Get(HisMetyProductFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMetyProductDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_METY_PRODUCT GetById(long id)
        {
            try
            {
                return GetById(id, new HisMetyProductFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_METY_PRODUCT GetById(long id, HisMetyProductFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMetyProductDAO.GetById(id, filter.Query());
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
