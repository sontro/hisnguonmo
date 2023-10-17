using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCare
{
    partial class HisCareTruncate : EntityBase
    {
        public HisCareTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE>();
        }

        private BridgeDAO<HIS_CARE> bridgeDAO;

        public bool Truncate(HIS_CARE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_CARE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
