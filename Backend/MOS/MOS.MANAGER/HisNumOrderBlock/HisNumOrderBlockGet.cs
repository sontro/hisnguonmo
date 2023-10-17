using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNumOrderBlock
{
    partial class HisNumOrderBlockGet : BusinessBase
    {
        internal HisNumOrderBlockGet()
            : base()
        {

        }

        internal HisNumOrderBlockGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_NUM_ORDER_BLOCK> Get(HisNumOrderBlockFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisNumOrderBlockDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_NUM_ORDER_BLOCK GetById(long id)
        {
            try
            {
                return GetById(id, new HisNumOrderBlockFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_NUM_ORDER_BLOCK GetById(long id, HisNumOrderBlockFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisNumOrderBlockDAO.GetById(id, filter.Query());
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
