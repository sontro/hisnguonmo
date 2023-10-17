using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SDA.DAO.SdaNotify
{
    partial class SdaNotifyCheck : EntityBase
    {
        public SdaNotifyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_NOTIFY>();
        }

        private BridgeDAO<SDA_NOTIFY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
