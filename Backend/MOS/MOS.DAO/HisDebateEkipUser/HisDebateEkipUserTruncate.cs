using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDebateEkipUser
{
    partial class HisDebateEkipUserTruncate : EntityBase
    {
        public HisDebateEkipUserTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_EKIP_USER>();
        }

        private BridgeDAO<HIS_DEBATE_EKIP_USER> bridgeDAO;

        public bool Truncate(HIS_DEBATE_EKIP_USER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DEBATE_EKIP_USER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
