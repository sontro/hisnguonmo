using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisCard
{
    partial class HisCardCheck : EntityBase
    {
        public HisCardCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARD>();
        }

        private BridgeDAO<HIS_CARD> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
