using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKsk
{
    partial class HisKskGet : BusinessBase
    {
        internal HisKskGet()
            : base()
        {

        }

        internal HisKskGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_KSK> Get(HisKskFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK GetById(long id)
        {
            try
            {
                return GetById(id, new HisKskFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK GetById(long id, HisKskFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskDAO.GetById(id, filter.Query());
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
