using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPatySub
{
    partial class HisMestPatySubTruncate : EntityBase
    {
        public HisMestPatySubTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PATY_SUB>();
        }

        private BridgeDAO<HIS_MEST_PATY_SUB> bridgeDAO;

        public bool Truncate(HIS_MEST_PATY_SUB data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEST_PATY_SUB> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
