using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMetyMety
{
    partial class HisMetyMetyTruncate : EntityBase
    {
        public HisMetyMetyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_METY_METY>();
        }

        private BridgeDAO<HIS_METY_METY> bridgeDAO;

        public bool Truncate(HIS_METY_METY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_METY_METY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
