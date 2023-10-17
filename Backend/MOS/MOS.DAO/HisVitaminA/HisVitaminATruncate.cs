using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVitaminA
{
    partial class HisVitaminATruncate : EntityBase
    {
        public HisVitaminATruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VITAMIN_A>();
        }

        private BridgeDAO<HIS_VITAMIN_A> bridgeDAO;

        public bool Truncate(HIS_VITAMIN_A data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_VITAMIN_A> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
