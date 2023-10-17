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
        internal List<V_HIS_BIRTH_CERT_BOOK> GetView(HisBirthCertBookViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBirthCertBookDAO.GetView(filter.Query(), param);
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
