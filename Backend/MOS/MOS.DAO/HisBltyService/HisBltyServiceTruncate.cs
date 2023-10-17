using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBltyService
{
    partial class HisBltyServiceTruncate : EntityBase
    {
        public HisBltyServiceTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLTY_SERVICE>();
        }

        private BridgeDAO<HIS_BLTY_SERVICE> bridgeDAO;

        public bool Truncate(HIS_BLTY_SERVICE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BLTY_SERVICE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
