using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBid
{
    partial class HisBidUpdate : EntityBase
    {
        public HisBidUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BID>();
        }

        private BridgeDAO<HIS_BID> bridgeDAO;

        public bool Update(HIS_BID data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BID> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
