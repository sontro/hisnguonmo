using Inventec.Common.Logging;
using Inventec.Core;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.SdaCustomizeButton
{
    partial class SdaCustomizeButtonGet : BusinessBase
    {
        internal SdaCustomizeButtonGet()
            : base()
        {

        }

        internal SdaCustomizeButtonGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<SDA_CUSTOMIZE_BUTTON> Get(SdaCustomizeButtonFilterQuery filter)
        {
            try
            {
                return DAOWorker.SdaCustomizeButtonDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal SDA_CUSTOMIZE_BUTTON GetById(long id)
        {
            try
            {
                return GetById(id, new SdaCustomizeButtonFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal SDA_CUSTOMIZE_BUTTON GetById(long id, SdaCustomizeButtonFilterQuery filter)
        {
            try
            {
                return DAOWorker.SdaCustomizeButtonDAO.GetById(id, filter.Query());
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
