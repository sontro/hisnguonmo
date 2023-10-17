using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisKskAccess
{
    partial class HisKskAccessCheck : EntityBase
    {
        public HisKskAccessCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_ACCESS>();
        }

        private BridgeDAO<HIS_KSK_ACCESS> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
