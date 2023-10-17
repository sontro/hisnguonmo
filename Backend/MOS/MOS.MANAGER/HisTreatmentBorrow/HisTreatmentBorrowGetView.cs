using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentBorrow
{
    partial class HisTreatmentBorrowGet : BusinessBase
    {
        internal List<V_HIS_TREATMENT_BORROW> GetView(HisTreatmentBorrowViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTreatmentBorrowDAO.GetView(filter.Query(), param);
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
