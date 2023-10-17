using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPetroleum
{
    partial class HisPetroleumGet : BusinessBase
    {
        internal HisPetroleumGet()
            : base()
        {

        }

        internal HisPetroleumGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_PETROLEUM> Get(HisPetroleumFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPetroleumDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PETROLEUM GetById(long id)
        {
            try
            {
                return GetById(id, new HisPetroleumFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PETROLEUM GetById(long id, HisPetroleumFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPetroleumDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PETROLEUM GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisPetroleumFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PETROLEUM GetByCode(string code, HisPetroleumFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPetroleumDAO.GetByCode(code, filter.Query());
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
