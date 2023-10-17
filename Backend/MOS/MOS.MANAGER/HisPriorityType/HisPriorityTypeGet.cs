using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPriorityType
{
    partial class HisPriorityTypeGet : BusinessBase
    {
        internal HisPriorityTypeGet()
            : base()
        {

        }

        internal HisPriorityTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_PRIORITY_TYPE> Get(HisPriorityTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPriorityTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PRIORITY_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisPriorityTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PRIORITY_TYPE GetById(long id, HisPriorityTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPriorityTypeDAO.GetById(id, filter.Query());
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
