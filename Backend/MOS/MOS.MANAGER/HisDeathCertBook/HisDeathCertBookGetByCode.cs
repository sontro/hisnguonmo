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
        internal HIS_DEATH_CERT_BOOK GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisDeathCertBookFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEATH_CERT_BOOK GetByCode(string code, HisDeathCertBookFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDeathCertBookDAO.GetByCode(code, filter.Query());
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
