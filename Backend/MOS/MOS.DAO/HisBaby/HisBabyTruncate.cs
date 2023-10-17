using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBaby
{
    partial class HisBabyTruncate : EntityBase
    {
        public HisBabyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BABY>();
        }

        private BridgeDAO<HIS_BABY> bridgeDAO;

        public bool Truncate(HIS_BABY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BABY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
