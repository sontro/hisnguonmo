using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMachine
{
    partial class HisMachineTruncate : EntityBase
    {
        public HisMachineTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MACHINE>();
        }

        private BridgeDAO<HIS_MACHINE> bridgeDAO;

        public bool Truncate(HIS_MACHINE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MACHINE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
