using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisCashierAddConfig
{
    partial class HisCashierAddConfigCheck : EntityBase
    {
        public HisCashierAddConfigCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CASHIER_ADD_CONFIG>();
        }

        private BridgeDAO<HIS_CASHIER_ADD_CONFIG> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
