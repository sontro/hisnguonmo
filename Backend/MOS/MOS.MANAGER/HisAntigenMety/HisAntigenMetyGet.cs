using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntigenMety
{
    partial class HisAntigenMetyGet : BusinessBase
    {
        internal HisAntigenMetyGet()
            : base()
        {

        }

        internal HisAntigenMetyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_ANTIGEN_METY> Get(HisAntigenMetyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntigenMetyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ANTIGEN_METY GetById(long id)
        {
            try
            {
                return GetById(id, new HisAntigenMetyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ANTIGEN_METY GetById(long id, HisAntigenMetyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAntigenMetyDAO.GetById(id, filter.Query());
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
