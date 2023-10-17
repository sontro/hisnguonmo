using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEkipUser
{
    partial class HisEkipUserCheck : EntityBase
    {
        public HisEkipUserCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EKIP_USER>();
        }

        private BridgeDAO<HIS_EKIP_USER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
