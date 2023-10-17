using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBltyService
{
    partial class HisBltyServiceCheck : EntityBase
    {
        public HisBltyServiceCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLTY_SERVICE>();
        }

        private BridgeDAO<HIS_BLTY_SERVICE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
