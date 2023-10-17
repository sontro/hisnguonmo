using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisHtu
{
    partial class HisHtuCheck : EntityBase
    {
        public HisHtuCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HTU>();
        }

        private BridgeDAO<HIS_HTU> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
