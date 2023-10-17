using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskService
{
    partial class HisKskServiceGet : BusinessBase
    {
        internal HisKskServiceGet()
            : base()
        {

        }

        internal HisKskServiceGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_KSK_SERVICE> Get(HisKskServiceFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskServiceDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_SERVICE GetById(long id)
        {
            try
            {
                return GetById(id, new HisKskServiceFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_SERVICE GetById(long id, HisKskServiceFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskServiceDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_KSK_SERVICE> GetByKskId(long kskId)
        {
            try
            {
                HisKskServiceFilterQuery filter = new HisKskServiceFilterQuery();
                filter.KSK_ID = kskId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_KSK_SERVICE> GetByKskIds(List<long> kskIds)
        {
            try
            {
                HisKskServiceFilterQuery filter = new HisKskServiceFilterQuery();
                filter.KSK_IDs = kskIds;
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
