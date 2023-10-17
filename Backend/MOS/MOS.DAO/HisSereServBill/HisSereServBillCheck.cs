using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSereServBill
{
    partial class HisSereServBillCheck : EntityBase
    {
        public HisSereServBillCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_BILL>();
        }

        private BridgeDAO<HIS_SERE_SERV_BILL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
