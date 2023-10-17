using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSurgRemuDetail
{
    partial class HisSurgRemuDetailGet : BusinessBase
    {
        internal HisSurgRemuDetailGet()
            : base()
        {

        }

        internal HisSurgRemuDetailGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SURG_REMU_DETAIL> Get(HisSurgRemuDetailFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSurgRemuDetailDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SURG_REMU_DETAIL GetById(long id)
        {
            try
            {
                return GetById(id, new HisSurgRemuDetailFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SURG_REMU_DETAIL GetById(long id, HisSurgRemuDetailFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSurgRemuDetailDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SURG_REMU_DETAIL> GetBySurgRemunerationId(long surgRemunerationId)
        {
            HisSurgRemuDetailFilterQuery filter = new HisSurgRemuDetailFilterQuery();
            filter.SURG_REMUNERATION_ID = surgRemunerationId;
            return this.Get(filter);
        }
    }
}
