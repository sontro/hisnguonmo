using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFormTypeCfg
{
    partial class HisFormTypeCfgGet : BusinessBase
    {
        internal HisFormTypeCfgGet()
            : base()
        {

        }

        internal HisFormTypeCfgGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_FORM_TYPE_CFG> Get(HisFormTypeCfgFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFormTypeCfgDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_FORM_TYPE_CFG GetById(long id)
        {
            try
            {
                return GetById(id, new HisFormTypeCfgFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_FORM_TYPE_CFG GetById(long id, HisFormTypeCfgFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFormTypeCfgDAO.GetById(id, filter.Query());
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
