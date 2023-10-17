using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDbLog
{
    partial class HisDbLogCheck : EntityBase
    {
        public HisDbLogCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DB_LOG>();
        }

        private BridgeDAO<HIS_DB_LOG> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
