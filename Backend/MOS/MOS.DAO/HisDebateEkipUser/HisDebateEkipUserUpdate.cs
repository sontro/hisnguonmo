using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDebateEkipUser
{
    partial class HisDebateEkipUserUpdate : EntityBase
    {
        public HisDebateEkipUserUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_EKIP_USER>();
        }

        private BridgeDAO<HIS_DEBATE_EKIP_USER> bridgeDAO;

        public bool Update(HIS_DEBATE_EKIP_USER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DEBATE_EKIP_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
