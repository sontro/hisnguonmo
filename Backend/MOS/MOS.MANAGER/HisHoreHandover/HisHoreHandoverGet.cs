using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreHandover
{
    partial class HisHoreHandoverGet : BusinessBase
    {
        internal HisHoreHandoverGet()
            : base()
        {

        }

        internal HisHoreHandoverGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_HORE_HANDOVER> Get(HisHoreHandoverFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHoreHandoverDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_HORE_HANDOVER GetById(long id)
        {
            try
            {
                return GetById(id, new HisHoreHandoverFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_HORE_HANDOVER GetById(long id, HisHoreHandoverFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHoreHandoverDAO.GetById(id, filter.Query());
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
