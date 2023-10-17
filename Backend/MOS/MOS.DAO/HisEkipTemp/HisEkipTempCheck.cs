using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEkipTemp
{
    partial class HisEkipTempCheck : EntityBase
    {
        public HisEkipTempCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EKIP_TEMP>();
        }

        private BridgeDAO<HIS_EKIP_TEMP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
