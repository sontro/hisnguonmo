using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisContactPoint
{
    partial class HisContactPointCheck : EntityBase
    {
        public HisContactPointCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CONTACT_POINT>();
        }

        private BridgeDAO<HIS_CONTACT_POINT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
