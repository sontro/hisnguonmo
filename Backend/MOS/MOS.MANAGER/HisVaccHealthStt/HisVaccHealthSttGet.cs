using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccHealthStt
{
    partial class HisVaccHealthSttGet : BusinessBase
    {
        internal HisVaccHealthSttGet()
            : base()
        {

        }

        internal HisVaccHealthSttGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_VACC_HEALTH_STT> Get(HisVaccHealthSttFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccHealthSttDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACC_HEALTH_STT GetById(long id)
        {
            try
            {
                return GetById(id, new HisVaccHealthSttFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACC_HEALTH_STT GetById(long id, HisVaccHealthSttFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccHealthSttDAO.GetById(id, filter.Query());
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
