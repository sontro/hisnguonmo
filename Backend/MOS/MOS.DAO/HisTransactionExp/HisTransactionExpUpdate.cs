using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTransactionExp
{
    partial class HisTransactionExpUpdate : EntityBase
    {
        public HisTransactionExpUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSACTION_EXP>();
        }

        private BridgeDAO<HIS_TRANSACTION_EXP> bridgeDAO;

        public bool Update(HIS_TRANSACTION_EXP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TRANSACTION_EXP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
