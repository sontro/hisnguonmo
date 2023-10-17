using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEinvoiceType
{
    partial class HisEinvoiceTypeGet : BusinessBase
    {
        internal List<V_HIS_EINVOICE_TYPE> GetView(HisEinvoiceTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEinvoiceTypeDAO.GetView(filter.Query(), param);
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
