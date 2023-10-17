using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPetroleum
{
    partial class HisPetroleumCheck : EntityBase
    {
        public HisPetroleumCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PETROLEUM>();
        }

        private BridgeDAO<HIS_PETROLEUM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
