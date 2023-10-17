using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExecuteGroup
{
    partial class HisExecuteGroupCheck : EntityBase
    {
        public HisExecuteGroupCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXECUTE_GROUP>();
        }

        private BridgeDAO<HIS_EXECUTE_GROUP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
