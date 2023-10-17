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
        internal HIS_EINVOICE_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisEinvoiceTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EINVOICE_TYPE GetByCode(string code, HisEinvoiceTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEinvoiceTypeDAO.GetByCode(code, filter.Query());
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
