using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContact
{
    partial class HisContactGet : BusinessBase
    {
        internal HisContactGet()
            : base()
        {

        }

        internal HisContactGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_CONTACT> Get(HisContactFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisContactDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CONTACT GetById(long id)
        {
            try
            {
                return GetById(id, new HisContactFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CONTACT GetById(long id, HisContactFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisContactDAO.GetById(id, filter.Query());
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
