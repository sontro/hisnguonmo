using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreHoha
{
    partial class HisHoreHohaGet : BusinessBase
    {
        internal HisHoreHohaGet()
            : base()
        {

        }

        internal HisHoreHohaGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_HORE_HOHA> Get(HisHoreHohaFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHoreHohaDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_HORE_HOHA GetById(long id)
        {
            try
            {
                return GetById(id, new HisHoreHohaFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_HORE_HOHA GetById(long id, HisHoreHohaFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHoreHohaDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_HORE_HOHA> GetByHoreHandoverId(long horeHandoverId)
        {
            try
            {
                HisHoreHohaFilterQuery filter = new HisHoreHohaFilterQuery();
                filter.HORE_HANDOVER_ID = horeHandoverId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
