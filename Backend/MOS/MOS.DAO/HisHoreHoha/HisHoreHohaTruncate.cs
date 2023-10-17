using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHoreHoha
{
    partial class HisHoreHohaTruncate : EntityBase
    {
        public HisHoreHohaTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HORE_HOHA>();
        }

        private BridgeDAO<HIS_HORE_HOHA> bridgeDAO;

        public bool Truncate(HIS_HORE_HOHA data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_HORE_HOHA> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
