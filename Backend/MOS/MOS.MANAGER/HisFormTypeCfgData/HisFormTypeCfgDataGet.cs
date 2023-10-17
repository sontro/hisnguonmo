using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFormTypeCfgData
{
    partial class HisFormTypeCfgDataGet : BusinessBase
    {
        internal HisFormTypeCfgDataGet()
            : base()
        {

        }

        internal HisFormTypeCfgDataGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_FORM_TYPE_CFG_DATA> Get(HisFormTypeCfgDataFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFormTypeCfgDataDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_FORM_TYPE_CFG_DATA GetById(long id)
        {
            try
            {
                return GetById(id, new HisFormTypeCfgDataFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_FORM_TYPE_CFG_DATA GetById(long id, HisFormTypeCfgDataFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFormTypeCfgDataDAO.GetById(id, filter.Query());
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
