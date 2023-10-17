using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDesk
{
    partial class HisDeskTruncate : EntityBase
    {
        public HisDeskTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DESK>();
        }

        private BridgeDAO<HIS_DESK> bridgeDAO;

        public bool Truncate(HIS_DESK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DESK> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
