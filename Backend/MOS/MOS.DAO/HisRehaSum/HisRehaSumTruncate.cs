using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRehaSum
{
    partial class HisRehaSumTruncate : EntityBase
    {
        public HisRehaSumTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REHA_SUM>();
        }

        private BridgeDAO<HIS_REHA_SUM> bridgeDAO;

        public bool Truncate(HIS_REHA_SUM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_REHA_SUM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
