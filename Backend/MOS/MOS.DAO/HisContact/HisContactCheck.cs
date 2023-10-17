using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisContact
{
    partial class HisContactCheck : EntityBase
    {
        public HisContactCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CONTACT>();
        }

        private BridgeDAO<HIS_CONTACT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
