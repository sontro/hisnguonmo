using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExecuteRoleUser
{
    partial class HisExecuteRoleUserCheck : EntityBase
    {
        public HisExecuteRoleUserCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXECUTE_ROLE_USER>();
        }

        private BridgeDAO<HIS_EXECUTE_ROLE_USER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
