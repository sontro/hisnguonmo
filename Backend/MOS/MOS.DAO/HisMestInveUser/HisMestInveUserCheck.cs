using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMestInveUser
{
    partial class HisMestInveUserCheck : EntityBase
    {
        public HisMestInveUserCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_INVE_USER>();
        }

        private BridgeDAO<HIS_MEST_INVE_USER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
