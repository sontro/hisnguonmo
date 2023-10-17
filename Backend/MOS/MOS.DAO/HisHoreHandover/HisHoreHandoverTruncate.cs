using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHoreHandover
{
    partial class HisHoreHandoverTruncate : EntityBase
    {
        public HisHoreHandoverTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HORE_HANDOVER>();
        }

        private BridgeDAO<HIS_HORE_HANDOVER> bridgeDAO;

        public bool Truncate(HIS_HORE_HANDOVER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_HORE_HANDOVER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
