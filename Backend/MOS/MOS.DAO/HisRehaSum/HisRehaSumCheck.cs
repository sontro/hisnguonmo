using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRehaSum
{
    partial class HisRehaSumCheck : EntityBase
    {
        public HisRehaSumCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REHA_SUM>();
        }

        private BridgeDAO<HIS_REHA_SUM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
