using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceFollow
{
    partial class HisServiceFollowTruncate : EntityBase
    {
        public HisServiceFollowTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_FOLLOW>();
        }

        private BridgeDAO<HIS_SERVICE_FOLLOW> bridgeDAO;

        public bool Truncate(HIS_SERVICE_FOLLOW data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERVICE_FOLLOW> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
