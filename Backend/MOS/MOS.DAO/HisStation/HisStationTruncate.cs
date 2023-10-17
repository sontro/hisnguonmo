using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisStation
{
    partial class HisStationTruncate : EntityBase
    {
        public HisStationTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_STATION>();
        }

        private BridgeDAO<HIS_STATION> bridgeDAO;

        public bool Truncate(HIS_STATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_STATION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
