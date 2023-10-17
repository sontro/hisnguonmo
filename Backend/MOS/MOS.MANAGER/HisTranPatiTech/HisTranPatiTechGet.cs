using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTranPatiTech
{
    partial class HisTranPatiTechGet : BusinessBase
    {
        internal HisTranPatiTechGet()
            : base()
        {

        }

        internal HisTranPatiTechGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_TRAN_PATI_TECH> Get(HisTranPatiTechFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTranPatiTechDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRAN_PATI_TECH GetById(long id)
        {
            try
            {
                return GetById(id, new HisTranPatiTechFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRAN_PATI_TECH GetById(long id, HisTranPatiTechFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTranPatiTechDAO.GetById(id, filter.Query());
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
