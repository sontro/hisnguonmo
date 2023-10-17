using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCareSum
{
    partial class HisCareSumTruncate : EntityBase
    {
        public HisCareSumTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE_SUM>();
        }

        private BridgeDAO<HIS_CARE_SUM> bridgeDAO;

        public bool Truncate(HIS_CARE_SUM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_CARE_SUM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
