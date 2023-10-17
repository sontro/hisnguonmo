using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMatyMaty
{
    partial class HisMatyMatyTruncate : EntityBase
    {
        public HisMatyMatyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATY_MATY>();
        }

        private BridgeDAO<HIS_MATY_MATY> bridgeDAO;

        public bool Truncate(HIS_MATY_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MATY_MATY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
