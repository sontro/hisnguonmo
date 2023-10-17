using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBlood
{
    partial class HisBloodTruncate : EntityBase
    {
        public HisBloodTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD>();
        }

        private BridgeDAO<HIS_BLOOD> bridgeDAO;

        public bool Truncate(HIS_BLOOD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BLOOD> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
