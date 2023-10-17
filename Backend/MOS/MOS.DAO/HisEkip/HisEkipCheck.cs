using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEkip
{
    partial class HisEkipCheck : EntityBase
    {
        public HisEkipCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EKIP>();
        }

        private BridgeDAO<HIS_EKIP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
