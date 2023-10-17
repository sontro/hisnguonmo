using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCashierAddConfig
{
    partial class HisCashierAddConfigUpdate : EntityBase
    {
        public HisCashierAddConfigUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CASHIER_ADD_CONFIG>();
        }

        private BridgeDAO<HIS_CASHIER_ADD_CONFIG> bridgeDAO;

        public bool Update(HIS_CASHIER_ADD_CONFIG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CASHIER_ADD_CONFIG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
