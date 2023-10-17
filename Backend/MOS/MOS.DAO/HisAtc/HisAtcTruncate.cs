using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAtc
{
    partial class HisAtcTruncate : EntityBase
    {
        public HisAtcTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ATC>();
        }

        private BridgeDAO<HIS_ATC> bridgeDAO;

        public bool Truncate(HIS_ATC data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ATC> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
