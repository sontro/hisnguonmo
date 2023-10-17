using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreHandoverStt
{
    partial class HisHoreHandoverSttGet : BusinessBase
    {
        internal HisHoreHandoverSttGet()
            : base()
        {

        }

        internal HisHoreHandoverSttGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_HORE_HANDOVER_STT> Get(HisHoreHandoverSttFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHoreHandoverSttDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_HORE_HANDOVER_STT GetById(long id)
        {
            try
            {
                return GetById(id, new HisHoreHandoverSttFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_HORE_HANDOVER_STT GetById(long id, HisHoreHandoverSttFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHoreHandoverSttDAO.GetById(id, filter.Query());
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
