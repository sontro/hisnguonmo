using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPatyTrty
{
    partial class HisMestPatyTrtyGet : BusinessBase
    {
        internal HisMestPatyTrtyGet()
            : base()
        {

        }

        internal HisMestPatyTrtyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MEST_PATY_TRTY> Get(HisMestPatyTrtyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestPatyTrtyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEST_PATY_TRTY GetById(long id)
        {
            try
            {
                return GetById(id, new HisMestPatyTrtyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEST_PATY_TRTY GetById(long id, HisMestPatyTrtyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestPatyTrtyDAO.GetById(id, filter.Query());
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
