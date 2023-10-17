using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNoneMediService
{
    partial class HisNoneMediServiceGet : BusinessBase
    {
        internal HisNoneMediServiceGet()
            : base()
        {

        }

        internal HisNoneMediServiceGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_NONE_MEDI_SERVICE> Get(HisNoneMediServiceFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisNoneMediServiceDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_NONE_MEDI_SERVICE GetById(long id)
        {
            try
            {
                return GetById(id, new HisNoneMediServiceFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_NONE_MEDI_SERVICE GetById(long id, HisNoneMediServiceFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisNoneMediServiceDAO.GetById(id, filter.Query());
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
