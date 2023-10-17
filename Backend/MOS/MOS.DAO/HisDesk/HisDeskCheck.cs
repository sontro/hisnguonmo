using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDesk
{
    partial class HisDeskCheck : EntityBase
    {
        public HisDeskCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DESK>();
        }

        private BridgeDAO<HIS_DESK> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
