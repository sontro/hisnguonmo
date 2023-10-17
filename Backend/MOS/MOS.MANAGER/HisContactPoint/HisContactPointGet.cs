using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContactPoint
{
    partial class HisContactPointGet : BusinessBase
    {
        internal HisContactPointGet()
            : base()
        {

        }

        internal HisContactPointGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_CONTACT_POINT> Get(HisContactPointFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisContactPointDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_CONTACT_POINT> GetByIds(List<long> ids)
        {
            try
            {
                if (IsNotNullOrEmpty(ids))
                {
                    HisContactPointFilterQuery filter = new HisContactPointFilterQuery();
                    filter.IDs = ids;
                    return DAOWorker.HisContactPointDAO.Get(filter.Query(), param);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CONTACT_POINT GetById(long id)
        {
            try
            {
                return GetById(id, new HisContactPointFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CONTACT_POINT GetById(long id, HisContactPointFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisContactPointDAO.GetById(id, filter.Query());
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
