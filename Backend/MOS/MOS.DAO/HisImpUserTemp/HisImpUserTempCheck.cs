using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisImpUserTemp
{
    partial class HisImpUserTempCheck : EntityBase
    {
        public HisImpUserTempCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_USER_TEMP>();
        }

        private BridgeDAO<HIS_IMP_USER_TEMP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
