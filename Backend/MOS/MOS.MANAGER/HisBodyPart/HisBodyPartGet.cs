using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBodyPart
{
    partial class HisBodyPartGet : BusinessBase
    {
        internal HisBodyPartGet()
            : base()
        {

        }

        internal HisBodyPartGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_BODY_PART> Get(HisBodyPartFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBodyPartDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BODY_PART GetById(long id)
        {
            try
            {
                return GetById(id, new HisBodyPartFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BODY_PART GetById(long id, HisBodyPartFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBodyPartDAO.GetById(id, filter.Query());
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
