using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRegisterGate
{
    partial class HisRegisterGateUpdate : EntityBase
    {
        public HisRegisterGateUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REGISTER_GATE>();
        }

        private BridgeDAO<HIS_REGISTER_GATE> bridgeDAO;

        public bool Update(HIS_REGISTER_GATE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_REGISTER_GATE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
