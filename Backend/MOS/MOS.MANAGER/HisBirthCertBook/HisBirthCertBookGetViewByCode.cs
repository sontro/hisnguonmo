using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBirthCertBook
{
    partial class HisBirthCertBookGet : BusinessBase
    {
        internal V_HIS_BIRTH_CERT_BOOK GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisBirthCertBookViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_BIRTH_CERT_BOOK GetViewByCode(string code, HisBirthCertBookViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBirthCertBookDAO.GetViewByCode(code, filter.Query());
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
