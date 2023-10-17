using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisNextTreaIntr
{
    partial class HisNextTreaIntrCheck : EntityBase
    {
        public HisNextTreaIntrCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_NEXT_TREA_INTR>();
        }

        private BridgeDAO<HIS_NEXT_TREA_INTR> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
