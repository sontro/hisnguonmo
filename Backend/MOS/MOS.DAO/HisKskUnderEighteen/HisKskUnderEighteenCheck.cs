using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisKskUnderEighteen
{
    partial class HisKskUnderEighteenCheck : EntityBase
    {
        public HisKskUnderEighteenCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_UNDER_EIGHTEEN>();
        }

        private BridgeDAO<HIS_KSK_UNDER_EIGHTEEN> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
