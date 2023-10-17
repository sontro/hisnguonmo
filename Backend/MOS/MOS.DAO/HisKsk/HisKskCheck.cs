using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisKsk
{
    partial class HisKskCheck : EntityBase
    {
        public HisKskCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK>();
        }

        private BridgeDAO<HIS_KSK> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
