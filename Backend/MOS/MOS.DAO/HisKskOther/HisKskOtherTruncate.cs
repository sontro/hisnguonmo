using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskOther
{
    partial class HisKskOtherTruncate : EntityBase
    {
        public HisKskOtherTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_OTHER>();
        }

        private BridgeDAO<HIS_KSK_OTHER> bridgeDAO;

        public bool Truncate(HIS_KSK_OTHER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_KSK_OTHER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
