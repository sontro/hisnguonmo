using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskOverEighteen
{
    partial class HisKskOverEighteenTruncate : EntityBase
    {
        public HisKskOverEighteenTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_OVER_EIGHTEEN>();
        }

        private BridgeDAO<HIS_KSK_OVER_EIGHTEEN> bridgeDAO;

        public bool Truncate(HIS_KSK_OVER_EIGHTEEN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_KSK_OVER_EIGHTEEN> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
