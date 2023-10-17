using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCareTemp
{
    partial class HisCareTempGet : BusinessBase
    {
        internal HisCareTempGet()
            : base()
        {

        }

        internal HisCareTempGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_CARE_TEMP> Get(HisCareTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCareTempDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CARE_TEMP GetById(long id)
        {
            try
            {
                return GetById(id, new HisCareTempFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CARE_TEMP GetById(long id, HisCareTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCareTempDAO.GetById(id, filter.Query());
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
