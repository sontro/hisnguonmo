using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPermission
{
    partial class HisPermissionCheck : EntityBase
    {
        public HisPermissionCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PERMISSION>();
        }

        private BridgeDAO<HIS_PERMISSION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
