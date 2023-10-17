using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBranchTime
{
    partial class HisBranchTimeGet : BusinessBase
    {
        internal HisBranchTimeGet()
            : base()
        {

        }

        internal HisBranchTimeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_BRANCH_TIME> Get(HisBranchTimeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBranchTimeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BRANCH_TIME GetById(long id)
        {
            try
            {
                return GetById(id, new HisBranchTimeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BRANCH_TIME GetById(long id, HisBranchTimeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBranchTimeDAO.GetById(id, filter.Query());
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
