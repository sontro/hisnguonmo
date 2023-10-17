using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBloodGiver
{
    partial class HisBloodGiverTruncate : EntityBase
    {
        public HisBloodGiverTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_GIVER>();
        }

        private BridgeDAO<HIS_BLOOD_GIVER> bridgeDAO;

        public bool Truncate(HIS_BLOOD_GIVER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BLOOD_GIVER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
