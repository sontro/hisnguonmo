using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKsk
{
    partial class HisKskTruncate : EntityBase
    {
        public HisKskTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK>();
        }

        private BridgeDAO<HIS_KSK> bridgeDAO;

        public bool Truncate(HIS_KSK data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_KSK> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
