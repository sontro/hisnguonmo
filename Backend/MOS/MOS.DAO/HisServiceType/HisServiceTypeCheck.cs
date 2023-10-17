using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceType
{
    partial class HisServiceTypeCheck : EntityBase
    {
        public HisServiceTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_TYPE>();
        }

        private BridgeDAO<HIS_SERVICE_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
