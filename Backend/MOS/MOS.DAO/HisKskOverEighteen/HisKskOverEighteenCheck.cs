using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisKskOverEighteen
{
    partial class HisKskOverEighteenCheck : EntityBase
    {
        public HisKskOverEighteenCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_OVER_EIGHTEEN>();
        }

        private BridgeDAO<HIS_KSK_OVER_EIGHTEEN> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
