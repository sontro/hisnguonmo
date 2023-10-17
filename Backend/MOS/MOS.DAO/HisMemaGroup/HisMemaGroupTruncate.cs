using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMemaGroup
{
    partial class HisMemaGroupTruncate : EntityBase
    {
        public HisMemaGroupTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEMA_GROUP>();
        }

        private BridgeDAO<HIS_MEMA_GROUP> bridgeDAO;

        public bool Truncate(HIS_MEMA_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEMA_GROUP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
