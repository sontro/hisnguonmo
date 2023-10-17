using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDeathCertBook
{
    partial class HisDeathCertBookGet : BusinessBase
    {
        internal HisDeathCertBookGet()
            : base()
        {

        }

        internal HisDeathCertBookGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_DEATH_CERT_BOOK> Get(HisDeathCertBookFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDeathCertBookDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEATH_CERT_BOOK GetById(long id)
        {
            try
            {
                return GetById(id, new HisDeathCertBookFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEATH_CERT_BOOK GetById(long id, HisDeathCertBookFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDeathCertBookDAO.GetById(id, filter.Query());
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
