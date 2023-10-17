using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAdr
{
    partial class HisAdrGet : BusinessBase
    {
        internal HisAdrGet()
            : base()
        {

        }

        internal HisAdrGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ADR> Get(HisAdrFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAdrDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ADR GetById(long id)
        {
            try
            {
                return GetById(id, new HisAdrFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ADR GetById(long id, HisAdrFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAdrDAO.GetById(id, filter.Query());
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
