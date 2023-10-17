using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDepartmentTran
{
    partial class HisDepartmentTranCheck : EntityBase
    {
        public HisDepartmentTranCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEPARTMENT_TRAN>();
        }

        private BridgeDAO<HIS_DEPARTMENT_TRAN> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
