using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRegisterGate
{
    partial class HisRegisterGateCheck : EntityBase
    {
        public HisRegisterGateCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REGISTER_GATE>();
        }

        private BridgeDAO<HIS_REGISTER_GATE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
