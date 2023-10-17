using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestMatyDepa
{
    partial class HisMestMatyDepaGet : BusinessBase
    {
        internal HisMestMatyDepaGet()
            : base()
        {

        }

        internal HisMestMatyDepaGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MEST_MATY_DEPA> Get(HisMestMatyDepaFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestMatyDepaDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEST_MATY_DEPA GetById(long id)
        {
            try
            {
                return GetById(id, new HisMestMatyDepaFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEST_MATY_DEPA GetById(long id, HisMestMatyDepaFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestMatyDepaDAO.GetById(id, filter.Query());
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
