using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisHeinServiceType
{
    partial class HisHeinServiceTypeCheck : EntityBase
    {
        public HisHeinServiceTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HEIN_SERVICE_TYPE>();
        }

        private BridgeDAO<HIS_HEIN_SERVICE_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
