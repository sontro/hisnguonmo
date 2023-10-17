using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDepartment
{
    partial class HisDepartmentCheck : EntityBase
    {
        public HisDepartmentCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEPARTMENT>();
        }

        private BridgeDAO<HIS_DEPARTMENT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
