using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTransactionType
{
    partial class HisTransactionTypeUpdate : EntityBase
    {
        public HisTransactionTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSACTION_TYPE>();
        }

        private BridgeDAO<HIS_TRANSACTION_TYPE> bridgeDAO;

        public bool Update(HIS_TRANSACTION_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TRANSACTION_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
