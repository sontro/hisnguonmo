using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSereServDebt
{
    partial class HisSereServDebtCheck : EntityBase
    {
        public HisSereServDebtCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_DEBT>();
        }

        private BridgeDAO<HIS_SERE_SERV_DEBT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
