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
        internal List<V_HIS_DEATH_CERT_BOOK> GetView(HisDeathCertBookViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDeathCertBookDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_DEATH_CERT_BOOK GetViewById(long id)
        {
            try
            {
                return GetViewById(id, new HisDeathCertBookViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_DEATH_CERT_BOOK GetViewById(long id, HisDeathCertBookViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDeathCertBookDAO.GetViewById(id, filter.Query());
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
