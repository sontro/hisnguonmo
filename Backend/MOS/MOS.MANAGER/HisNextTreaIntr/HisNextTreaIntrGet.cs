using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNextTreaIntr
{
    partial class HisNextTreaIntrGet : BusinessBase
    {
        internal HisNextTreaIntrGet()
            : base()
        {

        }

        internal HisNextTreaIntrGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_NEXT_TREA_INTR> Get(HisNextTreaIntrFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisNextTreaIntrDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_NEXT_TREA_INTR GetById(long id)
        {
            try
            {
                return GetById(id, new HisNextTreaIntrFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_NEXT_TREA_INTR GetById(long id, HisNextTreaIntrFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisNextTreaIntrDAO.GetById(id, filter.Query());
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
