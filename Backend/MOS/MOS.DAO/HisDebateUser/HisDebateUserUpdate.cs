using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDebateUser
{
    partial class HisDebateUserUpdate : EntityBase
    {
        public HisDebateUserUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_USER>();
        }

        private BridgeDAO<HIS_DEBATE_USER> bridgeDAO;

        public bool Update(HIS_DEBATE_USER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DEBATE_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
