using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDebate
{
    partial class HisDebateTruncate : EntityBase
    {
        public HisDebateTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE>();
        }

        private BridgeDAO<HIS_DEBATE> bridgeDAO;

        public bool Truncate(HIS_DEBATE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DEBATE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
