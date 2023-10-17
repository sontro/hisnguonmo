using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisUnlimitType
{
    partial class HisUnlimitTypeCheck : EntityBase
    {
        public HisUnlimitTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_UNLIMIT_TYPE>();
        }

        private BridgeDAO<HIS_UNLIMIT_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
