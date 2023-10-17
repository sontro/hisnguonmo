using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisCarerCard
{
    partial class HisCarerCardCheck : EntityBase
    {
        public HisCarerCardCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARER_CARD>();
        }

        private BridgeDAO<HIS_CARER_CARD> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
