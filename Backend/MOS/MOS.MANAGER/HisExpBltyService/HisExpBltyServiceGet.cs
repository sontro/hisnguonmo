using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpBltyService
{
    partial class HisExpBltyServiceGet : BusinessBase
    {
        internal HisExpBltyServiceGet()
            : base()
        {

        }

        internal HisExpBltyServiceGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_EXP_BLTY_SERVICE> Get(HisExpBltyServiceFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpBltyServiceDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EXP_BLTY_SERVICE GetById(long id)
        {
            try
            {
                return GetById(id, new HisExpBltyServiceFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EXP_BLTY_SERVICE GetById(long id, HisExpBltyServiceFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpBltyServiceDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_EXP_BLTY_SERVICE> GetByExpMestId(long expMestId)
        {
            try
            {
                HisExpBltyServiceFilterQuery filter = new HisExpBltyServiceFilterQuery();
                filter.EXP_MEST_ID = expMestId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_EXP_BLTY_SERVICE> GetByBloodTypeId(long bloodTypeId)
        {
            try
            {
                HisExpBltyServiceFilterQuery filter = new HisExpBltyServiceFilterQuery();
                filter.BLOOD_TYPE_ID = bloodTypeId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_EXP_BLTY_SERVICE> GetByServiceId(long serviceId)
        {
            try
            {
                HisExpBltyServiceFilterQuery filter = new HisExpBltyServiceFilterQuery();
                filter.SERVICE_ID = serviceId;
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
