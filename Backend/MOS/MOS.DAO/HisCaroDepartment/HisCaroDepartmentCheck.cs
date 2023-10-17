using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisCaroDepartment
{
    partial class HisCaroDepartmentCheck : EntityBase
    {
        public HisCaroDepartmentCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARO_DEPARTMENT>();
        }

        private BridgeDAO<HIS_CARO_DEPARTMENT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
