using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPatyTrty
{
    partial class HisMestPatyTrtyTruncate : EntityBase
    {
        public HisMestPatyTrtyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PATY_TRTY>();
        }

        private BridgeDAO<HIS_MEST_PATY_TRTY> bridgeDAO;

        public bool Truncate(HIS_MEST_PATY_TRTY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEST_PATY_TRTY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
