using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBank
{
    partial class HisBankCheck : EntityBase
    {
        public HisBankCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BANK>();
        }

        private BridgeDAO<HIS_BANK> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
