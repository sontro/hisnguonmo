using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntigen
{
    partial class HisAntigenGet : BusinessBase
    {
        internal HisAntigenGet()
            : base()
        {

        }

        internal HisAntigenGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ANTIGEN> Get(HisAntigenFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntigenDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ANTIGEN GetById(long id)
        {
            try
            {
                return GetById(id, new HisAntigenFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ANTIGEN GetById(long id, HisAntigenFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntigenDAO.GetById(id, filter.Query());
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
