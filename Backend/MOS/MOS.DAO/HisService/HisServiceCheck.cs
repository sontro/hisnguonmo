using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisService
{
    partial class HisServiceCheck : EntityBase
    {
        public HisServiceCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE>();
        }

        private BridgeDAO<HIS_SERVICE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
