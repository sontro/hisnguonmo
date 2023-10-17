using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMestTypeUser
{
    partial class HisImpMestTypeUserTruncate : EntityBase
    {
        public HisImpMestTypeUserTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_TYPE_USER>();
        }

        private BridgeDAO<HIS_IMP_MEST_TYPE_USER> bridgeDAO;

        public bool Truncate(HIS_IMP_MEST_TYPE_USER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_IMP_MEST_TYPE_USER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
