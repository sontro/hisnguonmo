using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskDriver
{
    partial class HisKskDriverGet : BusinessBase
    {
        internal HisKskDriverGet()
            : base()
        {

        }

        internal HisKskDriverGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_KSK_DRIVER> Get(HisKskDriverFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskDriverDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_DRIVER GetById(long id)
        {
            try
            {
                return GetById(id, new HisKskDriverFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_KSK_DRIVER GetById(long id, HisKskDriverFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisKskDriverDAO.GetById(id, filter.Query());
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
