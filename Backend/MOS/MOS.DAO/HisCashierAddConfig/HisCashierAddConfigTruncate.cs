using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCashierAddConfig
{
    partial class HisCashierAddConfigTruncate : EntityBase
    {
        public HisCashierAddConfigTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CASHIER_ADD_CONFIG>();
        }

        private BridgeDAO<HIS_CASHIER_ADD_CONFIG> bridgeDAO;

        public bool Truncate(HIS_CASHIER_ADD_CONFIG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_CASHIER_ADD_CONFIG> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
