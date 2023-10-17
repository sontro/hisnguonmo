using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEmployee
{
    partial class HisEmployeeCheck : EntityBase
    {
        public HisEmployeeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMPLOYEE>();
        }

        private BridgeDAO<HIS_EMPLOYEE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
