using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestInveUser
{
    partial class HisMestInveUserTruncate : EntityBase
    {
        public HisMestInveUserTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_INVE_USER>();
        }

        private BridgeDAO<HIS_MEST_INVE_USER> bridgeDAO;

        public bool Truncate(HIS_MEST_INVE_USER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEST_INVE_USER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
