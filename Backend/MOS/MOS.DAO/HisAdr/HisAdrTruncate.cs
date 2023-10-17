using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAdr
{
    partial class HisAdrTruncate : EntityBase
    {
        public HisAdrTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ADR>();
        }

        private BridgeDAO<HIS_ADR> bridgeDAO;

        public bool Truncate(HIS_ADR data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ADR> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
