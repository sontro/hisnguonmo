using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationSumStt
{
    partial class HisRationSumSttGet : BusinessBase
    {
        internal HisRationSumSttGet()
            : base()
        {

        }

        internal HisRationSumSttGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_RATION_SUM_STT> Get(HisRationSumSttFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationSumSttDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_RATION_SUM_STT GetById(long id)
        {
            try
            {
                return GetById(id, new HisRationSumSttFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_RATION_SUM_STT GetById(long id, HisRationSumSttFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationSumSttDAO.GetById(id, filter.Query());
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
