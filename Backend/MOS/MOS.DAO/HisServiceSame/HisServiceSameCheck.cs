using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceSame
{
    partial class HisServiceSameCheck : EntityBase
    {
        public HisServiceSameCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_SAME>();
        }

        private BridgeDAO<HIS_SERVICE_SAME> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
