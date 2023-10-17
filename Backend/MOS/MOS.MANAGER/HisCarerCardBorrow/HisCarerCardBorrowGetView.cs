using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCarerCardBorrow
{
    partial class HisCarerCardBorrowGet : BusinessBase
    {
        internal List<V_HIS_CARER_CARD_BORROW> GetView(HisCarerCardBorrowViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCarerCardBorrowDAO.GetView(filter.Query(), param);
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
