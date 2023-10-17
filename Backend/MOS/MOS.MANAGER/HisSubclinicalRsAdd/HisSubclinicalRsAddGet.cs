using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSubclinicalRsAdd
{
    partial class HisSubclinicalRsAddGet : BusinessBase
    {
        internal HisSubclinicalRsAddGet()
            : base()
        {

        }

        internal HisSubclinicalRsAddGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SUBCLINICAL_RS_ADD> Get(HisSubclinicalRsAddFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSubclinicalRsAddDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SUBCLINICAL_RS_ADD GetById(long id)
        {
            try
            {
                return GetById(id, new HisSubclinicalRsAddFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SUBCLINICAL_RS_ADD GetById(long id, HisSubclinicalRsAddFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSubclinicalRsAddDAO.GetById(id, filter.Query());
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
