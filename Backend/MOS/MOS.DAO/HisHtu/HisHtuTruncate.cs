using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHtu
{
    partial class HisHtuTruncate : EntityBase
    {
        public HisHtuTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HTU>();
        }

        private BridgeDAO<HIS_HTU> bridgeDAO;

        public bool Truncate(HIS_HTU data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_HTU> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
