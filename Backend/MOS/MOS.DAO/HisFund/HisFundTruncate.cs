using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisFund
{
    partial class HisFundTruncate : EntityBase
    {
        public HisFundTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FUND>();
        }

        private BridgeDAO<HIS_FUND> bridgeDAO;

        public bool Truncate(HIS_FUND data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_FUND> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
