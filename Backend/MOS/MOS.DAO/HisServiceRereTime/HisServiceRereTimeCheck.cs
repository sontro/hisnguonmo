using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceRereTime
{
    partial class HisServiceRereTimeCheck : EntityBase
    {
        public HisServiceRereTimeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_RERE_TIME>();
        }

        private BridgeDAO<HIS_SERVICE_RERE_TIME> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
