using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsToken
{
    partial class AcsTokenGet : BusinessBase
    {
        internal AcsTokenGet()
            : base()
        {

        }

        internal AcsTokenGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<ACS_TOKEN> Get(AcsTokenFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsTokenDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal ACS_TOKEN GetById(long id)
        {
            try
            {
                return GetById(id, new AcsTokenFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal ACS_TOKEN GetById(long id, AcsTokenFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsTokenDAO.GetById(id, filter.Query());
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
