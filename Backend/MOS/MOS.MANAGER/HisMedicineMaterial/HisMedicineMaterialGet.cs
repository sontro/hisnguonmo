using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineMaterial
{
    partial class HisMedicineMaterialGet : BusinessBase
    {
        internal HisMedicineMaterialGet()
            : base()
        {

        }

        internal HisMedicineMaterialGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MEDICINE_MATERIAL> Get(HisMedicineMaterialFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineMaterialDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICINE_MATERIAL GetById(long id)
        {
            try
            {
                return GetById(id, new HisMedicineMaterialFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEDICINE_MATERIAL GetById(long id, HisMedicineMaterialFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMedicineMaterialDAO.GetById(id, filter.Query());
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
