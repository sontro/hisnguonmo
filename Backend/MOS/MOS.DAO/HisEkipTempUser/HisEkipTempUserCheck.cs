using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEkipTempUser
{
    partial class HisEkipTempUserCheck : EntityBase
    {
        public HisEkipTempUserCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EKIP_TEMP_USER>();
        }

        private BridgeDAO<HIS_EKIP_TEMP_USER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
