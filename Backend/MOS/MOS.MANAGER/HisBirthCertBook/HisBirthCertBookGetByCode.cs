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
        internal HIS_BIRTH_CERT_BOOK GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisBirthCertBookFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BIRTH_CERT_BOOK GetByCode(string code, HisBirthCertBookFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBirthCertBookDAO.GetByCode(code, filter.Query());
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
