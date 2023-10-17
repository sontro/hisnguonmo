using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpBltyService
{
    partial class HisExpBltyServiceTruncate : EntityBase
    {
        public HisExpBltyServiceTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_BLTY_SERVICE>();
        }

        private BridgeDAO<HIS_EXP_BLTY_SERVICE> bridgeDAO;

        public bool Truncate(HIS_EXP_BLTY_SERVICE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXP_BLTY_SERVICE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
