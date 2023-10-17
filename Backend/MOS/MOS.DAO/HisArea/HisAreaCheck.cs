using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisArea
{
    partial class HisAreaCheck : EntityBase
    {
        public HisAreaCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_AREA>();
        }

        private BridgeDAO<HIS_AREA> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
