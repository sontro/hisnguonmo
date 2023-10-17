using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialTypeMap
{
    partial class HisMaterialTypeMapGet : BusinessBase
    {
        internal HisMaterialTypeMapGet()
            : base()
        {

        }

        internal HisMaterialTypeMapGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MATERIAL_TYPE_MAP> Get(HisMaterialTypeMapFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMaterialTypeMapDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MATERIAL_TYPE_MAP GetById(long id)
        {
            try
            {
                return GetById(id, new HisMaterialTypeMapFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MATERIAL_TYPE_MAP GetById(long id, HisMaterialTypeMapFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMaterialTypeMapDAO.GetById(id, filter.Query());
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
