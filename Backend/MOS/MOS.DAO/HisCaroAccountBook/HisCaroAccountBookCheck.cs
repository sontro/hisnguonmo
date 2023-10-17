using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisCaroAccountBook
{
    partial class HisCaroAccountBookCheck : EntityBase
    {
        public HisCaroAccountBookCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARO_ACCOUNT_BOOK>();
        }

        private BridgeDAO<HIS_CARO_ACCOUNT_BOOK> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
