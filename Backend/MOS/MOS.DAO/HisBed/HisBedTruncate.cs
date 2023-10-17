using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBed
{
    partial class HisBedTruncate : EntityBase
    {
        public HisBedTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BED>();
        }

        private BridgeDAO<HIS_BED> bridgeDAO;

        public bool Truncate(HIS_BED data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BED> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
