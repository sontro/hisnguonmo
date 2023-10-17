using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExpBltyService
{
    partial class HisExpBltyServiceCheck : EntityBase
    {
        public HisExpBltyServiceCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_BLTY_SERVICE>();
        }

        private BridgeDAO<HIS_EXP_BLTY_SERVICE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
