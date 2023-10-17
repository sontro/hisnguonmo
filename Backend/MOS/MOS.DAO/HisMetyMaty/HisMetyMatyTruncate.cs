using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMetyMaty
{
    partial class HisMetyMatyTruncate : EntityBase
    {
        public HisMetyMatyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_METY_MATY>();
        }

        private BridgeDAO<HIS_METY_MATY> bridgeDAO;

        public bool Truncate(HIS_METY_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_METY_MATY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
