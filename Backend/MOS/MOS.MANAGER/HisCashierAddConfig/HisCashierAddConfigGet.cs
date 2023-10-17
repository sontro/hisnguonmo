using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCashierAddConfig
{
    partial class HisCashierAddConfigGet : BusinessBase
    {
        internal HisCashierAddConfigGet()
            : base()
        {

        }

        internal HisCashierAddConfigGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_CASHIER_ADD_CONFIG> Get(HisCashierAddConfigFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCashierAddConfigDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CASHIER_ADD_CONFIG GetById(long id)
        {
            try
            {
                return GetById(id, new HisCashierAddConfigFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CASHIER_ADD_CONFIG GetById(long id, HisCashierAddConfigFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCashierAddConfigDAO.GetById(id, filter.Query());
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
