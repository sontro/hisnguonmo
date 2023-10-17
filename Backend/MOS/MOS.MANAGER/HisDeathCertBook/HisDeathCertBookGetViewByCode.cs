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
        internal V_HIS_DEATH_CERT_BOOK GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisDeathCertBookViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_DEATH_CERT_BOOK GetViewByCode(string code, HisDeathCertBookViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDeathCertBookDAO.GetViewByCode(code, filter.Query());
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
