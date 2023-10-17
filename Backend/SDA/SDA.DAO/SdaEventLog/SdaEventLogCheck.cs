using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaEventLog
{
    partial class SdaEventLogCheck : EntityBase
    {
        public SdaEventLogCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_EVENT_LOG>();
        }

        private BridgeDAO<SDA_EVENT_LOG> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
