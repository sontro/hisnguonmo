using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVareVart
{
    partial class HisVareVartGet : BusinessBase
    {
        internal HisVareVartGet()
            : base()
        {

        }

        internal HisVareVartGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_VARE_VART> Get(HisVareVartFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVareVartDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VARE_VART GetById(long id)
        {
            try
            {
                return GetById(id, new HisVareVartFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VARE_VART GetById(long id, HisVareVartFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVareVartDAO.GetById(id, filter.Query());
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
