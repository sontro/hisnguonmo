using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestMetyUnit
{
    partial class HisMestMetyUnitTruncate : EntityBase
    {
        public HisMestMetyUnitTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_METY_UNIT>();
        }

        private BridgeDAO<HIS_MEST_METY_UNIT> bridgeDAO;

        public bool Truncate(HIS_MEST_METY_UNIT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEST_METY_UNIT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
