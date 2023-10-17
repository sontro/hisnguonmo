using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttGroup
{
    partial class HisPtttGroupTruncate : EntityBase
    {
        public HisPtttGroupTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_GROUP>();
        }

        private BridgeDAO<HIS_PTTT_GROUP> bridgeDAO;

        public bool Truncate(HIS_PTTT_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_PTTT_GROUP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
