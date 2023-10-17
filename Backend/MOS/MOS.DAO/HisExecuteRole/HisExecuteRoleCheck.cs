using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExecuteRole
{
    partial class HisExecuteRoleCheck : EntityBase
    {
        public HisExecuteRoleCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXECUTE_ROLE>();
        }

        private BridgeDAO<HIS_EXECUTE_ROLE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
