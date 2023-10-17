using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisAdr
{
    partial class HisAdrCheck : EntityBase
    {
        public HisAdrCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ADR>();
        }

        private BridgeDAO<HIS_ADR> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
