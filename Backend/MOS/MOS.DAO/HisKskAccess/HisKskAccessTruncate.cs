using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskAccess
{
    partial class HisKskAccessTruncate : EntityBase
    {
        public HisKskAccessTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_ACCESS>();
        }

        private BridgeDAO<HIS_KSK_ACCESS> bridgeDAO;

        public bool Truncate(HIS_KSK_ACCESS data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_KSK_ACCESS> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
