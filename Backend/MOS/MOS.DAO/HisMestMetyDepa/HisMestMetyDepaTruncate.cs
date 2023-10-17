using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestMetyDepa
{
    partial class HisMestMetyDepaTruncate : EntityBase
    {
        public HisMestMetyDepaTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_METY_DEPA>();
        }

        private BridgeDAO<HIS_MEST_METY_DEPA> bridgeDAO;

        public bool Truncate(HIS_MEST_METY_DEPA data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEST_METY_DEPA> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
