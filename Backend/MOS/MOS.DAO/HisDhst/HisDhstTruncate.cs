using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDhst
{
    partial class HisDhstTruncate : EntityBase
    {
        public HisDhstTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DHST>();
        }

        private BridgeDAO<HIS_DHST> bridgeDAO;

        public bool Truncate(HIS_DHST data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DHST> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
