using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestMatyDepa
{
    partial class HisMestMatyDepaTruncate : EntityBase
    {
        public HisMestMatyDepaTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_MATY_DEPA>();
        }

        private BridgeDAO<HIS_MEST_MATY_DEPA> bridgeDAO;

        public bool Truncate(HIS_MEST_MATY_DEPA data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEST_MATY_DEPA> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
