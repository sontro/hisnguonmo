using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationSum
{
    partial class HisRationSumGet : BusinessBase
    {
        internal HisRationSumGet()
            : base()
        {

        }

        internal HisRationSumGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_RATION_SUM> Get(HisRationSumFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationSumDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_RATION_SUM GetById(long id)
        {
            try
            {
                return GetById(id, new HisRationSumFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_RATION_SUM GetById(long id, HisRationSumFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationSumDAO.GetById(id, filter.Query());
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
