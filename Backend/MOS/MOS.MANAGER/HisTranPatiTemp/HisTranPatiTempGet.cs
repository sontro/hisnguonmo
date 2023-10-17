using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTranPatiTemp
{
    partial class HisTranPatiTempGet : BusinessBase
    {
        internal HisTranPatiTempGet()
            : base()
        {

        }

        internal HisTranPatiTempGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_TRAN_PATI_TEMP> Get(HisTranPatiTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTranPatiTempDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRAN_PATI_TEMP GetById(long id)
        {
            try
            {
                return GetById(id, new HisTranPatiTempFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRAN_PATI_TEMP GetById(long id, HisTranPatiTempFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTranPatiTempDAO.GetById(id, filter.Query());
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
