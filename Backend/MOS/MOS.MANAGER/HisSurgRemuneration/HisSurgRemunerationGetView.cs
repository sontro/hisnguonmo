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
        internal List<V_HIS_SURG_REMUNERATION> GetView(HisSurgRemunerationViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSurgRemunerationDAO.GetView(filter.Query(), param);
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
