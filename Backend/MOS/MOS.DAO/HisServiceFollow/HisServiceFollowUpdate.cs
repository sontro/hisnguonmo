using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceFollow
{
    partial class HisServiceFollowUpdate : EntityBase
    {
        public HisServiceFollowUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_FOLLOW>();
        }

        private BridgeDAO<HIS_SERVICE_FOLLOW> bridgeDAO;

        public bool Update(HIS_SERVICE_FOLLOW data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE_FOLLOW> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
