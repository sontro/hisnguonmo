using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPtttGroup
{
    partial class HisPtttGroupCheck : EntityBase
    {
        public HisPtttGroupCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_GROUP>();
        }

        private BridgeDAO<HIS_PTTT_GROUP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
