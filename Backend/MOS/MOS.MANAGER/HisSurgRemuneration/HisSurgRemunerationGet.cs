using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSurgRemuneration
{
    partial class HisSurgRemunerationGet : BusinessBase
    {
        internal HisSurgRemunerationGet()
            : base()
        {

        }

        internal HisSurgRemunerationGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SURG_REMUNERATION> Get(HisSurgRemunerationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSurgRemunerationDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SURG_REMUNERATION GetById(long id)
        {
            try
            {
                return GetById(id, new HisSurgRemunerationFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SURG_REMUNERATION GetById(long id, HisSurgRemunerationFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSurgRemunerationDAO.GetById(id, filter.Query());
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
