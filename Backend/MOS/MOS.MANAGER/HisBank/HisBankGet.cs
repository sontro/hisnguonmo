using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBank
{
    partial class HisBankGet : BusinessBase
    {
        internal HisBankGet()
            : base()
        {

        }

        internal HisBankGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_BANK> Get(HisBankFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBankDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BANK GetById(long id)
        {
            try
            {
                return GetById(id, new HisBankFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BANK GetById(long id, HisBankFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBankDAO.GetById(id, filter.Query());
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
