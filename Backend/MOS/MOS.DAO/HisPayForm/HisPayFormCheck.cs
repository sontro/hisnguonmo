using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPayForm
{
    partial class HisPayFormCheck : EntityBase
    {
        public HisPayFormCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PAY_FORM>();
        }

        private BridgeDAO<HIS_PAY_FORM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
