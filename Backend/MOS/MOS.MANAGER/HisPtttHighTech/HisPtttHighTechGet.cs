using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttHighTech
{
    partial class HisPtttHighTechGet : BusinessBase
    {
        internal HisPtttHighTechGet()
            : base()
        {

        }

        internal HisPtttHighTechGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_PTTT_HIGH_TECH> Get(HisPtttHighTechFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPtttHighTechDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PTTT_HIGH_TECH GetById(long id)
        {
            try
            {
                return GetById(id, new HisPtttHighTechFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_PTTT_HIGH_TECH GetById(long id, HisPtttHighTechFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPtttHighTechDAO.GetById(id, filter.Query());
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
