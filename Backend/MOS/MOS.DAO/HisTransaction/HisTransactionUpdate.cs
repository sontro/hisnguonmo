using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTransaction
{
    partial class HisTransactionUpdate : EntityBase
    {
        public HisTransactionUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSACTION>();
        }

        private BridgeDAO<HIS_TRANSACTION> bridgeDAO;

        public bool Update(HIS_TRANSACTION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TRANSACTION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
