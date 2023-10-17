using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceNumOrder
{
    partial class HisServiceNumOrderUpdate : EntityBase
    {
        public HisServiceNumOrderUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_NUM_ORDER>();
        }

        private BridgeDAO<HIS_SERVICE_NUM_ORDER> bridgeDAO;

        public bool Update(HIS_SERVICE_NUM_ORDER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE_NUM_ORDER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
