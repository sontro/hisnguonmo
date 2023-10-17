using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBidType
{
    partial class HisBidTypeUpdate : EntityBase
    {
        public HisBidTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BID_TYPE>();
        }

        private BridgeDAO<HIS_BID_TYPE> bridgeDAO;

        public bool Update(HIS_BID_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BID_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
