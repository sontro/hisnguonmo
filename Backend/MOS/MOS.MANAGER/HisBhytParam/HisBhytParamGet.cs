using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBhytParam
{
    partial class HisBhytParamGet : BusinessBase
    {
        internal HisBhytParamGet()
            : base()
        {

        }

        internal HisBhytParamGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_BHYT_PARAM> Get(HisBhytParamFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBhytParamDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BHYT_PARAM GetById(long id)
        {
            try
            {
                return GetById(id, new HisBhytParamFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BHYT_PARAM GetById(long id, HisBhytParamFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBhytParamDAO.GetById(id, filter.Query());
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
