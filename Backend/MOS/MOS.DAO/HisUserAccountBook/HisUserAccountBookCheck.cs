using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisUserAccountBook
{
    partial class HisUserAccountBookCheck : EntityBase
    {
        public HisUserAccountBookCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_USER_ACCOUNT_BOOK>();
        }

        private BridgeDAO<HIS_USER_ACCOUNT_BOOK> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
