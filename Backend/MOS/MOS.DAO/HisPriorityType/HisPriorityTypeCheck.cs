using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPriorityType
{
    partial class HisPriorityTypeCheck : EntityBase
    {
        public HisPriorityTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PRIORITY_TYPE>();
        }

        private BridgeDAO<HIS_PRIORITY_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
