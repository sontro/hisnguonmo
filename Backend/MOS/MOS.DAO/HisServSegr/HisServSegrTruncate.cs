using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServSegr
{
    partial class HisServSegrTruncate : EntityBase
    {
        public HisServSegrTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERV_SEGR>();
        }

        private BridgeDAO<HIS_SERV_SEGR> bridgeDAO;

        public bool Truncate(HIS_SERV_SEGR data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERV_SEGR> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
