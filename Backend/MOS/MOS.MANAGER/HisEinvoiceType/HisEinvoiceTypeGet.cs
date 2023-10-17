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
        internal HisEinvoiceTypeGet()
            : base()
        {

        }

        internal HisEinvoiceTypeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_EINVOICE_TYPE> Get(HisEinvoiceTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEinvoiceTypeDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EINVOICE_TYPE GetById(long id)
        {
            try
            {
                return GetById(id, new HisEinvoiceTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EINVOICE_TYPE GetById(long id, HisEinvoiceTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEinvoiceTypeDAO.GetById(id, filter.Query());
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
