using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMetyMaty
{
    partial class HisMetyMatyGet : BusinessBase
    {
        internal HisMetyMatyGet()
            : base()
        {

        }

        internal HisMetyMatyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_METY_MATY> Get(HisMetyMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMetyMatyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_METY_MATY GetById(long id)
        {
            try
            {
                return GetById(id, new HisMetyMatyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_METY_MATY GetById(long id, HisMetyMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMetyMatyDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_METY_MATY> GetByMetyProductId(long metyProductId)
        {
            try
            {
                HisMetyMatyFilterQuery filter = new HisMetyMatyFilterQuery();
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
