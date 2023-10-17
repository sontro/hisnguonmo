using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMatyMaty
{
    partial class HisMatyMatyGet : BusinessBase
    {
        internal HisMatyMatyGet()
            : base()
        {

        }

        internal HisMatyMatyGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MATY_MATY> Get(HisMatyMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMatyMatyDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MATY_MATY GetById(long id)
        {
            try
            {
                return GetById(id, new HisMatyMatyFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MATY_MATY GetById(long id, HisMatyMatyFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMatyMatyDAO.GetById(id, filter.Query());
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
