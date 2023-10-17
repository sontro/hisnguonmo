using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisUserGroupTemp
{
    partial class HisUserGroupTempCheck : EntityBase
    {
        public HisUserGroupTempCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_USER_GROUP_TEMP>();
        }

        private BridgeDAO<HIS_USER_GROUP_TEMP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
