using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSereServDeposit
{
    partial class HisSereServDepositCheck : EntityBase
    {
        public HisSereServDepositCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_DEPOSIT>();
        }

        private BridgeDAO<HIS_SERE_SERV_DEPOSIT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
