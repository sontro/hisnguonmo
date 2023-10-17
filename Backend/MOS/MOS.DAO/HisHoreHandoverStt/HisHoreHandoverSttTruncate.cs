using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHoreHandoverStt
{
    partial class HisHoreHandoverSttTruncate : EntityBase
    {
        public HisHoreHandoverSttTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HORE_HANDOVER_STT>();
        }

        private BridgeDAO<HIS_HORE_HANDOVER_STT> bridgeDAO;

        public bool Truncate(HIS_HORE_HANDOVER_STT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_HORE_HANDOVER_STT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
