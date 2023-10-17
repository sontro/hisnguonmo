using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRegisterGate
{
    partial class HisRegisterGateTruncate : EntityBase
    {
        public HisRegisterGateTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REGISTER_GATE>();
        }

        private BridgeDAO<HIS_REGISTER_GATE> bridgeDAO;

        public bool Truncate(HIS_REGISTER_GATE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_REGISTER_GATE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
