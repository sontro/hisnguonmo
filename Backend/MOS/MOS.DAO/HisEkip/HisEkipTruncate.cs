using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEkip
{
    partial class HisEkipTruncate : EntityBase
    {
        public HisEkipTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EKIP>();
        }

        private BridgeDAO<HIS_EKIP> bridgeDAO;

        public bool Truncate(HIS_EKIP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EKIP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
