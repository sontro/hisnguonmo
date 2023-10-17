using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialMaterial
{
    partial class HisMaterialMaterialGet : BusinessBase
    {
        internal HisMaterialMaterialGet()
            : base()
        {

        }

        internal HisMaterialMaterialGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MATERIAL_MATERIAL> Get(HisMaterialMaterialFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMaterialMaterialDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MATERIAL_MATERIAL GetById(long id)
        {
            try
            {
                return GetById(id, new HisMaterialMaterialFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MATERIAL_MATERIAL GetById(long id, HisMaterialMaterialFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMaterialMaterialDAO.GetById(id, filter.Query());
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
