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
        internal V_HIS_EINVOICE_TYPE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisEinvoiceTypeViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EINVOICE_TYPE GetViewByCode(string code, HisEinvoiceTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEinvoiceTypeDAO.GetViewByCode(code, filter.Query());
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
