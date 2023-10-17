using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMetyMety
{
    partial class HisMetyMetyGet : BusinessBase
    {
        internal HisMetyMetyGet()
            : base()
        {

        }

        internal HisMetyMetyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_METY_METY> Get(HisMetyMetyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMetyMetyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_METY_METY GetById(long id)
        {
            try
            {
                return GetById(id, new HisMetyMetyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_METY_METY GetById(long id, HisMetyMetyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMetyMetyDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_METY_METY> GetByMetyProductId(long metyProductId)
        {
            try
            {
                HisMetyMetyFilterQuery filter = new HisMetyMetyFilterQuery();
                filter.METY_PRODUCT_ID = metyProductId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
