using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisCashout
{
    partial class HisCashoutCheck : EntityBase
    {
        public HisCashoutCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CASHOUT>();
        }

        private BridgeDAO<HIS_CASHOUT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
